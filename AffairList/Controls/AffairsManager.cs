using Microsoft.VisualBasic;

namespace AffairList
{
    public partial class AffairsManager : UserControl, IChildable
    {
        private int _currentDragIndex = 0;
        private int _deadlineDateNTagLength = 21;
        private int _selectedAffairIndex = -1;

        private string _priorityTag = "<priority>";
        private string _priorityWord = "\"Priority\"";
        private string _deadlineTag = "<deadline>";

        private List<string> _lines;

        private bool _isDragging = false;

        private Keys _addAffairKey = Keys.Enter;
        private Keys _deleteAffairKey = Keys.Delete;

        private Settings _settings;
        public IParentable ParentElement { get; set; }

        public AffairsManager(Settings settings, IParentable parentElement)
        {
            _settings = settings;
            ParentElement = parentElement;
            InitializeComponent();
            LoadProfiles();
            Task.Run(() => LoadTextAsync());
        }
        private void LoadProfiles()
        {
            int profilesCount = 0;
            foreach (string profile in Directory.EnumerateFiles(_settings.listsDirectoryFullPath))
            {
                profilesCount++;
                FileInfo profileInfo = new FileInfo(profile);
                ProfileBox.Items.Add(profileInfo.Name);
            }
            if (profilesCount > 0)
            {
                FileInfo selectedProfile = new FileInfo(_settings.GetCurrentProfile());
                ProfileBox.SelectedIndex = ProfileBox.Items.IndexOf(selectedProfile.Name);
            }
        }
        // Дополнить логику тем, что инициализацию lines вынести в конструктор
        private async Task LoadTextAsync()
        {
            Affairs.Items.Clear();
            if (_settings.CurrentListNotNull())
            {
                _lines = (await File.ReadAllLinesAsync(_settings.GetCurrentProfile()))
                    .OrderByDescending(x => x.EndsWith(_priorityTag)).AsParallel().ToList();

                await foreach (string line in (IAsyncEnumerable<string>)_lines)
                {
                    string currentLine = line;
                    if (currentLine.EndsWith(_priorityTag))
                    {
                        currentLine = currentLine
                            .Remove(currentLine.Length - _priorityTag.Length);
                        currentLine += " " + _priorityWord;
                    }
                    if (currentLine.StartsWith(_deadlineTag))
                    {
                        currentLine = currentLine.Remove(0, _deadlineTag.Length);
                    }
                    Affairs.Items.Add(currentLine);
                }
                if (_selectedAffairIndex != -1 && Affairs.Items.Count > 0) Affairs.SelectedIndex = _selectedAffairIndex;
                else if (Affairs.Items.Count > 0) Affairs.SelectedIndex = 0;
            }
        }
        private async void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == _settings.GetCloseKey())
            {
                ParentElement.Exit();
            }
            if (e.KeyCode == _settings.GetReturnKey())
            {
                ParentElement.Return();
            }
            if (e.KeyCode == _addAffairKey)
            {
                await AddAffairAsync();
            }
            if (e.KeyCode == _deleteAffairKey)
            {
                await DeleteAffairAsync();
            }
        }

        private void CloseButton_Click(object sender, EventArgs e) 
            => ParentElement.Exit();

        private void AffairsLab_MouseDown(object sender, MouseEventArgs e) 
            => ParentElement.SetLastPoint(e);

        private void AffairsLab_MouseMove(object sender, MouseEventArgs e) 
            => ParentElement.MoveForm(e);

        private void NameBackground_MouseDown_1(object sender, MouseEventArgs e)
            => ParentElement.SetLastPoint(e);

        private void NameBackground_MouseMove_1(object sender, MouseEventArgs e) 
            => ParentElement.MoveForm(e);

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Gray;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Black;
        }

        private async Task AddAffairAsync()
        {
            if (AffairInput.Text.Trim() == "")
            {
                MessageBox.Show("Error, textbox is null");
                return;
            }
            if (ContainKeyWords(AffairInput.Text)) return;

            if (!_settings.CurrentListNotNull())
            {
                await IfCurrentListDisappearedAsync();
            }

            string inputText = AffairInput.Text + ".";

            Task appendingText = AppendTextAsync(inputText + "\n");

            Affairs.Items.Add(inputText);
            Affairs.SelectedIndex = Affairs.Items.Count - 1;

            _lines.Add(inputText);

            AffairInput.Text = "";
            await appendingText;
        }
        private async Task DeleteAffairAsync()
        {
            if (Affairs.SelectedIndex == -1) return;

            if (_settings.DoesAskToDelete())
            {
                DialogResult dialogres = MessageBox.Show("Do you want to delete the affair?",
                    "Confirm form",
                    MessageBoxButtons.YesNo);
                if (dialogres == DialogResult.No) return;
            }

            if (!_settings.CurrentListNotNull())
            {
                await IfCurrentListDisappearedAsync();
            }
            _lines.RemoveAt(Affairs.SelectedIndex);

            Task savingText = SaveTextAsync(_lines);

            int selectedIndex = 0;
            if (Affairs.SelectedIndex == 0 && Affairs.Items.Count > 1)
            {
                Affairs.SelectedIndex++;
                selectedIndex = Affairs.SelectedIndex - 1;
            }
            else if (Affairs.Items.Count > 1)
            {
                Affairs.SelectedIndex--;
                selectedIndex = Affairs.SelectedIndex + 1;
            }

            Affairs.Items.RemoveAt(selectedIndex);
            await savingText;
        }
        private async Task IfCurrentListDisappearedAsync()
        {
            await _settings.SelectFirstProfileAsync();
            if (string.IsNullOrEmpty(_settings.GetCurrentProfile())) 
                _settings.CreateDefaultListAsync();
        }
        private async void AddAffairButton_Click(object sender, EventArgs e) => await AddAffairAsync();
        private async void DeleteButton_Click(object sender, EventArgs e) => await DeleteAffairAsync();
        private void ClearButton_Click(object sender, EventArgs e) => AffairInput.Clear();

        private void BackButton_Click(object sender, EventArgs e) => ParentElement.Return();

        private void ChangeListForm_FormClosing(object sender, FormClosingEventArgs e) 
            => ParentElement.Exit();

        private async void AddDeadlineButton_Click(object sender, EventArgs e)
        {
            if (Affairs.SelectedIndex == -1) return;

            if (_lines[Affairs.SelectedIndex].StartsWith(_deadlineTag))
            {
                DialogResult dialogres = MessageBox.Show(
                    "Do you want to delete the deadline or just change it. yes - delete, no - change?",
                    "Confirm form",
                    MessageBoxButtons.YesNoCancel);

                if (dialogres == DialogResult.Cancel) return;

                if (dialogres == DialogResult.No) AddDeadline(hasDeadline: true);
                else if (dialogres == DialogResult.Yes) DeleteDeadline();
            }
            else
            {
                AddDeadline(hasDeadline: false);
            }

            await SaveTextAsync(_lines);
        }
        private void DeleteDeadline()
        {
            _lines[Affairs.SelectedIndex] = _lines[Affairs.SelectedIndex]
                    .Remove(0, _deadlineDateNTagLength);

            Affairs.Items[Affairs.SelectedIndex] = Affairs.Items[Affairs.SelectedIndex]
                .ToString()!
                .Remove(0, _deadlineTag.Length + 1);
        }
        private void AddDeadline(bool hasDeadline)
        {
            string deadline = "";
            InputDeadlineForm inputDeadline = new InputDeadlineForm();
            inputDeadline.OnConfirm += delegate
            {
                deadline = inputDeadline.deadline.Date.ToShortDateString();
            };
            inputDeadline.ShowDialog();
            if (deadline == "") return;

            if (hasDeadline) DeleteDeadline();

            Affairs.Items[Affairs.SelectedIndex] = deadline + " " + Affairs.Items[Affairs.SelectedIndex];

            _lines[Affairs.SelectedIndex] = _deadlineTag + deadline + " " + _lines[Affairs.SelectedIndex];
        }
        private async void RenameAffairButton_Click(object sender, EventArgs e)
        {
            if (Affairs.SelectedIndex == -1) return;

            string selectedWord = _lines[Affairs.SelectedIndex];
            string affair = selectedWord;

            if (selectedWord.StartsWith(_deadlineTag))
            {
                affair = selectedWord.Remove(0, _deadlineDateNTagLength);
            }
            if (selectedWord.EndsWith(_priorityTag))
            {
                affair = affair.Remove(affair.Length - _priorityTag.Length - 1);
            }
            affair = affair.Remove(affair.Length - 1); // Убираем точку в конце

            string renaming = Interaction
                .InputBox("Enter renaming", "Input box", affair);

            if (ContainKeyWords(renaming)) return;
            if (string.IsNullOrEmpty(renaming)) return;

            selectedWord = selectedWord.Replace(affair, renaming);

            _lines[Affairs.SelectedIndex] = selectedWord;

            Task savingText = SaveTextAsync(_lines);

            Affairs.Items[Affairs.SelectedIndex] = Affairs.Items[Affairs.SelectedIndex]
                .ToString()!.Replace(affair, renaming);

            await savingText;
        }
        private bool ContainKeyWords(string word)
        {
            if (word.Contains(_deadlineTag) || word.Contains(_priorityTag)
                || word.Contains(_priorityWord))
            {
                MessageBox.Show("Error, you word is perhibited to contain - " +
                    $"{_deadlineTag}, {_priorityTag}, {_priorityWord}");
                return true;
            }
            return false;
        }

        private async void PriorityButton_Click(object sender, EventArgs e)
        {
            if (Affairs.SelectedIndex == -1) return;

            if (_lines[Affairs.SelectedIndex].EndsWith(_priorityTag))
            {

                _lines[Affairs.SelectedIndex] = _lines[Affairs.SelectedIndex]
                    .Remove(_lines[Affairs.SelectedIndex].Length - (" " + _priorityTag).Length);
            }
            else
            {
                _lines[Affairs.SelectedIndex] += " " + _priorityTag;
            }

            Task savingText = SaveTextAsync(_lines);
            Task loadingText = LoadTextAsync();
            await Task.WhenAll(savingText, loadingText);
        }
        private void Affairs_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_isDragging)
            {
                _isDragging = true;
                _currentDragIndex = Affairs.SelectedIndex;
            }
        }

        private async void Affairs_MouseUp(object sender, MouseEventArgs e)
        {
            if (Affairs.SelectedIndex == -1) return;
            bool newPlacePriority = _lines[Affairs.SelectedIndex].EndsWith(_priorityTag);
            bool oldPlacePriority = _lines[_currentDragIndex].EndsWith(_priorityTag);

            if (newPlacePriority != oldPlacePriority) return;

            // Не менять, уже нечего
            string switcher = _lines[_currentDragIndex];
            _lines[_currentDragIndex] = _lines[Affairs.SelectedIndex];
            _lines[Affairs.SelectedIndex] = switcher;

            Task savingText = SaveTextAsync(_lines);

            switcher = (string)Affairs.Items[_currentDragIndex];
            Affairs.Items[_currentDragIndex] = Affairs.Items[Affairs.SelectedIndex];
            Affairs.Items[Affairs.SelectedIndex] = switcher;

            _isDragging = false;
            await savingText;
        }

        private void MinimizeButton_Click(object sender, EventArgs e) => ParentElement.MinimizeForm();

        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Gray;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Black;
        }
        private async Task ChangeProfileAsync()
        {
            foreach (var profile in Directory.EnumerateFiles(_settings.listsDirectoryFullPath))
            {
                FileInfo profileInfo = new FileInfo(profile);
                if (profileInfo.Name == ProfileBox.SelectedItem!.ToString())
                {
                    _settings.SetCurrentProfile(profileInfo.FullName);
                    await _settings.SaveSettingsAsync();
                    _selectedAffairIndex = 0;
                    return;
                }
            }
        }

        private async void ProfileBox_TextUpdate(object sender, EventArgs e)
        {
            if (!File.Exists(ProfileBox.Text))
            {
                ProfileBox.Text = _settings.GetCurrentProfile();
                return;
            }
            await ChangeProfileAsync();
            await LoadTextAsync();
        }

        private async void ProfileBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            await ChangeProfileAsync();
            await LoadTextAsync();
        }

        private void Affairs_SelectedValueChanged(object sender, EventArgs e)
        {
            _selectedAffairIndex = Affairs.SelectedIndex;
        }
        private async Task SaveTextAsync(List<string> lines)
        {
            await File.WriteAllLinesAsync(_settings.GetCurrentProfile(), lines);
        }
        private async Task AppendTextAsync(string line)
        {
            await File.AppendAllTextAsync(_settings.GetCurrentProfile(), line);
        }
    }
}

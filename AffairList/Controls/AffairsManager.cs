using AffairList.Classes.Commands.AffairsManagerCommands;
using AffairList.Interfaces;
using Microsoft.VisualBasic;

namespace AffairList
{
    public partial class AffairsManager : UserControl, IChildable, IKeyPreviewable
    {
        private int _currentDragIndex = 0;
        private int _deadlineDateNTagLength = 21;
        private int _selectedAffair = 0;

        private string _priorityTag = "<priority>";
        private string _priorityWord = "\"Priority\"";
        private string _deadlineTag = "<deadline>";

        private List<string> _lines;

        private bool _isDragging = false;

        private Keys _addAffairKey = Keys.Enter;
        private Keys _deleteAffairKey = Keys.Delete;

        private Settings _settings;
        public IParentable ParentElement { get; private set; }
        public KeyEventHandler KeyDownHandlers { get; private set; }
        public KeyPressEventHandler KeyPressHandlers { get; private set; }
        public KeyEventHandler KeyUpHandlers { get; private set; }

        private Stack<ICommandAf> _undoOperations = null!;
        private Stack<ICommandAf> _redoOperations = null!;

        public AffairsManager(Settings settings, IParentable parentElement)
        {
            InitializeComponent();
            _settings = settings;
            ParentElement = parentElement;
            KeyDownHandlers += AffairsManager_KeyDown!;
            _undoOperations = new Stack<ICommandAf>();
            _redoOperations = new Stack<ICommandAf>();
        }
        private void LoadProfiles()
        {
            ProfileBox.Items.Clear();
            foreach (string profile in Directory.EnumerateFiles(Settings.listsDirectoryFullPath))
            {
                FileInfo profileInfo = new FileInfo(profile);
                ProfileBox.Items.Add(profileInfo.Name);
            }
            if (ProfileBox.Items.Count > 0)
            {
                if (!File.Exists(_settings.GetCurrentProfile())) _settings.SelectFirstProfile();
                FileInfo selectedProfile = new FileInfo(_settings.GetCurrentProfile());
                ProfileBox.SelectedIndex = ProfileBox.Items.IndexOf(selectedProfile.Name);
            }
        }
        private async Task LoadTextAsync()
        {
            Affairs.Items.Clear();
            if (_settings.CurrentListNotNull())
            {
                _lines = (await File.ReadAllLinesAsync(_settings.GetCurrentProfile()))
                    .OrderByDescending(x => x.EndsWith(_priorityTag)).AsParallel().ToList();
                string currentLine;
                foreach (string line in _lines)
                {
                    currentLine = line.Trim();
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
            }
            if (Affairs.Items.Count > 0)
            {
                Affairs.SelectedIndex = _selectedAffair;
            }
        }

        public async void AffairsManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == _addAffairKey)
            {
                CreateNExecuteAddAffairCommand();
            }
            else if (e.KeyCode == _deleteAffairKey)
            {
                CreateNExecuteDeleteAffairCommand();
            }
            else if (e.Control)
            {
                if (e.KeyCode == Keys.Z)
                {
                    Undo();
                }
                else if (e.KeyCode == Keys.Y)
                {
                    Redo();
                }
                else if (e.KeyCode == Keys.C && !AffairInput.Focused)
                {
                    CopySelectedAffair();
                }
            }
            else if (!AffairInput.Focused)
            {
                if (e.KeyCode.ToString().Length == 1)
                {
                    AffairInput.AppendText(e.KeyCode.ToString().ToUpper());
                }
                AffairInput.Focus();
            }
        }
        private void CopySelectedAffair()
        {
            if (Affairs.SelectedIndex == -1)
            {
                MessageBox.Show("Nothing to copy");
                return;
            }
            MessageBox.Show("The affair is copied");
            string selectedAffair = Affairs.SelectedItem!.ToString()!;
            Clipboard.SetText(selectedAffair.Remove(selectedAffair.Length - 1));
        }
        private void CreateNExecuteAddAffairCommand()
        {
            string affair = AffairInput.Text;
            if (string.IsNullOrEmpty(affair))
            {
                return;
            }
            _undoOperations.Push(new AddAffairCommand(this, affair));
            _undoOperations.Peek().Execute();
        }
        private void CreateNExecuteDeleteAffairCommand()
        {
            string affair = Affairs.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrEmpty(affair))
            {
                return;
            }
            _undoOperations.Push(
                new DeleteAffairCommand(this, affair));
            _undoOperations.Peek().Execute();
        }
        private void Undo()
        {
            if (_undoOperations.Count > 0)
            {
                _redoOperations.Push(_undoOperations.Peek());
                _undoOperations.Pop().Undo();
            }
        }

        private void Redo()
        {
            if (_redoOperations.Count > 0)
            {
                _redoOperations.Pop().Redo();
            }
        }
        private void CloseButton_Click(object sender, EventArgs e)
            => ParentElement.Exit();

        private void AffairsLab_MouseDown(object sender, MouseEventArgs e)
            => ParentElement.SetLastPoint(e);

        private void AffairsLab_MouseMove(object sender, MouseEventArgs e)
            => ParentElement.MoveForm(e);

        private void NameBackground_MouseDown(object sender, MouseEventArgs e)
            => ParentElement.SetLastPoint(e);

        private void NameBackground_MouseMove(object sender, MouseEventArgs e)
            => ParentElement.MoveForm(e);

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Gray;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Black;
        }
        /// <summary>
        /// Adds an affair and returns its
        /// </summary>
        /// <param name="affair"></param>
        /// <param name="clearInputLine"></param>
        /// <returns></returns>
        public async Task AddAffairAsync(string affair, bool clearInputLine = true)
        {
            affair = affair.Trim();
            if (affair == "")
            {
                MessageBox.Show("The input line is null");
                return;
            }
            if (ContainKeyWords(affair)) return;

            string inputText = CapitalizeAffair(affair) + ".";

            Task appendingText = AppendTextAsync(inputText + "\n");

            Affairs.Items.Add(inputText);
            Affairs.SelectedIndex = Affairs.Items.Count - 1;

            _lines.Add(inputText);

            if (clearInputLine) AffairInput.Text = "";
            await appendingText;
        }
        public async Task DeleteAffairAsync(string affair)
        {
            int affairIndex = Affairs.Items.IndexOf(affair);
            if (affairIndex == -1)
            {
                MessageBox.Show("Nothing to delete");
                return;
            }

            if (_settings.DoesAskToDelete())
            {
                DialogResult dialogres = MessageBox.Show("Do you want to delete the affair?",
                    "Confirm form",
                    MessageBoxButtons.YesNo);
                if (dialogres == DialogResult.No) return;
            }

            for (int i = 0; i < _lines.Count; i++)
            {
                if (_lines[i].Replace(_deadlineTag, "")
                        .Replace(_priorityTag, " " + _priorityWord) == affair)
                {
                    _lines.RemoveAt(i);
                    break;
                }
            }

            Task savingText = SaveTextAsync(_lines);

            int selectedIndex = Affairs.SelectedIndex;

            if (affairIndex <= Affairs.SelectedIndex)
            {
                if (Affairs.SelectedIndex == 0 && Affairs.Items.Count > 1)
                    Affairs.SelectedIndex = 1;
                else if (Affairs.Items.Count > 1) Affairs.SelectedIndex--;
            }

            Affairs.Items.RemoveAt(affairIndex);
            await savingText;
        }
        private async void AddAffairButton_Click(object sender, EventArgs e)
            => CreateNExecuteAddAffairCommand();
        private async void DeleteButton_Click(object sender, EventArgs e)
            => CreateNExecuteDeleteAffairCommand();
        private void ClearButton_Click(object sender, EventArgs e)
            => AffairInput.Clear();

        private void BackButton_Click(object sender, EventArgs e)
            => ParentElement.Return();

        private async void AddDeadlineButton_Click(object sender, EventArgs e)
        {
            await ManageDeadline();
        }
        private async Task ManageDeadline()
        {
            if (Affairs.SelectedIndex == -1)
            {
                MessageBox.Show("There is no affair selected");
                return;
            }

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
            await RenameAffair();
        }
        private async Task RenameAffair()
        {
            if (Affairs.SelectedIndex == -1)
            {
                MessageBox.Show("Nothing to rename");
                return;
            }

            string selectedWord = _lines[Affairs.SelectedIndex];
            string affair = selectedWord;

            if (selectedWord.StartsWith(_deadlineTag))
            {
                affair = affair.Substring(_deadlineDateNTagLength);
            }
            if (selectedWord.EndsWith(_priorityTag))
            {
                affair = affair.Substring(0, affair.Length - _priorityTag.Length - 1);
            }
            affair = affair.Remove(affair.Length - 1); // Убираем точку в конце, не блядско-нейроночный комментарий

            string renaming = Interaction
                .InputBox("Enter renaming", "Input box", affair);

            if (ContainKeyWords(renaming)) return;
            if (string.IsNullOrEmpty(renaming)) return;

            renaming = CapitalizeAffair(renaming);

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
            if (Affairs.SelectedIndex == -1)
            {
                MessageBox.Show("There is no affair selected");
                return;
            }

            if (_lines[Affairs.SelectedIndex].EndsWith(_priorityTag))
            {

                _lines[Affairs.SelectedIndex] = _lines[Affairs.SelectedIndex]
                    .Remove(_lines[Affairs.SelectedIndex].Length - (" " + _priorityTag).Length);
            }
            else
            {
                _lines[Affairs.SelectedIndex] += " " + _priorityTag;
            }

            await SaveTextAsync(_lines);
            await LoadTextAsync();
        }

        private string CapitalizeAffair(string affair)
        {
            return char.ToUpper(affair[0]) + affair[1..];
        }
        private void Affairs_MouseDown(object sender, MouseEventArgs e)
        {
            DragAffair();
        }
        private void DragAffair()
        {
            if (!_isDragging)
            {
                _isDragging = true;
                _currentDragIndex = Affairs.SelectedIndex;
            }
        }

        private async void Affairs_MouseUp(object sender, MouseEventArgs e)
        {
            await SwitchAffairs();
        }
        private async Task SwitchAffairs()
        {
            if (Affairs.SelectedIndex == -1)
            {
                MessageBox.Show("There is no affair selected");
                return;
            }
            bool newPlacePriority = _lines[Affairs.SelectedIndex].EndsWith(_priorityTag);
            bool oldPlacePriority = _lines[_currentDragIndex].EndsWith(_priorityTag);

            if (newPlacePriority != oldPlacePriority)
            {
                _isDragging = false;
                return;
            }

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
            if (new FileInfo(_settings.GetCurrentProfile()).Name ==
                new FileInfo(ProfileBox.SelectedItem!.ToString()!).Name) return;
            foreach (var profile in Directory.EnumerateFiles(Settings.listsDirectoryFullPath))
            {
                FileInfo profileInfo = new FileInfo(profile);
                if (profileInfo.Name == ProfileBox.SelectedItem!.ToString())
                {
                    _settings.SetCurrentProfile(profileInfo.FullName);
                    await _settings.SaveSettingsAsync();
                    if (Affairs.Items.Count > 0)
                    {
                        Affairs.SelectedIndex = 0;
                        _selectedAffair = 0;
                    }
                    return;
                }
            }
        }
        private async void ProfileBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            await ChangeProfileAsync();
            await LoadTextAsync();
        }
        private async Task SaveTextAsync(List<string> lines)
        {
            await File.WriteAllLinesAsync(_settings.GetCurrentProfile(), lines);
        }
        private async Task AppendTextAsync(string line)
        {
            await File.AppendAllTextAsync(_settings.GetCurrentProfile(), line);
        }
        public async void OnAddition()
        {
            LoadProfiles();
            await LoadTextAsync();

            _selectedAffair = 0;
            Affairs.SelectedIndex = 0;
        }

        public bool OnRemoving(bool closing = false) => true;

        private void Affairs_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedAffair = Affairs.SelectedIndex;
        }
    }
}

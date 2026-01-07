using AffairList.Classes.Commands.AffairsManagerCommands;
using AffairList.Classes.Factories;
using AffairList.Enums;
using AffairList.Interfaces;
using Microsoft.VisualBasic;
using static System.Runtime.CompilerServices.RuntimeHelpers;

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

        public event Action<string> affairAdded;

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
            Affairs.BeginUpdate();
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
                        currentLine += _priorityWord;
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
                if (_selectedAffair == -1) _selectedAffair = 0;
                Affairs.SelectedIndex = _selectedAffair;
            }
            Affairs.EndUpdate();
        }

        public async void AffairsManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == _addAffairKey)
            {
                await ExecuteAddAffairCommandAsync(AffairInput.Text);
            }
            else if (e.KeyCode == _deleteAffairKey)
            {
                await ExecuteDeleteAffairCommandAsync(Affairs.SelectedItem?.ToString() ?? "");
            }
            else if (e.Control)
            {
                if (e.KeyCode == Keys.Z)
                {
                    await UndoAsync();
                }
                else if (e.KeyCode == Keys.Y)
                {
                    await RedoAsync();
                }
                else if (e.KeyCode == Keys.C && !AffairInput.Focused)
                {
                    CopySelectedAffair();
                }
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Down || e.KeyCode == Keys.Right
                || e.KeyCode == Keys.Up)
            {
                Affairs.Focus();
            }
            else if (!AffairInput.Focused)
            {
                if (e.KeyCode.ToString().Length == 1)
                {
                    AffairInput.AppendText(e.KeyCode.ToString());
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
        private async Task<int> ExecuteAddAffairCommandAsync(string affair)
        {
            IAsyncCommandAf addAffairCommand = CommandFactory.CreateAddAffairCommand(this, affair);
            int result = await addAffairCommand.ExecuteAsync();
            if (result != (int)MethodResults.Success)
                return (int)MethodResults.NothingHappened;
            _undoOperations.Push(addAffairCommand);
            return result;
        }
        private async Task<int> ExecuteDeleteAffairCommandAsync(string affair)
        {
            IAsyncCommandAf deleteAffairCommand = 
                CommandFactory.CreateDeleteAffairCommand(this, affair);
            int result = await deleteAffairCommand.ExecuteAsync();
            if (result != (int)MethodResults.Success)
                return (int)MethodResults.NothingHappened;
            _undoOperations.Push(deleteAffairCommand);
            return result;
        }
        private async Task<int> UndoAsync()
        {
            if (_undoOperations.Count > 0)
            {
                ICommandAf command = _undoOperations.Pop();
                int result = 0;
                if (command is IAsyncCommandAf commandAsync)
                {
                    result = await commandAsync.UndoAsync();
                    if (result != (int)MethodResults.Success)
                        return (int)MethodResults.NothingHappened;
                    _redoOperations.Push(command);
                    return result;
                }

                result = command.Undo();

                if (result != (int)MethodResults.Success)
                    return (int)MethodResults.NothingHappened;
                _redoOperations.Push(command);
                return result;
            }
            return (int)MethodResults.NothingHappened;
        }

        private async Task<int> RedoAsync()
        {
            if (_redoOperations.Count > 0)
            {
                ICommandAf command = _redoOperations.Pop();
                int result = 0;
                if (command is IAsyncCommandAf commandAsync)
                {
                    result = await commandAsync.RedoAsync();
                    if (result != (int)MethodResults.Success)
                        return (int)MethodResults.NothingHappened;
                    return result;
                }

                result = command.Redo();
                if (result != (int)MethodResults.Success)
                    return (int)MethodResults.NothingHappened;
                return result;
            }
            return (int)MethodResults.NothingHappened;
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
        public async Task<int> AddAffairAsync(string affair, bool clearInputLine = true)
        {
            affair = affair.Trim();
            if (affair == "")
            {
                return (int)MethodResults.Error;
            }
            if (ContainKeyWords(affair)) return (int)MethodResults.Error;

            string inputText = CapitalizeAffair(affair);

            Task appendingText = AppendTextAsync(inputText + "\n");

            Affairs.Items.Add(inputText);
            Affairs.SelectedIndex = Affairs.Items.Count - 1;

            _lines.Add(inputText);

            if (clearInputLine) AffairInput.Text = "";
            await appendingText;
            affairAdded?.Invoke(inputText);
            return (int)MethodResults.Success;
        }
        public async Task<int> DeleteAffairAsync(string affair)
        {
            int affairIndex = Affairs.Items.IndexOf(affair); 
            if (affairIndex == -1)
            {
                MessageBox.Show("Nothing to delete");
                return (int)MethodResults.Error;
            }

            if (_settings.DoesAskToDelete())
            {
                DialogResult dialogres = MessageBox.Show("Do you want to delete the affair?",
                    "Confirm form",
                    MessageBoxButtons.YesNo);
                if (dialogres == DialogResult.No) return (int)MethodResults.Error;
            }

            for (int i = 0; i < _lines.Count; i++)
            {
                if (_lines[i].Replace(_deadlineTag, "")
                        .Replace(_priorityTag, _priorityWord) == affair)
                {
                    _lines.RemoveAt(i);
                    break;
                }
            }

            await SaveTextAsync(_lines);

            try
            {

                if (Affairs.SelectedIndex == 0 && Affairs.Items.Count > 1)
                    Affairs.SetSelected(affairIndex + 1, true);
                else if (Affairs.Items.Count > 1)
                    Affairs.SetSelected(affairIndex - 1, true);
            }
            catch {}

            Affairs.Items.RemoveAt(affairIndex);

            return (int)MethodResults.Success;
        }
        private async void AddAffairButton_Click(object sender, EventArgs e)
            => await ExecuteAddAffairCommandAsync(AffairInput.Text);
        private async void DeleteButton_Click(object sender, EventArgs e)
            => await ExecuteDeleteAffairCommandAsync(Affairs.SelectedItem?.ToString() ?? "");
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
            if (affair.Length < 1) return "";
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
            _selectedAffair = -1;
            LoadProfiles();
            await LoadTextAsync();
        }

        public bool OnRemoving(bool closing = false) => true;

        private void Affairs_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedAffair = Affairs.SelectedIndex;
        }
    }
}

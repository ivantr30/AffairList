using AffairList.Classes.Factories;
using AffairList.Constants;
using AffairList.Enums;
using AffairList.Services.Managers;
using AffairList.Services.Models;
using AffairList.Services.Providers;
using Microsoft.VisualBasic;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AffairList
{
    public partial class AffairsManager : UserControl, IChildable, IKeyPreviewable
    {
        private string _lastLoadedProfileFullPath = string.Empty;

        private int _currentDragIndex = 0;
        private int _selectedAffairIndex = 0;

        private AffairsCollection _affairsCollection;

        private bool _isDragging = false;

        private Keys _addAffairKey = Keys.Enter;
        private Keys _deleteAffairKey = Keys.Delete;

        private Settings _settings;

        #region Interface variables
        public IParentable ParentElement { get; private set; }
        public KeyEventHandler KeyDownHandlers { get; private set; }
        public KeyPressEventHandler KeyPressHandlers { get; private set; }
        public KeyEventHandler KeyUpHandlers { get; private set; }
        #endregion 

        private CommandManager _commandManager;

        private TaskDialogPage _taskDialog;

        public AffairsManager(Settings settings, IParentable parentElement)
        {
            InitializeComponent();
            _settings = settings;
            ParentElement = parentElement;
            KeyDownHandlers += AffairsManager_KeyDown!;
            _commandManager = new CommandManager();
            _taskDialog = new TaskDialogPage();
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
                if (!File.Exists(_settings.Data.CurrentProfileFullPath)) _settings.SelectFirstProfile();
                FileInfo selectedProfile = new FileInfo(_settings.Data.CurrentProfileFullPath);
                ProfileBox.SelectedIndex = ProfileBox.Items.IndexOf(selectedProfile.Name);
            }
        }
        private async Task LoadTextAsync()
        {
            if(_lastLoadedProfileFullPath != string.Empty &&
               _settings.Data.CurrentProfileFullPath == _lastLoadedProfileFullPath)
            {
               return;
            }
            Affairs.Items.Clear();
            Affairs.BeginUpdate();
            if (_settings.CurrentListExists())
            {
                try
                {
                    _affairsCollection = await AffairsProvider.GetAffairsAsync(_settings.Data.CurrentProfileFullPath);
                }
                catch (JsonException)
                {
                    _affairsCollection = await AffairsProvider.GetAffairsLegacyAsync(_settings.Data.CurrentProfileFullPath);
                    await AffairsProvider.SaveAffairsAsync(_settings.Data.CurrentProfileFullPath, _affairsCollection);
                }

                SortAffairs();

                for (int i = 0; i < _affairsCollection.Affairs.Count; i++)
                {
                    Affairs.Items.Add(_affairsCollection.Affairs[i].ToString());
                }
            }
            if (Affairs.Items.Count > 0)
            {
                if (_selectedAffairIndex >= _affairsCollection.Affairs.Capacity) _selectedAffairIndex = 0;
                Affairs.SelectedIndex = _selectedAffairIndex;
            }
            else _selectedAffairIndex = -1;
            Affairs.EndUpdate();
            _lastLoadedProfileFullPath = _settings.Data.CurrentProfileFullPath;
        }
        private void SortAffairs()
        {
            _affairsCollection.Affairs = _affairsCollection.Affairs.OrderByDescending(x => x.IsPrioritized).ToList();
        }
        private void RefreshAffairsUI()
        {
            Affairs.BeginUpdate();
            Affairs.Items.Clear();
            foreach (var affair in _affairsCollection.Affairs)
            {
                Affairs.Items.Add(affair.ToString());
            }
            if (Affairs.Items.Count > 0) Affairs.SelectedIndex = _selectedAffairIndex;
            Affairs.EndUpdate();
        }

        public async void AffairsManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == _addAffairKey)
            {
                await _commandManager.ExecuteAsync(CommandFactory.CreateAddAffairCommand(this, AffairInput.Text));
            }
            else if (e.KeyCode == _deleteAffairKey)
            {
                if (_selectedAffairIndex == -1)
                {
                    AffairDoesNotExistsException();
                    return;
                }
                await _commandManager
                    .ExecuteAsync(CommandFactory.CreateDeleteAffairCommand(this, _affairsCollection.Affairs[_selectedAffairIndex]));
            }
            else if (e.Control)
            {
                if (e.KeyCode == Keys.Z)
                {
                    await _commandManager.UndoAsync();
                }
                else if (e.KeyCode == Keys.Y)
                {
                    await _commandManager.RedoAsync();
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
                string keyString = e.KeyCode.ToString();
                if (keyString.Length == 1)
                {
                    AffairInput.AppendText(keyString);
                }
                AffairInput.Focus();
            }
        }
        private void CopySelectedAffair()
        {
            if (_selectedAffairIndex == -1)
            {
                ShowDialog("Nothing to copy", "Input error");
                return;
            }
            ShowDialog("The affair is copied", "Copy success");
            string selectedAffair = Affairs.SelectedItem!.ToString()!;
            Clipboard.SetText(selectedAffair);
        }
        public async Task<int> AddAffairAsync(Affair affair, bool clearInputLine = true)
        {
            affair.InnerText = affair.InnerText.Trim();

            if (string.IsNullOrEmpty(affair.InnerText))
            {
                ShowDialog("There is nothing to add to your affair list", "Input error");
                return (int)MethodResults.Error;
            }

            if (ContainKeyWords(affair.InnerText))
            {
                AffairContainsKeyWordsException();
                return (int)MethodResults.Error;
            }

            affair.InnerText = CapitalizeAffair(affair.InnerText);

            _affairsCollection.Affairs.Add(affair);

            Task appendingText = AffairsProvider.SaveAffairsAsync(_settings.Data.CurrentProfileFullPath, _affairsCollection);

            Affairs.Items.Add(affair.InnerText);
            Affairs.SelectedIndex = Affairs.Items.Count - 1;

            if (clearInputLine) AffairInput.Text = "";
            await appendingText;
            return (int)MethodResults.Success;
        }
        public async Task<int> DeleteAffairAsync(Affair affair)
        {
            int affairIndex = _affairsCollection.Affairs.IndexOf(affair);

            if (_settings.Data.AskToDelete)
            {
                var dialogRes = ShowDialog("Do you want to delete the affair?", MessageBoxButtons.YesNo, "Confirm form");
                if (dialogRes.Text == "No") return (int)MethodResults.NothingHappened;
            }

            if (!_affairsCollection.Affairs.Remove(affair))
                return (int)MethodResults.Error;

            await AffairsProvider.SaveAffairsAsync(_settings.Data.CurrentProfileFullPath, _affairsCollection);

            if (Affairs.SelectedIndex == 0 && Affairs.Items.Count > 1)
                Affairs.SetSelected(affairIndex + 1, true);
            else if (Affairs.Items.Count > 1)
                Affairs.SetSelected(affairIndex - 1, true);

            Affairs.Items.RemoveAt(affairIndex);

            return (int)MethodResults.Success;
        }
        private async void AddAffairButton_Click(object sender, EventArgs e)
            => await _commandManager
            .ExecuteAsync(CommandFactory.CreateAddAffairCommand(this, AffairInput.Text));
        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_selectedAffairIndex == -1)
            {
                AffairDoesNotExistsException();
                return;
            }
            await _commandManager
            .ExecuteAsync(CommandFactory.CreateDeleteAffairCommand(this, _affairsCollection.Affairs[_selectedAffairIndex]));
        }
        private void ClearButton_Click(object sender, EventArgs e)
            => AffairInput.Clear();

        private void BackButton_Click(object sender, EventArgs e)
            => ParentElement.ReturnAsync();

        private async void AddDeadlineButton_Click(object sender, EventArgs e)
        {
            if (_selectedAffairIndex == -1)
            {
                AffairDoesNotExistsException();
                return;
            }
            await _commandManager
                .ExecuteAsync(
                await CommandFactory.CreatManageAffairDeadlineCommandAsync(this, _affairsCollection.Affairs[_selectedAffairIndex]));
        }
        public async Task<DeadlineActions> DetermineDeadlineActionAsync(Affair affair)
        {
            if (affair.Deadline == null) return DeadlineActions.Add;

            DeadlineActions action = DeadlineActions.None;
            var updateDeadlineButton = new TaskDialogButton("Update");
            var deleteDeadlineButton = new TaskDialogButton("Delete");
            var cancelButton = new TaskDialogButton("Cancel");

            var actionInputDialog = new TaskDialogPage()
            {
                Text = "Do you want to delete the deadline or just change it?",
                Caption = "Action for the existing deadline",
                Buttons = { updateDeadlineButton, deleteDeadlineButton, cancelButton },
                
            };

            TaskDialogButton userChoice = TaskDialog.ShowDialog(this, actionInputDialog);

            if (userChoice == cancelButton) return action;

            else if (userChoice == updateDeadlineButton)
            {
                action = DeadlineActions.Update;
            }

            else if (userChoice == deleteDeadlineButton)
            {
                action = DeadlineActions.Delete;
            }
            return action;
        } 
        public async Task<DateOnly?> UpdateDeadlineAsync(Affair affair, DateOnly? deadline = null)
        {
            DateOnly? oldDeadline = affair.Deadline;
            int affairIndex = _affairsCollection.Affairs.IndexOf(affair);

            if(deadline != null)
            {
                affair.Deadline = deadline;
            }
            else
            {
                affair.Deadline = GetDeadline();
            }

            Affairs.Items[affairIndex] = affair.ToString();

            await AffairsProvider.SaveAffairsAsync(_settings.Data.CurrentProfileFullPath, _affairsCollection);
            return oldDeadline;
        }
        public async Task<DateOnly?> DeleteDeadlineAsync(Affair affair)
        {
            int affairIndex = _affairsCollection.Affairs.IndexOf(affair);

            DateOnly? deadline = affair.Deadline;

            affair.Deadline = null;

            Affairs.Items[affairIndex] = affair.ToString();

            await AffairsProvider.SaveAffairsAsync(_settings.Data.CurrentProfileFullPath, _affairsCollection);
            return deadline;
        }
        public async Task<DateOnly?> AddDeadlineAsync(Affair affair, DateOnly? deadline = null)
        {
            int affairIndex = _affairsCollection.Affairs.IndexOf(affair);

            if(deadline != null)
            {
                affair.Deadline = deadline;
            }
            else
            {
                affair.Deadline = GetDeadline();
            }

            Affairs.Items[affairIndex] = affair.ToString();

            await AffairsProvider.SaveAffairsAsync(_settings.Data.CurrentProfileFullPath, _affairsCollection);
            return deadline;
        }
        private DateOnly? GetDeadline()
        {
            DateOnly? deadline = null;
            InputDeadlineForm inputDeadline = new InputDeadlineForm();
            inputDeadline.OnConfirm += delegate
            {
                deadline = inputDeadline.deadline;
            };
            inputDeadline.ShowDialog();
            return deadline;
        }
        private async void RenameAffairButton_Click(object sender, EventArgs e)
        {
            if (_selectedAffairIndex == -1)
            {
                AffairDoesNotExistsException();
                return;
            }
            var renameAffairCommand = CommandFactory.CreateRenameAffairCommand(this, 
                _affairsCollection.Affairs[_selectedAffairIndex]);
            await _commandManager.ExecuteAsync(renameAffairCommand);
        }
        public async Task RenameAffairAsync(Affair affair)
        {
            int affairIndex = _affairsCollection.Affairs.IndexOf(affair);

            string renaming = Interaction.InputBox("Enter renaming", "Input box", affair.InnerText);

            if (ContainKeyWords(renaming))
            {
                AffairContainsKeyWordsException();
                return;
            }

            if (string.IsNullOrEmpty(renaming)) return;

            renaming = CapitalizeAffair(renaming).Trim();

            affair.InnerText = renaming;

            Affairs.Items[affairIndex] = affair.ToString();

            _affairsCollection.Affairs[affairIndex] = affair;

            await AffairsProvider.SaveAffairsAsync(_settings.Data.CurrentProfileFullPath, _affairsCollection);
        }

        private async void PriorityButton_Click(object sender, EventArgs e)
        {
            if (_selectedAffairIndex == -1)
            {
                AffairDoesNotExistsException();
                return;
            }
            var TogglePriorityCommand = CommandFactory.CreatToggleAffairPriorityCommand(
                this, 
                _affairsCollection.Affairs[_selectedAffairIndex]);

            await _commandManager.ExecuteAsync(TogglePriorityCommand);
        }
        public async Task<int> ToggleAffairPriority(Affair affair)
        {
            int affairIndex = _affairsCollection.Affairs.IndexOf(affair);
            if (affairIndex == -1)
            {
                MessageBox.Show("There is no affair selected");
                return (int)MethodResults.Error;
            }

            affair.IsPrioritized = !affair.IsPrioritized;

            Affairs.Items[affairIndex] = affair.ToString();

            await AffairsProvider.SaveAffairsAsync(_settings.Data.CurrentProfileFullPath, _affairsCollection);
            SortAffairs();
            RefreshAffairsUI();
            return (int)MethodResults.Success;
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
            if (_selectedAffairIndex == -1 || _currentDragIndex == -1)
            {
                AffairDoesNotExistsException();
                return;
            }
            var commandAsync = CommandFactory.CreatSwitchAffairCommand(this,
                _affairsCollection.Affairs[_selectedAffairIndex],
                _affairsCollection.Affairs[_currentDragIndex]);

            await _commandManager.ExecuteAsync(commandAsync);
        }
        public async Task<int> SwitchAffairsAsync(Affair firstAffair, Affair secondAffair)
        {
            int firstAffairIndex = _affairsCollection.Affairs.IndexOf(firstAffair);
            int secondAffairIndex = _affairsCollection.Affairs.IndexOf(secondAffair);

            bool newPlacePriority = _affairsCollection.Affairs[firstAffairIndex].IsPrioritized;
            bool oldPlacePriority = _affairsCollection.Affairs[secondAffairIndex].IsPrioritized;

            if (newPlacePriority != oldPlacePriority)
            {
                _isDragging = false;
                return (int)MethodResults.Error;
            }

            // Не менять, уже нечего
            string switcher = _affairsCollection.Affairs[firstAffairIndex].InnerText;
            _affairsCollection.Affairs[firstAffairIndex].InnerText = _affairsCollection.Affairs[secondAffairIndex].InnerText;
            _affairsCollection.Affairs[secondAffairIndex].InnerText = switcher;

            Task savingText = AffairsProvider.SaveAffairsAsync(_settings.Data.CurrentProfileFullPath, _affairsCollection);

            switcher = (string)Affairs.Items[firstAffairIndex];
            Affairs.Items[firstAffairIndex] = Affairs.Items[secondAffairIndex];
            Affairs.Items[secondAffairIndex] = switcher;

            _isDragging = false;
            await savingText;
            return (int)MethodResults.Success;
        }
        private async Task ChangeProfileAsync()
        {
            string newSelectedProfilePath = Path.Combine(Settings.listsDirectoryFullPath, ProfileBox.SelectedItem.ToString());

            if (_settings.Data.CurrentProfileFullPath == newSelectedProfilePath) return;

            _settings.Data.CurrentProfileFullPath = newSelectedProfilePath;

            await _settings.SaveSettingsAsync();
        }
        public async Task UpdateAffairInnerText(Affair affair, string innerText)
        {
            int affairIndex = _affairsCollection.Affairs.IndexOf(affair);
            if (affairIndex == -1) return;

            _affairsCollection.Affairs[affairIndex].InnerText = innerText;
            Affairs.Items[affairIndex] = _affairsCollection.Affairs[affairIndex].ToString();

            await AffairsProvider.SaveAffairsAsync(_settings.Data.CurrentProfileFullPath, _affairsCollection);
        }
        private async void ProfileBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            await ChangeProfileAsync();
            await LoadTextAsync();
        }
        public async Task OnAdditionAsync()
        {
            SuspendLayout();
            LoadProfiles();
            await LoadTextAsync();
            ResumeLayout();
        }

        public async Task<bool> OnRemovingAsync(bool closing = false) => true;
        private string CapitalizeAffair(string affair)
        {
            if (affair.Length < 1) return "";
            return char.ToUpper(affair[0]) + affair[1..];
        }
        private bool ContainKeyWords(string word)
        {
            if (word.Contains(AffairConstants.DeadlineTag)
                || word.Contains(AffairConstants.PriorityTag)
                || word.Contains(AffairConstants.PriorityWord))
                return true;

            return false;
        }
        private void AffairContainsKeyWordsException()
        {
            ShowDialog(
                $"Error, you word is perhibited to contain - {AffairConstants.DeadlineTag}, {AffairConstants.PriorityTag}, {AffairConstants.PriorityWord}",
                "Input error");
        }
        private void AffairDoesNotExistsException()
        {
            ShowDialog("There is no affair present to work on", "Input error");
        }
        private TaskDialogButton ShowDialog(string information, MessageBoxButtons buttons, string caption = "")
        {
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    TaskDialogButton buttonOk = new TaskDialogButton("Ok");
                    _taskDialog.Buttons.Add(buttonOk);
                    break;
                case MessageBoxButtons.YesNo:
                    TaskDialogButton buttonYes = new TaskDialogButton("Yes");
                    TaskDialogButton buttonNo = new TaskDialogButton("No");
                    _taskDialog.Buttons.Add(buttonYes);
                    _taskDialog.Buttons.Add(buttonNo);
                    break;
                default:
                    break;
            }

            return ShowDialog(information, caption);
        }
        private TaskDialogButton ShowDialog(string information, string caption = "", params TaskDialogButton[] buttons)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                _taskDialog.Buttons.Add(buttons[i]);    
            }

            return ShowDialog(information, caption);
        }
        private TaskDialogButton ShowDialog(string information, string caption = "")
        {
            _taskDialog.Text = information;
            if (!string.IsNullOrEmpty(caption))
            {
                _taskDialog.Caption = caption;
            }
            var dialogResult = TaskDialog.ShowDialog(this, _taskDialog);
            _taskDialog.Text = "";
            _taskDialog.Caption = "";
            _taskDialog.Buttons.Clear();
            return dialogResult;
        }

        private void Affairs_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedAffairIndex = Affairs.SelectedIndex;
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

        private void MinimizeButton_Click(object sender, EventArgs e) => ParentElement.MinimizeForm();

        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Gray;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Black;
        }
    }
}

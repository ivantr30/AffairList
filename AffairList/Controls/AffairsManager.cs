using AffairList.Classes.Factories;
using AffairList.Enums;
using Microsoft.VisualBasic;
using AffairList.Services.Models;
using AffairList.Services.Providers;
using AffairList.Services.Managers;
using AffairList.Constants;
using System.Text.Json;

namespace AffairList
{
    public partial class AffairsManager : UserControl, IChildable, IKeyPreviewable
    {
        private int _currentDragIndex = 0;
        private int _selectedAffairIndex = 0;

        private AffairsCollection _affairsCollection;

        private bool _isDragging = false;

        private Keys _addAffairKey = Keys.Enter;
        private Keys _deleteAffairKey = Keys.Delete;

        private Settings _settings;
        public IParentable ParentElement { get; private set; }
        public KeyEventHandler KeyDownHandlers { get; private set; }
        public KeyPressEventHandler KeyPressHandlers { get; private set; }
        public KeyEventHandler KeyUpHandlers { get; private set; }

        private CommandManager _commandManager;

        public AffairsManager(Settings settings, IParentable parentElement)
        {
            InitializeComponent();
            _settings = settings;
            ParentElement = parentElement;
            KeyDownHandlers += AffairsManager_KeyDown!;
            _commandManager = new CommandManager();
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
                if (_selectedAffairIndex == -1) _selectedAffairIndex = 0;
                Affairs.SelectedIndex = _selectedAffairIndex;
            }
            Affairs.EndUpdate();
        }
        private void SortAffairs()
        {
            _affairsCollection.Affairs = _affairsCollection.Affairs.OrderByDescending(x => x.IsPrioritized).ToList();
        }
        private void RefreshAffairsList()
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
        public async Task<int> AddAffairAsync(Affair affair, bool clearInputLine = true)
        {
            affair.InnerText = affair.InnerText.Trim();
            if (string.IsNullOrEmpty(affair.InnerText))
            {
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
            if (!_affairsCollection.Affairs.Contains(affair))
            {
                MessageBox.Show("Nothing to delete");
                return (int)MethodResults.Error;
            }

            if (_settings.Data.AskToDelete)
            {
                DialogResult dialogres = MessageBox.Show("Do you want to delete the affair?",
                    "Confirm form",
                    MessageBoxButtons.YesNo);
                if (dialogres == DialogResult.No) return (int)MethodResults.NothingHappened;
            }

            int affairIndex = _affairsCollection.Affairs.IndexOf(affair);

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
            => await _commandManager
            .ExecuteAsync(CommandFactory.CreateDeleteAffairCommand(this, _affairsCollection.Affairs[_selectedAffairIndex]));
        private void ClearButton_Click(object sender, EventArgs e)
            => AffairInput.Clear();

        private void BackButton_Click(object sender, EventArgs e)
            => ParentElement.ReturnAsync();

        private async void AddDeadlineButton_Click(object sender, EventArgs e)
        {
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
            if (affairIndex == -1)
            {
                MessageBox.Show("There is no affair to work on");
                return null;
            }
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

            if (affairIndex == -1)
            {
                MessageBox.Show("There is no affair to work on");
                return null;
            }

            DateOnly? deadline = affair.Deadline;

            affair.Deadline = null;

            Affairs.Items[affairIndex] = affair.ToString();

            await AffairsProvider.SaveAffairsAsync(_settings.Data.CurrentProfileFullPath, _affairsCollection);
            return deadline;
        }
        public async Task<DateOnly?> AddDeadlineAsync(Affair affair, DateOnly? deadline = null)
        {
            int affairIndex = _affairsCollection.Affairs.IndexOf(affair);

            if (affairIndex == -1)
            {
                MessageBox.Show("There is no affair to work on");
                return null;
            }
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
            var renameAffairCommand = CommandFactory.CreateRenameAffairCommand(this, _affairsCollection.Affairs[_selectedAffairIndex]);
            await _commandManager.ExecuteAsync(renameAffairCommand);
        }
        public async Task RenameAffairAsync(Affair affair)
        {
            int affairIndex = _affairsCollection.Affairs.IndexOf(affair);

            if (affairIndex == -1)
            {
                MessageBox.Show("There is no affair selected");
                return;
            }

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
        private bool ContainKeyWords(string word)
        {
            if (   word.Contains(AffairConstants.DeadlineTag)
                || word.Contains(AffairConstants.PriorityTag)
                || word.Contains(AffairConstants.PriorityWord))
                return true;

            return false;
        }
        private void AffairContainsKeyWordsException()
        {
            MessageBox.Show("Error, you word is perhibited to contain - " +
                $"{AffairConstants.DeadlineTag}, {AffairConstants.PriorityTag}, {AffairConstants.PriorityWord}");
        }

        private async void PriorityButton_Click(object sender, EventArgs e)
        {
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
            RefreshAffairsList();
            return (int)MethodResults.Success;
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
            var commandAsync = CommandFactory.CreatSwitchAffairCommand(this,
                _affairsCollection.Affairs[_selectedAffairIndex],
                _affairsCollection.Affairs[_currentDragIndex]);

            await _commandManager.ExecuteAsync(commandAsync);
        }
        public async Task<int> SwitchAffairsAsync(Affair firstAffair, Affair secondAffair)
        {
            int firstAffairIndex = _affairsCollection.Affairs.IndexOf(firstAffair);
            int secondAffairIndex = _affairsCollection.Affairs.IndexOf(secondAffair);
            if (firstAffairIndex == -1 || secondAffairIndex == -1)
            {
                MessageBox.Show("There is no affair selected");
                return (int)MethodResults.Error;
            }
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
            if (new FileInfo(_settings.Data.CurrentProfileFullPath).Name ==
                new FileInfo(ProfileBox.SelectedItem!.ToString()!).Name) return;
            foreach (var profile in Directory.EnumerateFiles(Settings.listsDirectoryFullPath))
            {
                FileInfo profileInfo = new FileInfo(profile);
                if (profileInfo.Name == ProfileBox.SelectedItem!.ToString())
                {
                    _settings.Data.CurrentProfileFullPath = profileInfo.FullName;
                    await _settings.SaveSettingsAsync();
                    if (Affairs.Items.Count > 0)
                    {
                        Affairs.SelectedIndex = 0;
                        _selectedAffairIndex = 0;
                    }
                    return;
                }
            }
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
            _selectedAffairIndex = -1;
            LoadProfiles();
            await LoadTextAsync();
            ResumeLayout();
        }

        public async Task<bool> OnRemovingAsync(bool closing = false) => true;

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

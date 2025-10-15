using Microsoft.VisualBasic;
namespace AffairList
{
    public partial class ProfileManager : UserControl, IChildable, IKeyPreviewable
    {
        private List<string> _profileLines;

        private string _priorityWord = " Priority.txt";
        private string _priorityWordWithoudTxt = " Priority";
        private string _priorityTag = " \"Priority\"";

        private Settings _settings;

        public IParentable ParentElement { get; private set; }

        public KeyEventHandler KeyDownHandlers { get; private set; }

        public KeyPressEventHandler KeyPressHandlers { get; private set; }

        public KeyEventHandler KeyUpHandlers { get; private set; }

        public ProfileManager(Settings settings, IParentable parent)
        {
            InitializeComponent();
            _settings = settings;
            ParentElement = parent;
            KeyDownHandlers += ChangeProfileForm_KeyDown;
        }

        private void LoadProfiles()
        {
            if (!_settings.ListsDirectoryExists()) _settings.CreateListsDirectory();

            Profiles.Items.Clear();
            _profileLines = Directory.GetFiles(_settings.listsDirectoryFullPath)
                    .OrderByDescending(x => x.EndsWith(_priorityWord)).ToList();

            for (int i = 0; i < _profileLines.Count; i++)
            {
                FileInfo profile = new FileInfo(_profileLines[i]);
                Profiles.Items.Add(profile.Name);
            }
            if (Profiles.Items.Count > 0)
            {
                Profiles.SelectedItem = new FileInfo(_settings.GetCurrentProfile()).Name;
            }
        }
        private bool ContainKeyWords(string fileName)
        {
            if (fileName.Contains(_priorityTag))
            {
                MessageBox.Show($"Error, you list name is perhibited to contain - {_priorityTag}");
                return true;
            }
            return false;
        }
        private void MinimizeButton_Click(object sender, EventArgs e) 
            => ParentElement.MinimizeForm();

        private void BackButton_Click(object sender, EventArgs e) => ParentElement.Return();

        private void CloseButtonLab_Click(object sender, EventArgs e) => ParentElement.Exit();

        private void CloseButtonLab_MouseEnter(object sender, EventArgs e)
        {
            CloseButtonLab.ForeColor = (System.Drawing.Color)Color.Gray;
        }

        private void CloseButtonLab_MouseLeave(object sender, EventArgs e)
        {
            CloseButtonLab.ForeColor = (System.Drawing.Color)Color.Black;
        }

        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = (System.Drawing.Color)Color.Gray;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = (System.Drawing.Color)Color.Black;
        }

        private void NameBackground_MouseMove(object sender, MouseEventArgs e)
            => ParentElement.MoveForm(e);

        private void NameBackground_MouseDown(object sender, MouseEventArgs e) 
            => ParentElement.SetLastPoint(e);

        private void ProfilesLab_MouseDown(object sender, MouseEventArgs e) 
            => ParentElement.SetLastPoint(e);

        private void ProfilesLab_MouseMove(object sender, MouseEventArgs e) 
            => ParentElement.MoveForm(e);
        private void AddProfile(string profile)
        {
            if (profile.Trim() == "")
            {
                MessageBox.Show("Error, textbox is null");
                return;
            }
            // If input ended with .txt or without
            if (ProfileExists(profile))
            {
                MessageBox.Show("Error, this profile already exists");
                return;
            }

            if (ContainKeyWords(profile)) return;
            if (profile.EndsWith(".txt")) AddNewProfile(profile);
            else AddNewProfile(profile + ".txt");

            Profiles.SelectedIndex = Profiles.Items.Count - 1;

            ProfileInput.Clear();
        }
        private void AddNewProfile(string profile)
        {
            Profiles.Items.Add(profile);
            _profileLines.Add($@"{_settings.listsDirectoryFullPath}\{profile}");

            using (File.Create($@"{_settings.listsDirectoryFullPath}\{profile}")) { }
        }
        /// <summary>
        /// Requires only name with extension
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private bool ProfileExists(string profileName)
        {
            return File.Exists(_settings.listsDirectoryFullPath + profileName + ".txt")
                || File.Exists(_settings.listsDirectoryFullPath + profileName);
        }
        private void DeleteProfile(string profile)
        {
            if (Profiles.SelectedIndex == -1) return;

            DialogResult dialogres = MessageBox.Show("Do you want to delete the profile?",
                "Confirm form",
                MessageBoxButtons.YesNo);
            if (dialogres == DialogResult.No) return;

            // It is without / because lists directory path ends with / (look into Settings.cs)
            File.Delete($@"{_settings.listsDirectoryFullPath}{profile}");

            if(profile.EndsWith(".txt")) 
                _profileLines.Remove(_profileLines.Where(x => x.EndsWith(profile)).First());
            else
                _profileLines.Remove(_profileLines.Where(x => x.EndsWith(profile + ".txt")).First());

            int selectedIndex = Profiles.Items.IndexOf(profile);
            if(Profiles.Items.Count > 1)
            {
                if(Profiles.SelectedIndex == 0)
                {
                    Profiles.SelectedIndex++;
                    selectedIndex = Profiles.SelectedIndex - 1;
                }
                else
                {
                    Profiles.SelectedIndex--;
                    selectedIndex = Profiles.SelectedIndex + 1;

                }
            }
            Profiles.Items.RemoveAt(selectedIndex);
        }

        private void AddButton_Click(object sender, EventArgs e) => AddProfile(ProfileInput.Text);

        private void DeleteButton_Click(object sender, EventArgs e) 
            => DeleteProfile(Profiles.SelectedItem.ToString());

        private void ClearButton_Click(object sender, EventArgs e) => ProfileInput.Clear();

        private async void ChangeProfileForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddProfile(ProfileInput.Text);
            }
            if (e.KeyCode == Keys.Delete)
            {
                DeleteProfile(Profiles.SelectedItem.ToString());
            }
        }

        private async void SelectProfileButton_Click(object sender, EventArgs e)
        {
            _settings.SetCurrentProfile(_profileLines
                .Where(x => x.EndsWith(Profiles.SelectedItem.ToString()))
                .First());
            await _settings.SaveSettingsAsync();
        }

        private async void RenameButton_Click(object sender, EventArgs e)
        {
            await RenameProfileAsync(Profiles.SelectedItem.ToString());
        }

        private async Task RenameProfileAsync(string profile)
        {
            if (Profiles.SelectedIndex == -1) return;

            FileInfo selectedProfile;
            if (profile.EndsWith(".txt"))
            {
                selectedProfile = new FileInfo(_profileLines
                    .Where(x => x.EndsWith(profile))
                    .First());
            }
            else
            {
                selectedProfile = new FileInfo(_profileLines
                    .Where(x => x.EndsWith(profile + ".txt"))
                    .First());
            }

            string newProfileName = Interaction
                .InputBox("Enter renaming", "Renaming form",
                selectedProfile.Name.Replace(".txt", "").Replace(_priorityWordWithoudTxt, "")
                );

            if (newProfileName.Trim() == "") return;
            if (ContainKeyWords(newProfileName)) return;

            if (profile.EndsWith(_priorityWord)) newProfileName += _priorityWord;
            else newProfileName += ".txt";

            if (ProfileExists(newProfileName))
            {
                MessageBox.Show("Error, this profile already exists");
                return;
            }

            newProfileName = $@"{selectedProfile.Directory.FullName}\{newProfileName}";
            if (_settings.GetCurrentProfile() == selectedProfile.FullName)
            {
                _settings.SetCurrentProfile(newProfileName);
                await _settings.SaveSettingsAsync();
            }
            File.Move(selectedProfile.FullName, newProfileName);
            LoadProfiles();
        }

        private async void ChangePriorityButton_Click(object sender, EventArgs e)
        {
            await ChangeProfilePriority(Profiles.SelectedItem.ToString());
        }

        private async Task ChangeProfilePriority(string profile)
        {
            if (Profiles.SelectedIndex == -1) return;

            FileInfo selectedProfileInfo; 

            if (profile.EndsWith(".txt"))
            {
                selectedProfileInfo = new FileInfo(_profileLines
                    .Where(x => x.EndsWith(profile))
                    .First());
            }
            else
            {
                selectedProfileInfo = new FileInfo(_profileLines
                    .Where(x => x.EndsWith(profile + ".txt"))
                    .First());
            }

            string newProfileName = _settings.listsDirectoryFullPath;

            if (selectedProfileInfo.Name.EndsWith(_priorityWord))
            {
                newProfileName += selectedProfileInfo.Name.Replace(_priorityWord, ".txt");
            }
            else
            {
                newProfileName += selectedProfileInfo.Name.Replace(".txt", _priorityWord);
            }
            if (selectedProfileInfo.FullName == _settings.GetCurrentProfile())
            {
                _settings.SetCurrentProfile(newProfileName);
                await _settings.SaveSettingsAsync();
            }

            File.Move(selectedProfileInfo.FullName, newProfileName);
            LoadProfiles();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            ProfilesExportPicker exportPicker = new ProfilesExportPicker(_profileLines, _settings);
            exportPicker.ShowDialog();
        }

        public void OnAddition()
        {
            LoadProfiles();
        }
    }
}

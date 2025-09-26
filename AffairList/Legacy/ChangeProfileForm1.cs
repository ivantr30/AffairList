using Microsoft.VisualBasic;
namespace AffairList
{
    public partial class ChangeProfileForm1 : BaseForm
    {
        private List<string> _profileLines;

        private string _priorityWord = " Priority.txt";
        private string _priorityTag = " \"Priority\"";

        private Settings settings;

        public ChangeProfileForm1(Settings settings)
        {
            InitializeComponent();
            LoadProfiles();
            this.settings = settings;
        }
        private void LoadProfiles()
        {
            if (!settings.ListsDirectoryExists()) return;

            Profiles.Items.Clear();
            _profileLines = Directory.GetFiles(settings.listsDirectoryFullPath)
                    .OrderByDescending(x => x.EndsWith(_priorityWord)).ToList();

            for (int i = 0; i < _profileLines.Count; i++)
            {
                FileInfo profileFile = new FileInfo(_profileLines[i]);
                Profiles.Items.Add(profileFile.Name);
            }
            if (Profiles.Items.Count > 0)
            {
                Profiles.SelectedIndex = Profiles.Items
                    .IndexOf(settings.GetCurrentProfile().Split("\\")[^1]);
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
        private void MinimizeButton_Click(object sender, EventArgs e) => MinimizeForm();

        private void BackButton_Click(object sender, EventArgs e) => Restart();

        private void CloseButtonLab_Click(object sender, EventArgs e) => Exit();

        private void CloseButtonLab_MouseEnter(object sender, EventArgs e)
        {
            CloseButtonLab.ForeColor = Color.Gray;
        }

        private void CloseButtonLab_MouseLeave(object sender, EventArgs e)
        {
            CloseButtonLab.ForeColor = Color.Black;
        }

        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Gray;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Black;
        }

        private void NameBackground_MouseMove(object sender, MouseEventArgs e) => MoveForm(e);

        private void NameBackground_MouseDown(object sender, MouseEventArgs e) => SetLastPoint(e);

        private void ProfilesLab_MouseDown(object sender, MouseEventArgs e) => SetLastPoint(e);

        private void ProfilesLab_MouseMove(object sender, MouseEventArgs e) => MoveForm(e);
        private void AddProfile()
        {
            if (ProfileInput.Text.Trim() == "")
            {
                MessageBox.Show("Error, textbox is null");
                return;
            }
            for (int i = 0; i < _profileLines.Count; i++)
            {
                FileInfo fileInfo = new FileInfo(_profileLines[i]);
                if (ProfileInput.Text + ".txt" == fileInfo.Name)
                {
                    MessageBox.Show("Error, this file already exists");
                    return;
                }
            }
            if (ContainKeyWords(ProfileInput.Text)) return;
            Profiles.Items.Add(ProfileInput.Text + ".txt");
            Profiles.SelectedIndex = Profiles.Items.Count - 1;

            _profileLines.Add(settings.listsDirectoryFullPath + "\\" + ProfileInput.Text + ".txt");

            using (File.Create(settings.listsDirectoryFullPath + "\\" + ProfileInput.Text + ".txt")) { }

            ProfileInput.Clear();
        }
        private void DeleteProfile()
        {
            if (Profiles.SelectedIndex != -1)
            {
                DialogResult dialogres = MessageBox.Show("Do you want to delete the profile?",
                    "Confirm form",
                    MessageBoxButtons.YesNo);
                if (dialogres == DialogResult.No) return;

               for (int i = 0; i < _profileLines.Count; i++)
                {
                    FileInfo fileInfo = new FileInfo(_profileLines[i]);
                    if (Profiles.SelectedItem!.ToString() == fileInfo.Name)
                    {
                        File.Delete(_profileLines[i]);
                    }
                }

                _profileLines.RemoveAt(Profiles.SelectedIndex);

                int selectedIndex = 0;
                if (Profiles.SelectedIndex == 0 && Profiles.Items.Count > 1)
                {
                    Profiles.SelectedIndex++;
                    selectedIndex = Profiles.SelectedIndex - 1;
                }
                else if (Profiles.Items.Count > 1)
                {
                    Profiles.SelectedIndex--;
                    selectedIndex = Profiles.SelectedIndex + 1;
                }
                Profiles.Items.RemoveAt(selectedIndex);
            }
        }

        private void AddButton_Click(object sender, EventArgs e) => AddProfile();

        private void DeleteButton_Click(object sender, EventArgs e) => DeleteProfile();

        private void ClearButton_Click(object sender, EventArgs e) => ProfileInput.Clear();

        private void ChangeProfileForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddProfile();
            }
            if (e.KeyCode == Keys.Delete)
            {
                DeleteProfile();
            }
            if (e.KeyCode == settings.GetReturnKey())
            {
                Restart();
            }
            if (e.KeyCode == settings.GetCloseKey())
            {
                Exit();
            }
        }

        private async void SelectProfileButton_Click(object sender, EventArgs e)
        {
            settings.SetCurrentProfile(_profileLines[Profiles.SelectedIndex]);
            await settings.SaveSettingsAsync();
        }

        private async void RenameButton_Click(object sender, EventArgs e)
        {
            if (Profiles.SelectedIndex == -1) return;

            FileInfo selectedProfile = new FileInfo(_profileLines[Profiles.SelectedIndex]);

            string newProfileName = Interaction
                .InputBox("Enter renaming", "Renaming form",
                selectedProfile.Name.Replace(".txt", "").Replace(_priorityTag, "")
                );

            if (ContainKeyWords(newProfileName)) return;
            if (newProfileName.Trim() == "")
            {
                MessageBox.Show("Error, wrong input format");
                return;
            }

            if (newProfileName.Contains(_priorityTag)) newProfileName += _priorityTag;
            newProfileName = settings.listsDirectoryFullPath + newProfileName + ".txt";

            if (settings.GetCurrentProfile() == selectedProfile.FullName)
            {
                settings.SetCurrentProfile(newProfileName);
                await settings.SaveSettingsAsync();
            }

            File.Move(selectedProfile.FullName, newProfileName);
            LoadProfiles();
        }

        private async void ChangePriorityButton_Click(object sender, EventArgs e)
        {
            if (Profiles.SelectedIndex == -1) return;

            FileInfo selectedProfileInfo = new FileInfo(_profileLines[Profiles.SelectedIndex]);
            string newProfileName = settings.listsDirectoryFullPath;
            if (selectedProfileInfo.Name.EndsWith(_priorityWord))
            {
                newProfileName += selectedProfileInfo.Name.Replace(_priorityWord, ".txt");
            }
            else
            {
                newProfileName += selectedProfileInfo.Name.Replace(".txt", _priorityWord);
            }
            if (selectedProfileInfo.FullName == settings.GetCurrentProfile())
            {
                settings.SetCurrentProfile(newProfileName);
                await settings.SaveSettingsAsync();
            }

            File.Move(selectedProfileInfo.FullName, newProfileName);
            LoadProfiles();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            ProfilesExportPicker exportPicker = new ProfilesExportPicker(_profileLines, settings);
            exportPicker.ShowDialog();
        }
    }
}

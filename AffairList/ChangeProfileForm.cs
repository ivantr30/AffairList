using Microsoft.VisualBasic;
namespace AffairList
{
    public partial class ChangeProfileForm : BaseForm
    {
        string[] profileLines;
        private string priorityWord = " Приоритетное.txt";
        private string priorityTag = " Приоритетное";
        public ChangeProfileForm(SettingsModel settings)
        {
            InitializeComponent();
            this.settings = settings;
            LoadProfiles();
        }
        private void LoadProfiles()
        {
            profileLines = Directory.GetFiles(settings.listsDirectoryFullPath)
                    .OrderByDescending(x => x.EndsWith(priorityWord)).ToArray();
            foreach (var profile in profileLines)
            {
                FileInfo profileFile = new FileInfo(profile);
                Profiles.Items.Add(profileFile.Name);
            }
            if (Profiles.Items.Count > 0)
            {
                Profiles.SelectedIndex = Profiles.Items
                    .IndexOf(settings.currentListFileFullPath.Split("\\")[^1]);
            }
        }
        private bool ContainKeyWords(string fileName)
        {
            if (fileName.Contains(priorityTag))
            {
                MessageBox.Show($"Error, you list name is perhibited to contain - {priorityTag}");
                return true;
            }
            return false;
        }
        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void CloseButtonLab_Click(object sender, EventArgs e)
        {
            Exit();
        }

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

        private void NameBackground_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }

        private void NameBackground_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) lastPoint = e.Location;
        }

        private void ProfilesLab_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) lastPoint = e.Location;
        }

        private void ProfilesLab_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }
        private void AddProfile()
        {
            if (ProfileInput.Text.Trim() == "")
            {
                MessageBox.Show("Error, textbox is null");
                return;
            }
            foreach (var file in profileLines)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (ProfileInput.Text + ".txt" == fileInfo.Name)
                {
                    MessageBox.Show("Error, this file already exists");
                    return;
                }
            }
            if (ContainKeyWords(ProfileInput.Text)) return;
            Profiles.Items.Add(ProfileInput.Text + ".txt");
            Profiles.SelectedIndex = Profiles.Items.Count - 1;

            var temp = profileLines.ToList();
            temp.Add(settings.listsDirectoryFullPath + "\\" + ProfileInput.Text + ".txt");
            profileLines = temp.ToArray();

            using (File.Create(settings.listsDirectoryFullPath + "\\" + ProfileInput.Text + ".txt"))
            {

            }

            ProfileInput.Text = "";
        }
        private void DeleteProfile()
        {
            if (Profiles.SelectedIndex != -1)
            {
                DialogResult dialogres = MessageBox.Show("Do you want to delete the profile?",
                    "Confirm form",
                    MessageBoxButtons.YesNo);
                if (dialogres == DialogResult.No) return;

                foreach (var file in profileLines)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (Profiles.SelectedItem.ToString() == fileInfo.Name)
                    {
                        File.Delete(file);
                    }
                }

                var temp = profileLines.ToList();
                temp.RemoveAt(Profiles.SelectedIndex);
                profileLines = temp.ToArray();

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

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddProfile();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteProfile();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ProfileInput.Clear();
        }

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
            if (e.KeyCode == settings.returnKey)
            {
                Restart();
            }
            if (e.KeyCode == settings.closeKey)
            {
                Exit();
            }
        }

        private void SelectProfileButton_Click(object sender, EventArgs e)
        {
            settings.SaveParametr("currentProfile", profileLines[Profiles.SelectedIndex]);
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            if (Profiles.SelectedIndex == -1) return;

            try
            {
                string previousProfileFullName = profileLines[Profiles.SelectedIndex];
                FileInfo fileInfo = new FileInfo(previousProfileFullName);
                string currentProfileName = fileInfo.Name.Replace(".txt", "");
                string newProfileName = Interaction
                    .InputBox("Enter renaming", "Renaming form", currentProfileName);

                if (ContainKeyWords(newProfileName)) return;
                if (newProfileName.Trim() == "") throw new Exception();

                profileLines[Profiles.SelectedIndex] = previousProfileFullName
                    .Replace(currentProfileName, newProfileName);

                File.Move(previousProfileFullName, profileLines[Profiles.SelectedIndex]);
                Profiles.Items.Clear();
                LoadProfiles();
            }
            catch
            {
                MessageBox.Show("Error, wrong input format");
            }
        }

        private void ChangePriorityButton_Click(object sender, EventArgs e)
        {
            if (Profiles.SelectedIndex == -1) return;

            FileInfo selectedProfileInfo = new FileInfo(profileLines[Profiles.SelectedIndex]);
            string newProfileName = settings.listsDirectoryFullPath;
            if (selectedProfileInfo.Name.EndsWith(priorityWord))
            {
                newProfileName += selectedProfileInfo.Name.Replace(priorityWord, ".txt");
            }
            else
            {
                newProfileName += selectedProfileInfo.Name.Replace(".txt", priorityWord);
            }
            if (selectedProfileInfo.FullName == settings.currentListFileFullPath)
            {
                settings.currentListFileFullPath = newProfileName;
                settings.SaveParametr("currentProfile", newProfileName);
            }

            File.Move(selectedProfileInfo.FullName, newProfileName);
            Profiles.Items.Clear();
            LoadProfiles();
        }
    }
}

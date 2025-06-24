using Microsoft.VisualBasic;
using System.Data;
using System.Globalization;
namespace AffairList
{
    public partial class ChangeListForm : BaseForm
    {
        private int currentDragIndex = 0;

        private string priorityTag = "<priority>";
        private string priorityWord= "\"Приоритетное\"";

        private string[] lines;

        private bool isDragging = false;

        private Keys addAffairKey = Keys.Enter;
        private Keys deleteAffairKey = Keys.Delete;

        public ChangeListForm(SettingsModel settings)
        {
            InitializeComponent();
            this.settings = settings;
            LoadProfiles();
            LoadText();
        }
        private void LoadProfiles()
        {
            var profiles = Directory.GetFiles(SettingsModel.listsDirectoryFullPath);
            foreach (var profile in profiles)
            {
                FileInfo profileInfo = new FileInfo(profile);
                ProfileBox.Items.Add(profileInfo.Name);
            }
            if (profiles.Length > 0)
            {
                FileInfo selectedProfile = new FileInfo(settings.currentListFileFullPath);
                ProfileBox.SelectedIndex = ProfileBox.Items.IndexOf(selectedProfile.Name);
                settings.currentListFileFullPath = selectedProfile.FullName;
            }
        }
        private void LoadText()
        {
            if (settings.CurrentListNotNull())
            {
                lines = File.ReadAllLines(settings.currentListFileFullPath)
                    .OrderByDescending(x => x.EndsWith(priorityTag)).ToArray();
                Affairs.Items.Clear();

                foreach (string line in lines)
                {
                    var temp = line.Trim();
                    if (temp.EndsWith(priorityTag))
                    {
                        temp = temp.Substring(0, temp.Length - priorityTag.Length);
                        temp += " " + priorityWord;
                    }
                    Affairs.Items.Add(temp);
                }
                if (Affairs.Items.Count > 0)
                {
                    Affairs.SelectedIndex = 0;
                }
            }
        }
        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == settings.closeKey)
            {
                Exit();
            }
            if (e.KeyCode == settings.returnKey)
            {
                Restart();
            }
            if (e.KeyCode == addAffairKey)
            {
                AddAffair();
            }
            if (e.KeyCode == deleteAffairKey)
            {
                DeleteAffair();
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void AffairsLab_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void AffairsLab_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }

        private void NameBackground_MouseDown_1(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void NameBackground_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Gray;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Black;
        }

        private void AddAffair()
        {
            if (AffairInput.Text.Trim() == "")
            {
                MessageBox.Show("Error, textbox is null");
                return;
            }
            string inputText = AffairInput.Text + ".";
            Affairs.Items.Add(inputText);
            Affairs.SelectedIndex = Affairs.Items.Count - 1;

            var temp = lines.ToList();
            temp.Add(inputText);
            lines = temp.ToArray();

            if (settings.CurrentListNotNull())
            {
                File.AppendAllText(settings.currentListFileFullPath, inputText + "\n");
            }
            AffairInput.Text = "";
        }
        private void DeleteAffair()
        {
            if (Affairs.SelectedIndex != -1)
            {
                if (settings.askToDelete)
                {
                    DialogResult dialogres = MessageBox.Show("Do you want to delete the affair?",
                        "Confirm form",
                        MessageBoxButtons.YesNo);
                    if (dialogres == DialogResult.No) return;
                }

                if (settings.CurrentListNotNull())
                {
                    var temp = lines.ToList();
                    temp.RemoveAt(Affairs.SelectedIndex);
                    lines = temp.ToArray();

                    File.WriteAllLines(settings.currentListFileFullPath, lines);
                }
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
            }
        }
        private void AddAffairButton_Click(object sender, EventArgs e)
        {
            AddAffair();
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteAffair();
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            AffairInput.Text = "";
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void ChangeListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Exit();
        }

        private void AddDeadlineButton_Click(object sender, EventArgs e)
        {
            if (Affairs.SelectedIndex == -1)
                return;
            string res;
            string temp = (string)Affairs.Items[Affairs.SelectedIndex];
            try
            {
                DateTime.ParseExact(temp.Substring(0, 10), "dd.MM.yyyy", null, DateTimeStyles.None);
                temp = temp.Substring(10).Trim();

                DialogResult dialogres = MessageBox.Show(
                    "Do you want to delete the deadline or just change it. yes - delete, no - change?",
                    "Confirm form",
                    MessageBoxButtons.YesNoCancel);
                if (dialogres == DialogResult.Yes)
                {
                    Affairs.Items[Affairs.SelectedIndex] = temp;

                    lines[Affairs.SelectedIndex] = lines[Affairs.SelectedIndex].Substring(11);

                    File.WriteAllLines(settings.currentListFileFullPath, lines);
                    return;
                }
                else if (dialogres == DialogResult.No)
                {
                    lines[Affairs.SelectedIndex] = lines[Affairs.SelectedIndex].Substring(11);
                }
                if (dialogres == DialogResult.Cancel) return;
            }
            catch
            {
                temp = temp.Trim();
            }
            try
            {
                res = DateTime.ParseExact(
                            Interaction.InputBox(
                            "Enter Deadline in format dd-MM-yyyy",
                            "DateTime input box"), "dd-MM-yyyy", null, DateTimeStyles.None).ToString();
                res = res.Substring(0, 10).Trim();

                Affairs.Items[Affairs.SelectedIndex] = res + " " + temp;

                lines[Affairs.SelectedIndex] = res + " " + lines[Affairs.SelectedIndex];

                File.WriteAllLines(settings.currentListFileFullPath, lines);
            }
            catch
            {
                MessageBox.Show("Error, wrong input format");
                return;
            }
        }

        private void RenameAffairButton_Click(object sender, EventArgs e)
        {
            if (Affairs.SelectedIndex == -1)
                return;
            string selectedWord = (string)Affairs.Items[Affairs.SelectedIndex];
            string deadline = "";
            string priority = priorityWord;
            string renamedWord = "";
            if(selectedWord.Length > 10)
            {
                deadline = selectedWord.Substring(0, 10);
                if (!DateTime.TryParseExact(
                    deadline, "dd.MM.yyyy", null, DateTimeStyles.None
                    ,out DateTime dateTimeParseResult))
                {
                    deadline = "";
                }
            }
            if (!lines[Affairs.SelectedIndex].EndsWith(priorityTag))
            {
                priority = "";
            }
            if (deadline.Length > 0) selectedWord = selectedWord[11..];
            if (priority.Length > 0) selectedWord = selectedWord.Replace(priorityWord, "").Trim();
            if (selectedWord.EndsWith(".")) selectedWord = selectedWord[0..(selectedWord.Length - 1)];
            string newWord = Interaction
                .InputBox("Enter renaming", "Input box",  selectedWord)+ ".";
            if (newWord.Length == 1) return;

            renamedWord = deadline + " " + newWord;

            Affairs.Items[Affairs.SelectedIndex] = (renamedWord + " " + priority).Trim();
            lines[Affairs.SelectedIndex] = (renamedWord + " " 
                + priority.Replace(priorityWord, priorityTag)).Trim();

            File.WriteAllLines(settings.currentListFileFullPath, lines);
            Affairs.Items.Clear();
            LoadText();
        }

        private void PriorityButton_Click(object sender, EventArgs e)
        {
            if (Affairs.SelectedIndex == -1) return;

            if (lines[Affairs.SelectedIndex].EndsWith(priorityTag))
            {
                string currentLine = (string)Affairs.Items[Affairs.SelectedIndex];

                Affairs.Items[Affairs.SelectedIndex] = currentLine.Replace(" "+ priorityWord, "");

                var temp = ((string)(lines[Affairs.SelectedIndex])).Split(" ").Reverse().ToArray()[0];
                
                if (temp == priorityTag)
                {
                    lines[Affairs.SelectedIndex] = lines[Affairs.SelectedIndex]
                        .Substring(0, lines[Affairs.SelectedIndex].Length - (" " + priorityTag).Length);
                }
            }
            else
            {
                Affairs.Items[Affairs.SelectedIndex] += " " + priorityWord;
                lines[Affairs.SelectedIndex] += " " + priorityTag;
            }

            lines = lines.OrderByDescending(x => x.Contains(priorityTag)).ToArray();
            File.WriteAllLines(settings.currentListFileFullPath, lines);
            Affairs.Items.Clear();
            LoadText();
        }
        private void Affairs_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isDragging)
            {
                isDragging = true;
                currentDragIndex = Affairs.SelectedIndex;
                if (currentDragIndex == Affairs.SelectedIndex)
                    isDragging = false;
            }
        }

        private void Affairs_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentDragIndex == -1 || lines.Length <= Affairs.SelectedIndex)
            {
                return;
            }
            bool newPlacePriority = lines[Affairs.SelectedIndex].EndsWith(priorityTag);
            bool oldPlacePriority = lines[currentDragIndex].EndsWith(priorityTag);
            if (
                (newPlacePriority && !oldPlacePriority) ||
                (!newPlacePriority && oldPlacePriority))
            {
                return;
            }
            var temp = Affairs.Items[currentDragIndex];
            Affairs.Items[currentDragIndex] = Affairs.Items[Affairs.SelectedIndex];
            Affairs.Items[Affairs.SelectedIndex] = temp;

            temp = lines[currentDragIndex];
            lines[currentDragIndex] = lines[Affairs.SelectedIndex];
            lines[Affairs.SelectedIndex] = (string)temp;

            File.WriteAllLines(settings.currentListFileFullPath, lines);
            isDragging = false;
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Gray;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Black;
        }
        private void ChangeProfile()
        {
            var profiles = Directory.GetFiles(SettingsModel.listsDirectoryFullPath);
            foreach (var profile in profiles)
            {
                FileInfo profileInfo = new FileInfo(profile);
                if (profileInfo.Name == ProfileBox.SelectedItem.ToString())
                {
                    settings.SaveParametr("currentProfile", profileInfo.FullName, "");
                    settings.currentListFileFullPath = profileInfo.FullName;
                }
            }
        }

        private void ProfileBox_TextUpdate(object sender, EventArgs e)
        {
            if (File.Exists(ProfileBox.Text))
            {
                ChangeProfile();
            }
            LoadText();
        }

        private void ProfileBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ChangeProfile();
            LoadText();
        }
    }
}

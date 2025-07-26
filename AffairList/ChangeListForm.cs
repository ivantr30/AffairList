using Microsoft.VisualBasic;
using System.Data;
namespace AffairList
{
    public partial class ChangeListForm : BaseForm
    {
        private int currentDragIndex = 0;
        private int deadlineDateNTagLength = 21;
        private int selectedAffairIndex = -1;

        private string priorityTag = "<priority>";
        private string priorityWord = "\"Priority\"";
        private string deadlineTag = "<deadline>";

        private List<string> lines;

        private bool isDragging = false;

        private Keys addAffairKey = Keys.Enter;
        private Keys deleteAffairKey = Keys.Delete;

        public ChangeListForm(Settings settings)
            : base(settings)
        {
            InitializeComponent();
            LoadProfiles();
            LoadText();
        }
        private void LoadProfiles()
        {
            var profiles = Directory.GetFiles(settings.listsDirectoryFullPath);
            foreach (var profile in profiles)
            {
                FileInfo profileInfo = new FileInfo(profile);
                ProfileBox.Items.Add(profileInfo.Name);
            }
            if (profiles.Length > 0)
            {
                FileInfo selectedProfile = new FileInfo(settings.GetCurrentProfile());
                ProfileBox.SelectedIndex = ProfileBox.Items.IndexOf(selectedProfile.Name);
            }
        }
        private void LoadText()
        {
            Affairs.Items.Clear();
            if (settings.CurrentListNotNull())
            {
                lines = File.ReadAllLines(settings.GetCurrentProfile())
                    .OrderByDescending(x => x.EndsWith(priorityTag)).ToList();

                foreach (string line in lines)
                {
                    string currentLine = line;
                    if (currentLine.EndsWith(priorityTag))
                    {
                        currentLine = currentLine
                            .Substring(0, currentLine.Length - priorityTag.Length);
                        currentLine += " " + priorityWord;
                    }
                    if (currentLine.StartsWith(deadlineTag))
                    {
                        currentLine = currentLine.Substring(deadlineTag.Length);
                    }
                    Affairs.Items.Add(currentLine);
                }
                if (selectedAffairIndex != -1 && Affairs.Items.Count > 0) Affairs.SelectedIndex = selectedAffairIndex;
                else if (Affairs.Items.Count > 0) Affairs.SelectedIndex = 0;
            }
        }
        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == settings.GetCloseKey())
            {
                Exit();
            }
            if (e.KeyCode == settings.GetReturnKey())
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
            SetLastPoint(e);
        }

        private void AffairsLab_MouseMove(object sender, MouseEventArgs e)
        {
            MoveForm(e);
        }

        private void NameBackground_MouseDown_1(object sender, MouseEventArgs e)
        {
            SetLastPoint(e);
        }

        private void NameBackground_MouseMove_1(object sender, MouseEventArgs e)
        {
            MoveForm(e);
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
            if(ContainKeyWords(AffairInput.Text)) return;

            string inputText = AffairInput.Text + ".";
            Affairs.Items.Add(inputText);
            Affairs.SelectedIndex = Affairs.Items.Count - 1;

            lines.Add(inputText);

            if (settings.CurrentListNotNull())
            {
                AppendText(inputText + "\n");
            }
            AffairInput.Text = "";
        }
        private void DeleteAffair()
        {
            if (Affairs.SelectedIndex == -1) return;

            if (settings.DoesAskToDelete())
            {
                DialogResult dialogres = MessageBox.Show("Do you want to delete the affair?",
                    "Confirm form",
                    MessageBoxButtons.YesNo);
                if (dialogres == DialogResult.No) return;
            }

            if (settings.CurrentListNotNull())
            {
                lines.RemoveAt(Affairs.SelectedIndex);

                SaveText(lines);
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
            if (Affairs.SelectedIndex == -1) return;

            if (lines[Affairs.SelectedIndex].StartsWith(deadlineTag))
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

            SaveText(lines);
        }
        private void DeleteDeadline()
        {
            lines[Affairs.SelectedIndex] = lines[Affairs.SelectedIndex]
                    .Substring(deadlineDateNTagLength);

            Affairs.Items[Affairs.SelectedIndex] = Affairs.Items[Affairs.SelectedIndex]
                .ToString()!
                .Substring(deadlineTag.Length + 1);
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

            lines[Affairs.SelectedIndex] = deadlineTag + deadline + " " + lines[Affairs.SelectedIndex];
        }
        private void RenameAffairButton_Click(object sender, EventArgs e)
        {
            if (Affairs.SelectedIndex == -1) return;

            string selectedWord = lines[Affairs.SelectedIndex];
            string affair = selectedWord;

            if (selectedWord.StartsWith(deadlineTag))
            {
                affair = selectedWord.Substring(deadlineDateNTagLength);
            }
            if (selectedWord.EndsWith(priorityTag))
            {
                affair = affair.Substring(0, affair.Length - priorityTag.Length - 1);
            }
            affair = affair.Substring(0, affair.Length-1); // Убираем точку в конце

            string renaming = Interaction
                .InputBox("Enter renaming", "Input box", affair);

            if (ContainKeyWords(renaming)) return;
            if (string.IsNullOrEmpty(renaming)) return;

            selectedWord = selectedWord.Replace(affair, renaming);

            lines[Affairs.SelectedIndex] = selectedWord;
            Affairs.Items[Affairs.SelectedIndex] = Affairs.Items[Affairs.SelectedIndex]
                .ToString()!.Replace(affair, renaming);

            SaveText(lines);
        }
        private bool ContainKeyWords(string word)
        {
            if (word.Contains(deadlineTag) || word.Contains(priorityTag)
                || word.Contains(priorityWord))
            {
                MessageBox.Show("Error, you word is perhibited to contain - " +
                    $"{deadlineTag}, {priorityTag}, {priorityWord}");
                return true;
            }
            return false;
        }

        private void PriorityButton_Click(object sender, EventArgs e)
        {
            if (Affairs.SelectedIndex == -1) return;

            if (lines[Affairs.SelectedIndex].EndsWith(priorityTag))
            {

                lines[Affairs.SelectedIndex] = lines[Affairs.SelectedIndex]
                    .Substring(0, lines[Affairs.SelectedIndex].Length - (" " + priorityTag).Length);
            }
            else
            {
                lines[Affairs.SelectedIndex] += " " + priorityTag;
            }

            lines = lines.OrderByDescending(x => x.EndsWith(priorityTag)).ToList();
            SaveText(lines);
            LoadText();
        }
        private void Affairs_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isDragging)
            {
                isDragging = true;
                currentDragIndex = Affairs.SelectedIndex;
            }
        }

        private void Affairs_MouseUp(object sender, MouseEventArgs e)
        {
            if(Affairs.SelectedIndex == -1) return;
            bool newPlacePriority = lines[Affairs.SelectedIndex].EndsWith(priorityTag);
            bool oldPlacePriority = lines[currentDragIndex].EndsWith(priorityTag);

            if (newPlacePriority != oldPlacePriority) return;

            // Не менять, уже нечего
            var switcher = Affairs.Items[currentDragIndex];
            Affairs.Items[currentDragIndex] = Affairs.Items[Affairs.SelectedIndex];
            Affairs.Items[Affairs.SelectedIndex] = switcher;

            switcher = lines[currentDragIndex];
            lines[currentDragIndex] = lines[Affairs.SelectedIndex];
            lines[Affairs.SelectedIndex] = (string)switcher;

            isDragging = false;
            SaveText(lines);
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            MinimizeForm();
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
            var profiles = Directory.GetFiles(settings.listsDirectoryFullPath);
            foreach (var profile in profiles)
            {
                FileInfo profileInfo = new FileInfo(profile);
                if (profileInfo.Name == ProfileBox.SelectedItem!.ToString())
                {
                    settings.SetCurrentProfile(profileInfo.FullName);
                    settings.SaveSettings();
                    selectedAffairIndex = 0;
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

        private void Affairs_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedAffairIndex = Affairs.SelectedIndex;
        }
        private async Task SaveText(List<string> lines)
        {
            await File.WriteAllLinesAsync(settings.GetCurrentProfile(), lines);
        }
        private async Task AppendText(string line)
        {
            await File.AppendAllTextAsync(settings.GetCurrentProfile(), line);
        }
    }
}

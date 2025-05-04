using Gma.System.MouseKeyHook;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace AffairList
{
    public partial class ChangeListForm : Form
    {
        int currentDragIndex = 0;
        bool isDragging = false;
        string[] lines;
        public ChangeListForm()
        {
            InitializeComponent();
            LoadProfiles();
            LoadText();
        }
        private void LoadProfiles()
        {
            var profiles = Directory.GetFiles(Config.listsDirectoryFullPath);
            foreach (var profile in profiles)
            {
                FileInfo profileInfo = new FileInfo(profile);
                ProfileBox.Items.Add(profileInfo.Name);
            }
            if (profiles.Length > 0)
            {
                FileInfo selectedProfile = new FileInfo(Config.currentListFileFullPath);
                ProfileBox.SelectedIndex = ProfileBox.Items.IndexOf(selectedProfile.Name);
                Config.currentListFileFullPath = selectedProfile.FullName;
            }
        }
        private void LoadText()
        {
            if (File.Exists(Config.currentListFileFullPath))
            {
                lines = File.ReadAllLines(Config.currentListFileFullPath)
                    .OrderByDescending(x => x.EndsWith("<priority>")).ToArray();
                Affairs.Items.Clear();

                foreach (string line in lines)
                {
                    var temp = line.Trim();
                    if (temp.EndsWith("<priority>"))
                    {
                        temp = temp.Substring(0, temp.Length - "<priority>".Length);
                        temp += " \"Приоритное\"";
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
            if (e.KeyCode == Config.closeKey)
            {
                Config.Exit();
            }
            if (e.KeyCode == Config.returnKey)
            {
                Config.Restart();
            }
            if (e.KeyCode == Keys.Enter)
            {
                AddAffair();
            }
            if (e.KeyCode == Keys.Delete)
            {
                DeleteAffair();
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Config.Exit();
        }

        private void AffairsLab_MouseDown(object sender, MouseEventArgs e)
        {
            Config.lastPoint = new Point(e.X, e.Y);
        }

        private void AffairsLab_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - Config.lastPoint.X;
                Top += e.Y - Config.lastPoint.Y;
            }
        }

        private void NameBackground_MouseDown_1(object sender, MouseEventArgs e)
        {
            Config.lastPoint = new Point(e.X, e.Y);
        }

        private void NameBackground_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - Config.lastPoint.X;
                Top += e.Y - Config.lastPoint.Y;
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

            Affairs.Items.Add(AffairInput.Text);
            Affairs.SelectedIndex = Affairs.Items.Count - 1;

            var temp = lines.ToList();
            temp.Add(AffairInput.Text);
            lines = temp.ToArray();

            if (File.Exists(Config.currentListFileFullPath))
            {
                File.AppendAllText(Config.currentListFileFullPath, AffairInput.Text + "\n");
            }
            AffairInput.Text = "";
        }
        private void DeleteAffair()
        {
            if (Affairs.SelectedIndex != -1)
            {
                if (Config.askToDelete)
                {
                    DialogResult dialogres = MessageBox.Show("Do you want to delete the affair?",
                        "Confirm form",
                        MessageBoxButtons.YesNo);
                    if (dialogres == DialogResult.No) return;
                }

                if (File.Exists(Config.currentListFileFullPath))
                {
                    var temp = lines.ToList();
                    temp.RemoveAt(Affairs.SelectedIndex);
                    lines = temp.ToArray();

                    File.WriteAllLines(Config.currentListFileFullPath, lines);
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
            Config.Restart();
        }

        private void ChangeListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.Exit();
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

                DialogResult dialogres = MessageBox.Show("Do you want to delete the deadline?",
                    "Confirm form",
                    MessageBoxButtons.YesNo);
                if (dialogres == DialogResult.Yes)
                {
                    Affairs.Items[Affairs.SelectedIndex] = temp;

                    lines[Affairs.SelectedIndex] = lines[Affairs.SelectedIndex].Substring(11);

                    File.WriteAllLines(Config.currentListFileFullPath, lines);
                    return;
                }
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

                File.WriteAllLines(Config.currentListFileFullPath, lines);
            }
            catch
            {
                MessageBox.Show("Error, wrong input format");
                return;
            }
        }

        private void PriorityButton_Click(object sender, EventArgs e)
        {
            if (Affairs.SelectedIndex == -1) return;

            if (lines[Affairs.SelectedIndex].EndsWith("<priority>"))
            {
                string currentLine = (string)Affairs.Items[Affairs.SelectedIndex];

                Affairs.Items[Affairs.SelectedIndex] = currentLine.Replace(" \"Приоритное\"", "");

                var temp = ((string)(lines[Affairs.SelectedIndex])).Split(" ").Reverse().ToArray()[0];

                if (temp == "<priority>")
                {
                    lines[Affairs.SelectedIndex] = lines[Affairs.SelectedIndex]
                        .Substring(0, lines[Affairs.SelectedIndex].Length - " <priority>".Length);

                    File.WriteAllLines(Config.currentListFileFullPath, lines);
                }
                return;
            }
            Affairs.Items[Affairs.SelectedIndex] += " \"Приоритное\"";
            lines[Affairs.SelectedIndex] += " <priority>";

            lines = lines.OrderByDescending(x => x.Contains("<priority>")).ToArray();

            File.WriteAllLines(Config.currentListFileFullPath, lines);

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
            bool newPlacePriority = lines[Affairs.SelectedIndex].EndsWith("<priority>");
            bool oldPlacePriority = lines[currentDragIndex].EndsWith("<priority>");
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

            File.WriteAllLines(Config.currentListFileFullPath, lines);
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
            var profiles = Directory.GetFiles(Config.listsDirectoryFullPath);
            foreach (var profile in profiles)
            {
                FileInfo profileInfo = new FileInfo(profile);
                if (profileInfo.Name == ProfileBox.SelectedItem.ToString())
                {
                    Config.SaveParametr("currentProfile", profileInfo.FullName, "");
                    Config.currentListFileFullPath = profileInfo.FullName;
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

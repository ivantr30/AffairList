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

namespace AffairList
{
    public partial class ChangeListForm : Form
    {
        Point lastPoint;
        int currentDragIndex = 0;
        bool isDragging = false;
        public ChangeListForm()
        {
            InitializeComponent();
            LoadText();
        }
        private void LoadText()
        {
            if (File.Exists(Application.StartupPath + "\\list.txt"))
            {
                foreach (string line in File.ReadAllLines(Application.StartupPath + "\\list.txt"))
                {
                    Affairs.Items.Add(line);
                    Affairs.SelectedIndex++;
                }
            }
        }
        private void CloseOrExit(Action action)
        {
            AffairList.trayIcon.Visible = false;
            action();
        }
        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                CloseOrExit(Application.Exit);
            }
            if (e.KeyCode == Keys.F6)
            {
                CloseOrExit(Application.Restart);
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
            CloseOrExit(Application.Exit);
        }

        private void AffairsLab_MouseDown(object sender, MouseEventArgs e)
        {

            lastPoint = new Point(e.X, e.Y);
        }

        private void AffairsLab_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
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
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
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
            if (File.Exists(Application.StartupPath + "\\list.txt"))
            {
                File.AppendAllText(Application.StartupPath + "\\list.txt", AffairInput.Text + ",\n");
            }
            AffairInput.Text = "";
        }
        private void DeleteAffair()
        {
            if (Affairs.SelectedIndex != -1)
            {
                if (File.Exists(Application.StartupPath + "\\list.txt"))
                {
                    var fileText = File.ReadAllLines(Application.StartupPath + "\\list.txt").ToList();
                    fileText.RemoveAt(Affairs.SelectedIndex);
                    File.WriteAllLines(Application.StartupPath + "\\list.txt", fileText);
                }
                Affairs.Items.RemoveAt(Affairs.SelectedIndex);
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
            CloseOrExit(Application.Restart);
        }

        private void ChangeListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseOrExit(Application.Exit);
        }

        private void AddDeadlineButton_Click(object sender, EventArgs e)
        {
            if (Affairs.SelectedIndex == -1)
                return;
            string res;
            string temp = (string)Affairs.Items[Affairs.SelectedIndex];
            try
            {
                DateTime.ParseExact(temp.Substring(0,10),"dd.MM.yyyy", null, DateTimeStyles.None);
                temp = temp.Substring(10).Trim();
            }
            catch
            {
                temp = temp.Trim();
            }
            res = DateTime.ParseExact(
                        Interaction.InputBox(
                        "Enter Deadline in format dd-MM-yyyy",
                        "DateTime input box"), "dd-MM-yyyy", null, DateTimeStyles.None).ToString();
            res = res.Substring(0, 10).Trim();
            Affairs.Items[Affairs.SelectedIndex] = res + " " + temp;
                ;
            string[] output = new string[Affairs.Items.Count];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (string)Affairs.Items[i];
            }
            File.WriteAllLines(Application.StartupPath + "\\list.txt", output);
        }

        private void PriorityButton_Click(object sender, EventArgs e)
        {

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
            if (currentDragIndex == -1)
                return;
            var temp = Affairs.Items[currentDragIndex];
            Affairs.Items[currentDragIndex] = Affairs.Items[Affairs.SelectedIndex];
            Affairs.Items[Affairs.SelectedIndex] = temp;
            string[] res = new string[Affairs.Items.Count];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = (string)Affairs.Items[i];
            }
            File.WriteAllLines(Application.StartupPath + "\\list.txt", res);
            isDragging = false;
        }
    }
}

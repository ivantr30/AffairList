using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AffairList
{
    public partial class Settings : Form
    {
        Point lastPoint;
        string settingsFileFullPath = Application.StartupPath + "\\settings.txt";
        public Settings()
        {
            InitializeComponent();
        }
        private void LoadSettings()
        {

        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Settings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                Application.Exit();
            }
            if (e.KeyCode == Keys.F6)
            {
                Application.Restart();
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

        private void NameBackground_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void NameBackground_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void SettingsLab_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void SettingsLab_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left = e.X - lastPoint.X;
                this.Top = e.Y - lastPoint.Y;
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Do you really want to reset the settings?",
                "Confirm window",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                File.WriteAllText(settingsFileFullPath, "x,y: \nmusicOn: \ntextColor: \nbackTextColor: \n" +
                            "");
                if (!File.Exists(settingsFileFullPath))
                {
                    MessageBox.Show("Error, settings file does not exist");
                    return;
                }
                MessageBox.Show("The settings were reseted succesfully");
            }
        }
    }
}

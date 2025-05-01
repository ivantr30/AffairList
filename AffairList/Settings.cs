using Microsoft.VisualBasic;
using Microsoft.Win32;
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
        public Settings()
        {
            InitializeComponent();
            VolumeValueLab.Text = VolumeBar.Value.ToString();
            LoadSettings();
        }
        private void LoadSettings()
        {
            LocationLab.Text = Config.x + ", " + Config.y;
            ListTextColorLab.ForeColor = Config.textColor;
            ListBgTextColorLab.ForeColor = Config.bgtextColor;
            if (Config.musicState)
            {
                StateLab.Text = "On";
            }
            else
            {
                StateLab.Text = "OFF";
            }
            if (Config.autostartState)
            {
                autostartStateLab.Text = "On";
            }
            else
            {
                autostartStateLab.Text = "OFF";
            }
            if (Config.askToDelete)
            {
                AskToDeleteState.Text = "On";
            }
            else
            {
                AskToDeleteState.Text = "OFF";
            }
            VolumeValueLab.Text = Config.currentVolume.ToString();
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Config.Exit();
        }

        private void Settings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                Config.Exit();
            }
            if (e.KeyCode == Keys.F6)
            {
                Config.Restart();
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
            Config.lastPoint = new Point(e.X, e.Y);
        }

        private void NameBackground_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - Config.lastPoint.X;
                Top += e.Y - Config.lastPoint.Y;
            }
        }

        private void SettingsLab_MouseDown(object sender, MouseEventArgs e)
        {
            Config.lastPoint = new Point(e.X, e.Y);
        }

        private void SettingsLab_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - Config.lastPoint.X;
                Top += e.Y - Config.lastPoint.Y;
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (!Config.isConfirmed)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure to leave with unsaved settings?",
                    "Confirm window",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            Config.Restart();
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
                Config.WriteBaseSettings();

                if (!File.Exists(Config.settingsFileFullPath))
                {
                    MessageBox.Show("Error, settings file does not exist");
                    return;
                }
                MessageBox.Show("The settings were reseted succesfully");
            }
            Config.Restart();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            Config.SaveSettings();
        }

        private void StateLab_MouseLeave(object sender, EventArgs e)
        {
            StateLab.ForeColor = Color.White;
        }


        private void StateLab_MouseUp(object sender, MouseEventArgs e)
        {
            StateLab.ForeColor = Color.White;
        }

        private void StateLab_MouseEnter(object sender, EventArgs e)
        {
            StateLab.ForeColor = Color.Gray;
        }

        private void StateLab_MouseDown(object sender, MouseEventArgs e)
        {
            StateLab.ForeColor = Color.DarkGray;
            if (StateLab.Text == "On")
            {
                StateLab.Text = "OFF";
                Config.musicState = false;
                return;
            }
            StateLab.Text = "On";
            Config.musicState = true;
            Config.isConfirmed = false;
        }

        private void autostartStateLab_MouseDown(object sender, MouseEventArgs e)
        {
            autostartStateLab.ForeColor = Color.DarkGray;
            if (autostartStateLab.Text == "On")
            {
                autostartStateLab.Text = "OFF";
                Config.autostartState = false;
                return;
            }
            autostartStateLab.Text = "On";
            Config.autostartState = true;
            Config.isConfirmed = false;
        }

        private void autostartStateLab_MouseLeave(object sender, EventArgs e)
        {
            autostartStateLab.ForeColor = Color.White;
        }

        private void autostartStateLab_MouseUp(object sender, MouseEventArgs e)
        {
            autostartStateLab.ForeColor = Color.White;
        }

        private void autostartStateLab_MouseEnter(object sender, EventArgs e)
        {
            autostartStateLab.ForeColor = Color.Gray;
        }

        private void PickTextColorButton_Click(object sender, EventArgs e)
        {
            var res = ColorPicker.ShowDialog();
            if (res == DialogResult.OK)
            {
                ListTextColorLab.ForeColor = ColorPicker.Color;
                Config.textColor = ListTextColorLab.ForeColor;
                Config.isConfirmed = false;
            }
        }

        private void PickBgColorButton_Click(object sender, EventArgs e)
        {
            var res = ColorPicker.ShowDialog();
            if (res == DialogResult.OK)
            {
                ListBgTextColorLab.ForeColor = ColorPicker.Color;
                Config.bgtextColor = ListBgTextColorLab.ForeColor;
                Config.isConfirmed = false;
            }
        }

        private void VolumeBar_Scroll(object sender, EventArgs e)
        {
            Config.currentVolume = VolumeBar.Value;
            VolumeValueLab.Text = Config.currentVolume.ToString();
            Config.isConfirmed = false;
        }

        private void LocationLab_DoubleClick(object sender, EventArgs e)
        {
            int prevX = Config.x, prevY = Config.y;
            try
            {
                Config.x = int.Parse(Interaction.InputBox("Enter x coordinate", "InputWindow", ""));
            }
            catch
            {
                MessageBox.Show("Error");
                Config.x = prevX;
                return;
            }
            try
            {
                Config.y = int.Parse(Interaction.InputBox("Enter y coordinate", "InputWindow", ""));
            }
            catch
            {
                MessageBox.Show("Error");
                Config.y = prevY;
                return;
            }
            Config.isConfirmed = false;
            LocationLab.Text = Config.x + ", " + Config.y;
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.Exit();
        }

        private void LocationLab_MouseEnter(object sender, EventArgs e)
        {
            LocationLab.ForeColor = Color.Gray;
        }

        private void LocationLab_MouseLeave(object sender, EventArgs e)
        {
            LocationLab.ForeColor = Color.White;
        }

        private void AskToDeleteState_MouseDown(object sender, MouseEventArgs e)
        {
            AskToDeleteState.ForeColor = Color.DarkGray;
            if (AskToDeleteState.Text == "On")
            {
                AskToDeleteState.Text = "OFF";
                Config.askToDelete = false;
                return;
            }
            AskToDeleteState.Text = "On";
            Config.askToDelete = true;
            Config.isConfirmed = false;
        }

        private void AskToDeleteState_MouseLeave(object sender, EventArgs e)
        {
            AskToDeleteState.ForeColor = Color.White;
        }

        private void AskToDeleteState_MouseEnter(object sender, EventArgs e)
        {
            AskToDeleteState.ForeColor = Color.Gray;
        }

        private void AskToDeleteState_MouseUp(object sender, MouseEventArgs e)
        {
            AskToDeleteState.ForeColor = Color.White;
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
    }
}

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
        Point lastPoint;
        string settingsFileFullPath = Application.StartupPath + "\\settings.txt";
        bool isConfirmed = true;
        bool musicState = true;
        bool autostartState = true;
        int currentVolume = 0;
        int x, y;
        public Settings()
        {
            InitializeComponent();
            currentVolume = VolumeBar.Value;
            VolumeValueLab.Text = currentVolume.ToString();
            LoadSettings();
        }
        private void LoadSettings()
        {
            string[] settingLine = File.ReadAllLines(settingsFileFullPath);
            for (int i = 0; i < settingLine.Length; i++)
            {
                string[] currentSetting = settingLine[i].Substring(settingLine[i].IndexOf(":")
                    ).Split(" ");
                if (settingLine[i].Contains("x,y:"))
                {
                    x = int.Parse(currentSetting[1]);
                    y = int.Parse(currentSetting[2]);

                    LocationLab.Text = x + ", " + y;
                }
                if (settingLine[i].Contains("textColor"))
                {
                    ListTextColorLab.ForeColor = Color.FromArgb(0, int.Parse(currentSetting[1]), int.Parse(currentSetting[2]),
                        int.Parse(currentSetting[3]));
                }
                if (settingLine[i].Contains("backTextColor:"))
                {
                    ListBgTextColorLab.ForeColor = Color.FromArgb(0, int.Parse(currentSetting[1]), int.Parse(currentSetting[2]),
                        int.Parse(currentSetting[3]));
                }
                if (settingLine[i].Contains("musicOn"))
                {
                    if (currentSetting[1].Contains("True"))
                    {
                        StateLab.Text = "On";
                        musicState = true;
                    }
                    else
                    {
                        StateLab.Text = "OFF";
                        musicState = false;
                    }
                }
                if (settingLine[i].Contains("autostarts"))
                {
                    if (currentSetting[1].Contains("True"))
                    {
                        autostartStateLab.Text = "On";
                        autostartState = true;
                    }
                    else
                    {
                        autostartStateLab.Text = "OFF";
                        autostartState = false;
                    }
                }
                if (settingLine[i].Contains("musicVolume"))
                {
                    VolumeValueLab.Text = currentSetting[1];
                }
            }
        }
        private void CloseOrExit(Action action)
        {
            AffairList.trayIcon.Visible = false;
            action();
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            CloseOrExit(Application.Exit);
        }

        private void Settings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                CloseOrExit(Application.Exit);
            }
            if (e.KeyCode == Keys.F6)
            {
                CloseOrExit(Application.Restart);
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
            if (!isConfirmed)
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
            CloseOrExit(Application.Restart);
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
                File.WriteAllText(settingsFileFullPath, "x,y: \nmusicOn: true" +
                    $"\ntextColor: {Color.MediumSpringGreen.R} {Color.MediumSpringGreen.G} {Color.MediumSpringGreen.B}" +
                    $"\nbackTextColor: {Color.Black.R} {Color.Black.G} {Color.Black.B}\n" +
                        "musicVolume: 35\nautostarts: true\n");
                if (!File.Exists(settingsFileFullPath))
                {
                    MessageBox.Show("Error, settings file does not exist");
                    return;
                }
                MessageBox.Show("The settings were reseted succesfully");
            }
            Application.Restart();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            isConfirmed = true;
            string[] settingLines = File.ReadAllLines(settingsFileFullPath);
            for (int i = 0; i < settingLines.Length; i++)
            {
                if (settingLines[i].Contains("x,y:"))
                {
                    settingLines[i] = settingLines[i].Substring(0, settingLines[i].IndexOf(":")) +
                        ": " + x + " " + y;

                    LocationLab.Text = x + ", " + y;
                }
                if (settingLines[i].Contains("textColor"))
                {
                    settingLines[i] = settingLines[i].Substring(0, settingLines[i].IndexOf(":")) +
                        ": " + ListTextColorLab.ForeColor.R + " " + ListTextColorLab.ForeColor.G
                        + " " + ListTextColorLab.ForeColor.B;
                }
                if (settingLines[i].Contains("backTextColor"))
                {
                    settingLines[i] = settingLines[i].Substring(0, settingLines[i].IndexOf(":")) +
                        ": " + ListBgTextColorLab.ForeColor.R + " " + ListBgTextColorLab.ForeColor.G
                        + " " + ListBgTextColorLab.ForeColor.B;
                }
                if (settingLines[i].Contains("musicOn"))
                {
                    settingLines[i] = settingLines[i].Substring(0, settingLines[i].IndexOf(":")) +
                        ": " + musicState;
                }
                if (settingLines[i].Contains("autostarts"))
                {
                    settingLines[i] = settingLines[i].Substring(0, settingLines[i].IndexOf(":")) +
                        ": " + autostartState;
                }
                if (settingLines[i].Contains("musicVolume"))
                {
                    settingLines[i] = settingLines[i].Substring(0, settingLines[i].IndexOf(":")) +
                        ": " + VolumeBar.Value;
                }
            }
            File.WriteAllLines(settingsFileFullPath, settingLines);
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
                musicState = false;
                return;
            }
            StateLab.Text = "On";
            musicState = true;
            isConfirmed = false;
        }

        private void autostartStateLab_MouseDown(object sender, MouseEventArgs e)
        {
            autostartStateLab.ForeColor = Color.DarkGray;
            if (autostartStateLab.Text == "On")
            {
                autostartStateLab.Text = "OFF";
                autostartState = false;
                return;
            }
            autostartStateLab.Text = "On";
            autostartState = true;
            isConfirmed = false;
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
            }
        }

        private void PickBgColorButton_Click(object sender, EventArgs e)
        {
            var res = ColorPicker.ShowDialog();
            if (res == DialogResult.OK)
            {
                ListBgTextColorLab.ForeColor = ColorPicker.Color;
            }
        }

        private void VolumeBar_Scroll(object sender, EventArgs e)
        {
            currentVolume = VolumeBar.Value;
            VolumeValueLab.Text = currentVolume.ToString();
            isConfirmed = false;
        }

        private void LocationLab_DoubleClick(object sender, EventArgs e)
        {
            int prevX = x, prevY = y;
            try
            {
                x = int.Parse(Interaction.InputBox("Enter x coordinate", "InputWindow", ""));
            }
            catch
            {
                MessageBox.Show("Error");
                x = prevX;
                return;
            }
            try
            {
                y = int.Parse(Interaction.InputBox("Enter y coordinate", "InputWindow", ""));
            }
            catch
            {
                MessageBox.Show("Error");
                y = prevY;
                return;
            }
            isConfirmed = false;
            LocationLab.Text = x + ", " + y;
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void LocationLab_MouseEnter(object sender, EventArgs e)
        {

        }

        private void LocationLab_MouseLeave(object sender, EventArgs e)
        {

        }
    }
}

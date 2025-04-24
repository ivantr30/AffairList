namespace AffairList
{
    public partial class AffairList : Form
    {
        string listFileFullPath = Application.StartupPath + "\\list.txt";
        string settingsFileFullPath = Application.StartupPath + "\\settings.txt";
        Point lastPoint;
        public AffairList()
        {
            InitializeComponent();

            if (!File.Exists(listFileFullPath))
            {
                using (File.Create(listFileFullPath))
                {

                }
            }
            if (!File.Exists(settingsFileFullPath))
            {
                using (File.Create(settingsFileFullPath))
                {

                }
                File.WriteAllText(settingsFileFullPath, "x,y: \nmusicOn: \ntextColor: \nbackTextColor: \n" +
                        "");
            }
            ConfigureSettings();
        }
        private void ConfigureSettings()
        {
            string[] settingLines = File.ReadAllLines(settingsFileFullPath);
            for (int i = 0; i < settingLines.Length; i++)
            {
                string[] lineSplitted = settingLines[i].Split(":");
                if (lineSplitted[1].Trim().Length > 0)
                {
                    continue;
                }
                if (lineSplitted[0].StartsWith("x,y"))
                {
                    int width = Screen.PrimaryScreen.WorkingArea.Width;
                    int height = Screen.PrimaryScreen.WorkingArea.Height + Screen.PrimaryScreen.WorkingArea.Height / 10;
                    lineSplitted[1] = width - width / 6 + " " + height / 90;
                }

                settingLines[i] = lineSplitted[0] + ": " + lineSplitted[1];
            }
            File.WriteAllLines(settingsFileFullPath,settingLines);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void AffairListLab_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void AffairListLab_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void OpenListButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            List list = new List();
            list.Show();
        }

        private void ClearListButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Are you sure to clear your affair list?",
            "Confirm window",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                if (!File.Exists(listFileFullPath))
                {
                    MessageBox.Show("Error, list file does not exist");
                    return;
                }
                File.WriteAllText(listFileFullPath, "");
                MessageBox.Show("The list is cleared");
            }
        }

        private void ChangeListButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChangeListForm listForm = new ChangeListForm();
            listForm.Show();
        }

        private void ReplaceAffairListButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            List list = new List();
            list.BackColor = Color.White;
            list.canReplace = true;
            list.Show();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Settings settings = new Settings();
            settings.Show();
        }
    }
}

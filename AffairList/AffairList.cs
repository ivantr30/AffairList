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
                File.Create(listFileFullPath);
            }
            if (!File.Exists(settingsFileFullPath))
            {
                File.Create(settingsFileFullPath);
            }
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
            "Вы уверены, что хотите очистить список дел?",
            "Подтверждение действия",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                if (!File.Exists(listFileFullPath))
                {
                    MessageBox.Show("Ошибка, список не найден");
                    return;
                }
                File.WriteAllText(listFileFullPath, "");
                MessageBox.Show("Список очищен");
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
    }
}


namespace AffairList
{
    public partial class HotKeySettings : BaseForm
    {
        private bool isConfirmed = true;
        private Keys key;
        public HotKeySettings(Settings settings) 
            : base(settings) 
        {
            InitializeComponent();
            LoadSettings();
        }
        private void LoadSettings()
        {
            CloseKeyType.Text = settings.GetCloseKey().ToString();
            BackKeyType.Text = settings.GetReturnKey().ToString();
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            if (!isConfirmed)
            {
                DialogResult saveOrNot = MessageBox.Show("Are you sure to leave with unsaved settings?",
                "Confirm form",
                MessageBoxButtons.YesNo);
                if (saveOrNot == DialogResult.No) return;
            }
            Restart();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            MinimizeForm();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            DialogResult resetOrNot = MessageBox.Show("Are you sure to reset hotkey settings?",
                "Confirm form",
                MessageBoxButtons.YesNo);
            if (resetOrNot == DialogResult.No) return;

            settings.SetCloseKey(Keys.F7);
            settings.SetReturnKey(Keys.F6);
            settings.SaveSettings();

            CloseKeyType.Text = "F7";
            BackKeyType.Text = "F6";

            isConfirmed = false;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            settings.SaveSettings();
            isConfirmed = true;
        }
        private void CloseKeyType_DoubleClick(object sender, EventArgs e)
        {
            key = SetKey();
            if (key == Keys.Escape) return;

            settings.SetCloseKey(key);
            CloseKeyType.Text = settings.GetCloseKey().ToString();
            isConfirmed = false;
        }

        private void BackKeyType_DoubleClick(object sender, EventArgs e)
        {
            key = SetKey();
            if (key == Keys.Escape) return;

            settings.SetReturnKey(key);
            BackKeyType.Text = settings.GetReturnKey().ToString();
            isConfirmed = false;
        }
        private Keys SetKey()
        {
            Keys key = Keys.None;
            InputKeyForm keyForm = new InputKeyForm();
            keyForm.OnKeyPressed += delegate {
                key = keyForm.Key;
            };
            keyForm.ShowDialog();
            return key;
        }

        private void CloseKeyType_MouseEnter(object sender, EventArgs e)
        {
            CloseKeyType.ForeColor = Color.Gray;
        }

        private void CloseKeyType_MouseUp(object sender, MouseEventArgs e)
        {
            CloseKeyType.ForeColor = Color.White;
        }

        private void BackKeyType_MouseEnter(object sender, EventArgs e)
        {
            BackKeyType.ForeColor = Color.Gray;
        }

        private void BackKeyType_MouseUp(object sender, MouseEventArgs e)
        {
            BackKeyType.ForeColor = Color.White;
        }

        private void BackKeyType_MouseLeave(object sender, EventArgs e)
        {
            BackKeyType.ForeColor = Color.White;
        }

        private void CloseKeyType_MouseLeave(object sender, EventArgs e)
        {
            CloseKeyType.ForeColor = Color.White;
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Gray;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Black;
        }

        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Gray;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Black;
        }
        private void MinimizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            MinimizeButton.ForeColor = Color.Black;
        }

        private void NameBackground_MouseMove(object sender, MouseEventArgs e)
        {
            MoveForm(e);
        }
        private void NameBackground_MouseDown(object sender, MouseEventArgs e)
        {
            SetLastPoint(e);
        }
        private void HotKeySettingsLab_MouseMove(object sender, MouseEventArgs e)
        {
            MoveForm(e);
        }

        private void HotKeySettingsLab_MouseDown(object sender, MouseEventArgs e)
        {
            SetLastPoint(e);
        }
    }
}

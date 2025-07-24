
namespace AffairList
{
    public partial class HotKeySettings : BaseForm
    {
        private bool isConfirmed = true;
        private Keys key;
        public HotKeySettings(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
            LoadSettings();
        }
        private void LoadSettings()
        {
            CloseKeyType.Text = settings.closeKey.ToString();
            BackKeyType.Text = settings.returnKey.ToString();
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

            settings.SaveParametr("closeKey", Keys.F7);
            settings.SaveParametr("returnKey", Keys.F6);

            CloseKeyType.Text = "F7";
            BackKeyType.Text = "F6";

            isConfirmed = false;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            settings.SaveParametr("closeKey", settings.closeKey);
            settings.SaveParametr("returnKey", settings.returnKey);
            isConfirmed = true;
        }
        private void CloseKeyType_DoubleClick(object sender, EventArgs e)
        {
            key = SetKey();
            if (key == Keys.None) return;

            settings.closeKey = key;
            CloseKeyType.Text = settings.closeKey.ToString();
            isConfirmed = false;
        }

        private void BackKeyType_DoubleClick(object sender, EventArgs e)
        {
            key = SetKey();
            if (key == Keys.None) return;

            settings.returnKey = key;
            BackKeyType.Text = settings.returnKey.ToString();
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

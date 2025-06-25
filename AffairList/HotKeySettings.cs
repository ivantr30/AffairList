using Microsoft.VisualBasic;
using System.Runtime;
namespace AffairList
{
    public partial class HotKeySettings : BaseForm
    {
        private bool isConfirmed = true;
        public HotKeySettings(SettingsModel settings)
        {
            InitializeComponent();
            this.settings = settings;
            LoadSettings();
        }
        private void LoadSettings()
        {
            CloseKeyType.Text = Enum.Parse(typeof(Keys), settings.closeKey.ToString()).ToString();
            BackKeyType.Text = Enum.Parse(typeof(Keys), settings.returnKey.ToString()).ToString();
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            if (!isConfirmed)
            {
                DialogResult resetOrNot = MessageBox.Show("Are you sure to leave with unsaved settings?",
                "Confirm form",
                MessageBoxButtons.YesNo);
                if (resetOrNot == DialogResult.No) return;
            }
            Restart();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
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
            try
            {
                InputKeyForm keyForm = new InputKeyForm();
                keyForm.OnKeyPressed += delegate { settings.closeKey = keyForm.Key; };
                keyForm.ShowDialog();

                CloseKeyType.Text = settings.closeKey.ToString();
                isConfirmed = false;
            }
            catch
            {
                MessageBox.Show("Error, wrong input format");
            }
        }

        private void BackKeyType_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                InputKeyForm keyForm = new InputKeyForm();
                keyForm.OnKeyPressed += delegate { settings.returnKey = keyForm.Key; };
                keyForm.ShowDialog();

                BackKeyType.Text = settings.returnKey.ToString();
                isConfirmed = false;
            }
            catch
            {
                MessageBox.Show("Error, wrong input format");
            }
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
            if (e.Button == MouseButtons.Left)
            {
                Top += e.Y - lastPoint.Y;
                Left += e.X - lastPoint.X;
            }
        }
        private void NameBackground_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) lastPoint = e.Location;
        }
        private void HotKeySettingsLab_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Top += e.Y - lastPoint.Y;
                Left += e.X - lastPoint.X;
            }
        }

        private void HotKeySettingsLab_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) lastPoint = e.Location;
        }
    }
}

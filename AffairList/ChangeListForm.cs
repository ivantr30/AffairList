using Gma.System.MouseKeyHook;
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
    public partial class ChangeListForm : Form
    {
        private IKeyboardMouseEvents globalHook;
        public ChangeListForm()
        {
            InitializeComponent();
            SubscribeGlobalHook();
            LoadText();
        }
        private void LoadText()
        {
            if (File.Exists(Application.StartupPath + "\\list.txt"))
            {
                foreach (string line in File.ReadAllLines(Application.StartupPath + "\\list.txt"))
                {
                    Affairs.Items.Add(line);
                }
            }
        }
        private void SubscribeGlobalHook()
        {
            globalHook = Hook.GlobalEvents();
            globalHook.KeyDown += GlobalHook_KeyDown;
        }
        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                Application.Exit();
            }
            if (e.KeyCode == Keys.F6)
            {
                this.Hide();
                AffairList list = new AffairList();
                globalHook.KeyDown -= GlobalHook_KeyDown;
                globalHook.Dispose();
                list.Show();
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Point lastPoint;

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

        private void AddAffairButton_Click(object sender, EventArgs e)
        {
            Affairs.Items.Add(AffairInput.Text);
            if (File.Exists(Application.StartupPath + "\\list.txt"))
            {
                File.AppendAllText(Application.StartupPath + "\\list.txt", AffairInput.Text + ",\n");
            }
            AffairInput.Text = "";
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            AffairInput.Text = "";
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            AffairList list = new AffairList();
            list.Show();
        }

        int? currentIndex = null;
        private void Affairs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (currentIndex != null)
                {
                    if (File.Exists(Application.StartupPath + "\\list.txt"))
                    {
                        var fileText = File.ReadAllLines(Application.StartupPath + "\\list.txt").ToList();
                        fileText.RemoveAt((int)currentIndex); 
                        File.WriteAllLines(Application.StartupPath + "\\list.txt", fileText);
                    }
                    Affairs.Items.RemoveAt((int)currentIndex);
                }
            }
        }

        private void Affairs_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentIndex = Affairs.SelectedIndex;
        }
    }
}

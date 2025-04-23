using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AffairList
{
    public partial class List : Form
    {
        private IKeyboardMouseEvents globalHook;
        public List()
        {
            InitializeComponent();
            SubscribeGlobalHook();
            LoadText();
            SetLocation();
        }
        private void SetLocation()
        {
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height + Screen.PrimaryScreen.WorkingArea.Height / 10;
            TopMost = true;

            Affairs.Left = Width - Width / 6;
            Affairs.AutoSize = false;
            Affairs.Padding = new Padding(0,0,180,0);
            Affairs.Size = new Size(500, Height);
        }
        private void LoadText()
        {
            if (File.Exists(Application.StartupPath + "\\list.txt"))
            {
                Affairs.Text = File.ReadAllText(Application.StartupPath + "\\list.txt");
            }
            else
            {
                Affairs.Text = "Нет дел";
            }
            Affairs.Text += "Это весь список ваших дел";
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
    }
}

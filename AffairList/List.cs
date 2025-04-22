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
        public List()
        {
            InitializeComponent();
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height + Screen.PrimaryScreen.WorkingArea.Height / 10;
            TopMost = true;
            Affairs.Left = Width - Width / 6;
            Affairs.Padding = new Padding(0, 0, 180, 0);
            Affairs.Size = new Size(500, Height);
            if (File.Exists(Path.GetFullPath(@"С:\AffairList") + "\\list.txt"))
            {
                Affairs.Text = File.ReadAllText(Path.GetFullPath(@"С:\AffairList") + "\\list.txt");
            }
            else
            {
                Affairs.Text = "Нет дел";
            }
        }

        private void List_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                Application.Exit(); 
            }
            if (e.KeyCode == Keys.F6)
            {
                this.Hide();
                AffairList list = new AffairList();
                list.Show();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            TopLevel = true;
            Affairs.Left = Width - Width / 6;
            Affairs.Padding = new Padding(0, 0, 180, 0);
            Affairs.Size = new Size(500, Height);
            Affairs.Text = "Дело 1: доделать этот проект";
        }
    }
}

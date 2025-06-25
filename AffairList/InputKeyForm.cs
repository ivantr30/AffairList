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
    public partial class InputKeyForm : BaseForm
    {
        public Keys Key { get; private set; }
        public delegate void KeyPressed();
        public event KeyPressed OnKeyPressed;
        public InputKeyForm()
        {
            InitializeComponent();
        }

        private void InputKeyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else
            {
                Key = e.KeyCode;
                OnKeyPressed?.Invoke();
                Close();
            }
        }
    }
}

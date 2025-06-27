
namespace AffairList
{
    public partial class InputKeyForm : BaseForm
    {
        public Keys Key { get; private set; }
        public event Action OnKeyPressed;
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

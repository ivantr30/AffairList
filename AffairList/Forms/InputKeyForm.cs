namespace AffairList
{
    public partial class InputKeyForm : Form
    {
        public Keys Key { get; private set; }
        public event Action OnKeyPressed;
        public InputKeyForm() => InitializeComponent();

        private void InputKeyForm_KeyDown(object sender, KeyEventArgs e)
        {
            Key = e.KeyCode;
            OnKeyPressed?.Invoke();
            Close();
        }
    }
}

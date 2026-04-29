namespace AffairList
{
    public partial class InputDeadlineForm : Form
    {
        public DateOnly? deadline { get; set; } = null;

        public event Action OnConfirm;
        public InputDeadlineForm()
        {
            InitializeComponent();
            DeadlinePicker.Value = DateTime.Now.ToLocalTime();
        }

        private void BackButton_Click(object sender, EventArgs e) => Close();

        private void ConfirmButton_Click(object sender, EventArgs e) => Confirm();
        private void Confirm()
        {
            deadline = new DateOnly(DeadlinePicker.Value.Year, DeadlinePicker.Value.Month, DeadlinePicker.Value.Day);
            OnConfirm?.Invoke();
            Close();
        }

        private void DeadlinePicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Confirm();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}

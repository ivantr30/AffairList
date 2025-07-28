
namespace AffairList
{
    public partial class InputDeadlineForm : BaseForm
    {
        public DateTime deadline { get; set; }
        public event Action OnConfirm;
        public InputDeadlineForm()
        {
            InitializeComponent();
            DeadlinePicker.Value = DateTime.Now.ToLocalTime();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            Confirm();
        }
        private void Confirm()
        {
            deadline = DeadlinePicker.Value;
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

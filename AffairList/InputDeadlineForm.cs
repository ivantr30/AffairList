
namespace AffairList
{
    public partial class InputDeadlineForm : Form
    {
        public DateTime deadline;
        public event Action OnConfirm;
        public InputDeadlineForm()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            deadline = DeadlinePicker.Value;
            OnConfirm?.Invoke();
            Close();
        }
    }
}

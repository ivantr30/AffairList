using Microsoft.VisualBasic;
using System.Data;
namespace AffairList
{
    public partial class ChangeListForm1 : Form
    {
        public ChangeListForm1(Settings settings)
        {
            InitializeComponent();
        }
        private void LoadProfiles()
        {
        }
        private void LoadText()
        {
           
        }
        private async void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void CloseButton_Click(object sender, EventArgs e) => Console.Write(1);

        private void AffairsLab_MouseDown(object sender, MouseEventArgs e) => Console.Write(1);

        private void AffairsLab_MouseMove(object sender, MouseEventArgs e) => Console.Write(1);

        private void NameBackground_MouseDown_1(object sender, MouseEventArgs e) => Console.Write(1);

        private void NameBackground_MouseMove_1(object sender, MouseEventArgs e) => Console.Write(1);

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Gray;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Black;
        }

        private async Task AddAffair()
        {
        }
        private async Task DeleteAffair()
        {

            int selectedIndex = 0;
            if (Affairs.SelectedIndex == 0 && Affairs.Items.Count > 1)
            {
                Affairs.SelectedIndex++;
                selectedIndex = Affairs.SelectedIndex - 1;
            }
            else if (Affairs.Items.Count > 1)
            {
                Affairs.SelectedIndex--;
                selectedIndex = Affairs.SelectedIndex + 1;
            }

            Affairs.Items.RemoveAt(selectedIndex);
        }
        private async void AddAffairButton_Click(object sender, EventArgs e) => await AddAffair();
        private async void DeleteButton_Click(object sender, EventArgs e) => await DeleteAffair();
        private void ClearButton_Click(object sender, EventArgs e) => AffairInput.Clear();

        private void BackButton_Click(object sender, EventArgs e) => Console.Write(1);

        private void ChangeListForm_FormClosing(object sender, FormClosingEventArgs e) => Console.Write(1);

        private async void AddDeadlineButton_Click(object sender, EventArgs e)
        {
        }
        private void DeleteDeadline()
        {
        }
        private void AddDeadline(bool hasDeadline)
        {
          
        }
        private async void RenameAffairButton_Click(object sender, EventArgs e)
        {
        }
        private bool ContainKeyWords(string word)
        {
            return false;
        }

        private async void PriorityButton_Click(object sender, EventArgs e)
        {
        }
        private void Affairs_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private async void Affairs_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void MinimizeButton_Click(object sender, EventArgs e) => MinimizeForm();

        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Gray;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Black;
        }
        private async Task ChangeProfile()
        {
        }

        private async void ProfileBox_TextUpdate(object sender, EventArgs e)
        {
            if (File.Exists(ProfileBox.Text))
            {
                await ChangeProfile();
            }
            LoadText();
        }

        private async void ProfileBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            await ChangeProfile();
            LoadText();
        }

        private void Affairs_SelectedValueChanged(object sender, EventArgs e)
        {
        }
        private async Task SaveText(List<string> lines)
        {
        }
        private async Task AppendText(string line)
        {
        }
    }
}

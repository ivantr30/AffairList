using AffairList.Services.Managers;
namespace AffairList
{
    public partial class ProfilesExportPicker : Form
    {
        private Settings _settings;
        public ProfilesExportPicker(List<string> profiles, Settings settings)
        {
            InitializeComponent();
            _settings = settings;
            for (int i = 0; i < profiles.Count; i++)
            {
                FileInfo profileFile = new FileInfo(profiles[i]);
                ProfilePicker.Items.Add(profileFile.Name);
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (ProfilePicker.CheckedItems.Count == 0)
            {
                Close();
                return;
            }
            bool _operationSuceed = false;

            var res = ProfilesExportDialog.ShowDialog();

            if (string.IsNullOrEmpty(ProfilesExportDialog.SelectedPath) || res == DialogResult.Cancel) return;

            for (int i = 0; i < ProfilePicker.CheckedItems.Count; i++)
            {
                string sourceFilePath = Path.Combine(Settings.listsDirectoryFullPath, ProfilePicker.CheckedItems[i].ToString());
                string destinationFilePath = Path.Combine(ProfilesExportDialog.SelectedPath, ProfilePicker.CheckedItems[i].ToString());

                if (File.Exists(destinationFilePath))
                {
                    DialogResult result = MessageBox.Show(
                        $"File {ProfilePicker.CheckedItems[i]} exists, do you want to override it?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        Close();
                        return;
                    }
                    try
                    {
                        File.Delete(destinationFilePath);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Error, the file is in use");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("Error, the acces denied");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error, the unexpected error");
                    }
                }
                else
                {
                    try
                    {
                        File.Copy(sourceFilePath, destinationFilePath);
                        _operationSuceed = true;
                    }
                    catch (IOException)
                    {
                        MessageBox.Show($"Error, couldn't find the file or it is in use");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("Error, the acces denied");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error, the unexpected error");
                    }
                }

            }

            if(_operationSuceed) MessageBox.Show("Success");

            Close();
        }
    }
}

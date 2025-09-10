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

            ProfilesExportDialog.ShowDialog();

            for (int i = 0; i < ProfilePicker.CheckedItems.Count; i++)
            {
                string sourceFilePath = _settings.listsDirectoryFullPath +
                    ProfilePicker.CheckedItems[i];
                string destinationFilePath = ProfilesExportDialog.SelectedPath + "\\" +
                    ProfilePicker.CheckedItems[i];

                if (File.Exists(destinationFilePath))
                {
                    DialogResult result = MessageBox.Show(
                        $"File {ProfilePicker.CheckedItems[i]} exists, do you want to override it?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if(result == DialogResult.Yes)
                    {
                        File.Delete(destinationFilePath);
                        File.Copy(sourceFilePath, destinationFilePath);
                    }
                }
                else
                {
                    File.Copy(sourceFilePath, destinationFilePath);
                }
            }
            MessageBox.Show("Success");
            Close();
        }
    }
}

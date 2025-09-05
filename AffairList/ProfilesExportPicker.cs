
using Newtonsoft.Json.Linq;
using System;

namespace AffairList
{
    public partial class ProfilesExportPicker : BaseForm
    {
        public ProfilesExportPicker(List<string> profiles, Settings settings) : base(settings)
        {
            InitializeComponent();
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
                string sourceFilePath = settings.listsDirectoryFullPath +
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

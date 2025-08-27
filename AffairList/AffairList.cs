using Newtonsoft.Json;

namespace AffairList
{
    public partial class AffairList : BaseForm
    {
        public static readonly NotifyIcon trayIcon = new NotifyIcon();
        private ContextMenuStrip _trayMenu = new ContextMenuStrip();
        private LoadTimeManager _loadTimeManager;

        public AffairList()
        {
            InitializeComponent();

            _trayMenu.Items.Add("Открыть", null, OnOpen!);
            _trayMenu.Items.Add("Выход", null, OnExit!);

            trayIcon.Text = "AffairList";
            trayIcon.Icon = Icon.ExtractAssociatedIcon("AffairListLogo.ico");

            trayIcon.ContextMenuStrip = _trayMenu;
            trayIcon.Visible = true;

            settings = new Settings();
            _loadTimeManager = new LoadTimeManager(settings);
        }
        private void OnOpen(object sender, EventArgs e)
        {
            Restart();
            ShowInTaskbar = true;
        }

        private void OnExit(object sender, EventArgs e) => Exit();

        private void CloseButton_Click(object sender, EventArgs e) => Exit();

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Gray;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Black;
        }

        private void NameBackground_MouseDown(object sender, MouseEventArgs e) => SetLastPoint(e);
        private void NameBackground_MouseMove(object sender, MouseEventArgs e) => MoveForm(e);
        private void AffairListLab_MouseMove(object sender, MouseEventArgs e) => MoveForm(e);
        private void AffairListLab_MouseDown(object sender, MouseEventArgs e) => SetLastPoint(e);

        private void OpenListButton_Click(object sender, EventArgs e)
        {
            if (!settings.CurrentListNotNull())
            {
                MessageBox.Show("Error, there is no list available");
                return;
            }
            CreateForm(new ToDoList(settings));
        }

        private void ClearListButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Are you sure to clear your affair list?",
            "Confirm window",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                if (!settings.CurrentListNotNull())
                {
                    MessageBox.Show("Error, list file does not exist");
                    return;
                }
                File.WriteAllText(settings.GetCurrentProfile(), "");
                MessageBox.Show("The list is cleared");
            }
        }

        private void ChangeListButton_Click(object sender, EventArgs e)
        {
            if (!settings.ListFilesAvailable())
            {
                DialogResult createDefault = MessageBox.Show("There are no profiles available," +
                    " do you want to add default?",
                    "Confirm Form",
                    MessageBoxButtons.YesNo);
                if (createDefault == DialogResult.No) return;

                settings.CreateDefaultList();
            }
            CreateForm(new ChangeListForm(settings));
        }

        private void ReplaceAffairListButton_Click(object sender, EventArgs e)
        {
            if (!settings.CurrentListNotNull())
            {
                MessageBox.Show("Error, there is no list available");
                return;
            }
            ToDoList list = new ToDoList(settings);
            list.GetAffairs().BackColor = Color.White;
            list.canReplace = true;
            CreateForm(list);
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            CreateForm(new SettingsForm(settings));
        }
        private void ChangeProfileButton_Click(object sender, EventArgs e)
        {
            CreateForm(new ChangeProfileForm(settings));
        }
        private void HotKeyButton_Click(object sender, EventArgs e)
        {
            CreateForm(new HotKeySettings(settings));
        }
        private void AffairList_FormClosing(object sender, FormClosingEventArgs e) => Exit();
        private void MinimizeButton_Click(object sender, EventArgs e) => MinimizeForm();

        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Gray;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = Color.Black;
        }

        private void AffairList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == settings.GetCloseKey())
            {
                Exit();
            }
        }

        private void AffairList_Load(object sender, EventArgs e)
        {
            Task.Run(() => _loadTimeManager.SaveTime());
            TopMost = true;
        }

        private void AffairList_Shown(object sender, EventArgs e) => TopMost = false;
    }
}

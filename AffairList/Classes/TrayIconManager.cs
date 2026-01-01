namespace AffairList
{
    public class TrayIconManager
    {
        public NotifyIcon TrayIcon { get; private set; }
        private ContextMenuStrip _trayMenu;
        private const string LogoName = "AffairListLogo.ico";
        private const string LogoUrl = "https://raw.githubusercontent.com/ivantr30/AffairList/refs/heads/master/AffairListLogo.ico";

        public TrayIconManager()
        {
            _trayMenu = new ContextMenuStrip();
            TrayIcon = new NotifyIcon();

            TrayIcon.BalloonTipTitle = "AffairList";
            TrayIcon.Text = "AffairList";
            if(File.Exists(LogoName)) TrayIcon.Icon = Icon.ExtractAssociatedIcon(LogoName);
            else Task.Run(DownloadIcon);

            TrayIcon.ContextMenuStrip = _trayMenu;
            TrayIcon.Visible = true;
        }
        private async Task DownloadIcon()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(LogoUrl);
                    response.EnsureSuccessStatusCode();
                    using (FileStream creatingStream = File.Create(LogoName))
                    {
                        response.Content.ReadAsStream().CopyTo(creatingStream);
                    }
                }
                TrayIcon.Icon = Icon.ExtractAssociatedIcon(LogoName);
            }
            catch
            {
                MessageBox.Show("Cannot download icon for tray");
            }
        }
        public void AddTrayMenuAction(string actionName, EventHandler action)
        {
            _trayMenu.Items.Add(actionName, null, action);
        }
        public void Dispose()
        {
            TrayIcon.Visible = false;
            TrayIcon.Dispose();
            _trayMenu?.Dispose();
        }
    }
}

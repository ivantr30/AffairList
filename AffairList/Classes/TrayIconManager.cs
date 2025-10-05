namespace AffairList
{
    public class TrayIconManager
    {
        public NotifyIcon TrayIcon { get; private set; }
        private ContextMenuStrip _trayMenu;

        public TrayIconManager()
        {
            _trayMenu = new ContextMenuStrip();
            TrayIcon = new NotifyIcon();

            TrayIcon.BalloonTipTitle = "AffairList";
            TrayIcon.Text = "AffairList";
            TrayIcon.Icon = Icon.ExtractAssociatedIcon("AffairListLogo.ico");

            TrayIcon.ContextMenuStrip = _trayMenu;
            TrayIcon.Visible = true;
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

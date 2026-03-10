namespace AffairList.Infrastructure.Classes;

public class TrayIconManager
{
    private static readonly HttpClient _httpClient = new();

    public NotifyIcon TrayIcon { get; private set; }
    private readonly ContextMenuStrip _trayMenu;
    private const string LogoName = "AffairListLogo.ico";
    private const string LogoUrl = "https://raw.githubusercontent.com/ivantr30/AffairList/refs/heads/master/AffairListLogo.ico";

    public TrayIconManager()
    {
        _trayMenu = new ContextMenuStrip();
        TrayIcon = new NotifyIcon
        {
            BalloonTipTitle = "AffairList",
            Text = "AffairList"
        };
        if (File.Exists(LogoName)) TrayIcon.Icon = Icon.ExtractAssociatedIcon(LogoName);
        else Task.Run(DownloadIcon);

        TrayIcon.ContextMenuStrip = _trayMenu;
        TrayIcon.Visible = true;
    }

    private async Task DownloadIcon()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(LogoUrl);
            response.EnsureSuccessStatusCode();

            using (FileStream creatingStream = File.Create(LogoName))
            {
                await response.Content.CopyToAsync(creatingStream);
            }

            TrayIcon.Icon = Icon.ExtractAssociatedIcon(LogoName);
        }
        catch
        {
            Console.WriteLine("Cannot download icon for tray");
        }
    }

    public void AddTrayMenuAction(string actionName, EventHandler action)
        => _trayMenu.Items.Add(actionName, null, action);

    public void Dispose()
    {
        TrayIcon.Visible = false;
        TrayIcon.Dispose();
        _trayMenu?.Dispose();
    }
}
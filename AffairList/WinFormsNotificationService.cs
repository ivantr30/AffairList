using AffairList.Core.Interfaces;

namespace AffairList.Classes;

public class WinFormsNotificationService(NotifyIcon trayIcon) : INotificationService
{
    public void ShowNotification(string title, string text, bool isWarning = false)
    {
        ToolTipIcon icon = isWarning ? ToolTipIcon.Warning : ToolTipIcon.Info;
        trayIcon.ShowBalloonTip(3000, title, text, icon);
    }
}
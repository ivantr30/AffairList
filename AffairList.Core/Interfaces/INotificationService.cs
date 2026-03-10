namespace AffairList.Core.Interfaces;

public interface INotificationService
{
    void ShowNotification(string title, string text, bool isWarning = false);
}
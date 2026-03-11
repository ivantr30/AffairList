using System.Text.Json;

using AffairList.Core;
using AffairList.Core.Interfaces;
using AffairList.Core.Models;
using AffairList.Core.Settings.Models;

namespace AffairList.Infrastructure.Settings;

public class LoadTimeManager(Settings settings)
{
    public readonly string LoadTimeFileFullPath = $@"{Settings.programDirectoryFolderFullPath}\loadtime.json";

    private LoadTimeModel _loadTime = null!;
    private FileLogger _fileLogger = null!;

    private INotificationService _notificationService = null!;

    public async Task Initialize(INotificationService notificationService)
    {
        _fileLogger = new FileLogger(Settings.logFileFullPath);
        _notificationService = notificationService;

        if (!File.Exists(LoadTimeFileFullPath))
        {
            await CreateLoadTimeFile();
            await WriteBaseTime();
            await _fileLogger.LogWarningAsync($"{DateTime.Now} LoadTimeFile wasn't present and was created");
        }
        try
        {
            _loadTime = JsonSerializer.Deserialize<LoadTimeModel>(File.ReadAllText(LoadTimeFileFullPath))!;
        }
        catch
        {
            await WriteBaseTime();
            await _fileLogger.LogWarningAsync(
                $"{DateTime.Now} LoadTimeFile wasn't in right format and was rewritten");
        }
    }

    public async Task Notificate()
    {
        if (!settings.DoesNotificate() || !ShouldNotificate()) return;
        await _fileLogger.LogInformationAsync($"{DateTime.Now} Starting notificating");

        Dictionary<string, bool> notifications = [];
        SaveTime();

        string[] profiles = Directory.GetFiles(Settings.listsDirectoryFullPath);
        foreach (var profile in profiles)
        {
            try
            {
                var content = File.ReadAllText(profile);
                var affairs = JsonSerializer.Deserialize<List<Affair>>(content) ?? [];

                foreach (var affair in affairs)
                {
                    if (!affair.Deadline.HasValue) continue;

                    int daysLeft = (affair.Deadline.Value.Date - DateTime.Now.Date).Days;
                    if (daysLeft > settings.GetNotificationDayDistance()) continue;

                    if (daysLeft == 0)
                    {
                        TimeSpan timeLeft = TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay;
                        notifications.Add($"{affair.Title} - осталось {timeLeft.Hours}ч {timeLeft.Minutes}м", false);
                    }
                    else if (daysLeft > 0)
                    {
                        notifications.Add($"{affair.Title} - осталось {daysLeft} дней", false);
                    }
                    else
                    {
                        notifications.Add($"{affair.Title} - просрочено", true);
                    }
                }
            }
            catch { }
        }

        foreach (KeyValuePair<string, bool> notification in notifications)
            _notificationService.ShowNotification("AffairList", notification.Key, notification.Value);

        await _fileLogger.LogInformationAsync($"{DateTime.Now} notified");
    }

    private bool ShouldNotificate()
        => GetPreviousLoadTime().Date != DateTime.Now.Date ||
        DateTime.Now.Hour - GetPreviousLoadTime().Hour >= settings.GetNotificationHourDistance();

    private async Task WriteBaseTime()
    {
        _loadTime = new LoadTimeModel();
        File.WriteAllText(LoadTimeFileFullPath, JsonSerializer.Serialize(_loadTime));
        await _fileLogger.LogInformationAsync($"{DateTime.Now} load time was set to base");
    }

    public void SaveTime()
    {
        SetPreviousLoadTime(DateTime.Now);
        File.WriteAllText(LoadTimeFileFullPath, JsonSerializer.Serialize(_loadTime));
    }

    public async Task CreateLoadTimeFile()
    {
        using (File.Create(LoadTimeFileFullPath)) { }
        await _fileLogger.LogInformationAsync($"{DateTime.Now} load time file was created");
    }

    public DateTime GetPreviousLoadTime()
        => _loadTime.PreviousLoadTime;

    public void SetPreviousLoadTime(DateTime time) => _loadTime.PreviousLoadTime = time;
}
using System.Text.Json;

using AffairList.Core;
using AffairList.Core.Interfaces;
using AffairList.Core.Settings.Models;

namespace AffairList.Infrastructure.Settings;

public class LoadTimeManager
{
    public readonly string LoadTimeFileFullPath;

    private const string _priorityTag = "<priority>";
    private const string _priorityWord = "\"Priority\"";
    private const string _deadlineTag = "<deadline>";

    private LoadTimeModel _loadTime = null!;

    private readonly Settings _settings;

    private FileLogger _fileLogger = null!;

    private INotificationService _notificationService = null!;

    public LoadTimeManager(Settings settings)
    {
        _settings = settings;
        LoadTimeFileFullPath = $@"{Settings.programDirectoryFolderFullPath}\loadtime.json";
    }

    public void Initialize(INotificationService notificationService)
    {
        _fileLogger = new FileLogger(Settings.logFileFullPath);

        _notificationService = notificationService;

        if (!LoadTimeFileExist())
        {
            CreateLoadTimeFile();
            WriteBaseTime();
            _fileLogger.LogWarning($"{DateTime.Now} LoadTimeFile wasn't present and was created");
        }
        try
        {
            _loadTime = JsonSerializer.Deserialize<LoadTimeModel>(File.ReadAllText(LoadTimeFileFullPath))!;
        }
        catch
        {
            WriteBaseTime();
            _fileLogger.LogWarning(
                $"{DateTime.Now} LoadTimeFile wasn't in right format and was rewritten");
        }
    }
    public void Notificate()
    {
        if (!_settings.DoesNotificate() || !ShouldNotificate()) return;

        _fileLogger.LogInformation($"{DateTime.Now} Starting notificating");

        Dictionary<string, bool> notifications = [];

        SaveTimeAsync().Wait();

        string[] profiles = Directory.GetFiles(Settings.listsDirectoryFullPath);

        for (int i = 0; i < profiles.Length; i++)
        {
            string[] affairs = File.ReadAllLines(profiles[i]);
            for (int j = 0; j < affairs.Length; j++)
            {
                if (!affairs[j].StartsWith(_deadlineTag)) continue;

                DateTime deadline = DateTime.Parse(affairs[j].Substring(10, 11));

                int daysLeft = (deadline.Date - DateTime.Now.Date).Days;
                if (daysLeft > _settings.GetNotificationDayDistance()) continue;

                if (daysLeft == 0)
                {
                    TimeSpan day = TimeSpan.FromHours(24);
                    TimeSpan now = TimeSpan.FromTicks(DateTime.Now.Ticks);
                    notifications.Add(AffairWithoutTags(affairs[j]) + $" - осталось {day - now}", false);
                }
                else if (daysLeft > 0)
                {
                    notifications.Add(AffairWithoutTags(affairs[j]) + $" - осталось {daysLeft} дней", false);
                }
                else
                {
                    notifications.Add( AffairWithoutTags(affairs[j]) + " - просрочено", true);
                }
            }
        }

        foreach (KeyValuePair<string, bool> notification in notifications)
            _notificationService.ShowNotification("AffairList", notification.Key, notification.Value == notification.Value);

        _fileLogger.LogInformation($"{DateTime.Now} notified");
    }

    private bool ShouldNotificate()
        => GetPreviousLoadTime().Date != DateTime.Now.Date ||
        DateTime.Now.Hour - GetPreviousLoadTime().Hour >= _settings.GetNotificationHourDistance();

    private static string AffairWithoutTags(string affair)
        => affair[10..].Replace(_priorityTag, _priorityWord);

    private void WriteBaseTime()
    {
        _loadTime = new LoadTimeModel();
        File.WriteAllText(LoadTimeFileFullPath, JsonSerializer.Serialize(_loadTime));
        _fileLogger.LogInformation($"{DateTime.Now} load time was set to base");
    }

    public async Task SaveTimeAsync()
    {
        SetPreviousLoadTime(DateTime.Now);
        await File.WriteAllTextAsync(LoadTimeFileFullPath, JsonSerializer.Serialize(_loadTime));
    }

    public bool LoadTimeFileExist()
        => File.Exists(LoadTimeFileFullPath);

    public void CreateLoadTimeFile()
    {
        using (File.Create(LoadTimeFileFullPath)) { }
        _fileLogger.LogInformation($"{DateTime.Now} load time file was created");
    }

    public DateTime GetPreviousLoadTime()
        => _loadTime.PreviousLoadTime;

    public void SetPreviousLoadTime(DateTime time) => _loadTime.PreviousLoadTime = time;
}
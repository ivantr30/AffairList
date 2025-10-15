using System.Text.Json;

namespace AffairList
{
    public class LoadTimeManager
    {
        public readonly string LoadTimeFileFullPath;

        private readonly string _priorityTag = "<priority>";
        private readonly string _priorityWord = "\"Priority\"";
        private readonly string _deadlineTag = "<deadline>";

        private LoadTimeModel _loadTime = null!;
        private readonly Settings _settings;
        private FileLogger _fileLogger = null!;
        private NotifyIcon _notification = null!;

        public LoadTimeManager(Settings settings, NotifyIcon notification)
        {
            _settings = settings;

            LoadTimeFileFullPath = $@"{_settings.programDirectoryFolderFullPath}\loadtime.json";

            Initialize(notification);
        }

        public void Initialize(NotifyIcon notification)
        {
            _fileLogger = new FileLogger(_settings.logFileFullPath);

            _notification = notification;

            if (!LoadTimeFileExist())
            {
                CreateLoadTimeFile();
                WriteBaseTime();
                _fileLogger.LogWarning($"{DateTime.Now} LoadTimeFile wasn't present and was created");
            }
            try
            {
                _loadTime = JsonSerializer.Deserialize<LoadTimeModel>
                        (File.ReadAllText(LoadTimeFileFullPath))!;
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

            foreach (var profile in Directory.GetFiles(_settings.listsDirectoryFullPath))
            {
                foreach (string affair in File.ReadAllLines(profile))
                {
                    if (!affair.StartsWith(_deadlineTag)) continue;

                    DateTime deadline = DateTime.Parse(affair.Substring(10, 11));

                    int daysLeft = (deadline.Date - DateTime.Now.Date).Days;
                    if (daysLeft > _settings.GetNotificationDayDistance()) continue;

                    if (daysLeft == 0)
                    {
                        TimeSpan day = new TimeSpan(24, 0, 0);
                        TimeSpan now = new TimeSpan(
                            DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        _notification.BalloonTipIcon = ToolTipIcon.Info;
                        _notification.BalloonTipText = AffairWithoutTags(affair) +
                            $" - осталось {day - now}";
                    }
                    else if (daysLeft > 0)
                    {
                        _notification.BalloonTipIcon = ToolTipIcon.Info;
                        _notification.BalloonTipText = AffairWithoutTags(affair) +
                            $" - осталось {daysLeft} дней";
                    }
                    else
                    {
                        _notification.BalloonTipIcon = ToolTipIcon.Warning;
                        _notification.BalloonTipText = AffairWithoutTags(affair) + " - просрочено";
                    }

                    _notification.ShowBalloonTip(1000);
                }
            }
            _fileLogger.LogInformation($"{DateTime.Now} notified");
        }
        private bool ShouldNotificate()
        {
            return (GetPreviousLoadTime().Date != DateTime.Now.Date) ||
            (DateTime.Now.Hour - GetPreviousLoadTime().Hour >= _settings.GetNotificationHourDistance());
        }
        private string AffairWithoutTags(string affair)
        {
            return affair[10..].Replace(_priorityTag, _priorityWord);
        }
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
        public void SaveTime()
        {
            SetPreviousLoadTime(DateTime.Now);
            File.WriteAllText(LoadTimeFileFullPath, JsonSerializer.Serialize(_loadTime));
        }

        public bool LoadTimeFileExist()
        {
            return File.Exists(LoadTimeFileFullPath);
        }
        public void CreateLoadTimeFile()
        {
            using (File.Create(LoadTimeFileFullPath)) { }
            _fileLogger.LogInformation($"{DateTime.Now} load time file was created");
        }
        public DateTime GetPreviousLoadTime()
        {
            return _loadTime.previousLoadTime;
        }
        public void SetPreviousLoadTime(DateTime time) => _loadTime.previousLoadTime = time;
    }
}

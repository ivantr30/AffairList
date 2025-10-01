using Newtonsoft.Json;

namespace AffairList
{
    public class LoadTimeManager
    {
        public readonly string LoadTimeFileFullPath;

        private string _priorityTag = "<priority>";
        private string _priorityWord = "\"Priority\"";
        private string _deadlineTag = "<deadline>";

        private LoadTimeModel _loadTime;

        private Settings _settings;

        private FileLogger _fileLogger;

        public LoadTimeManager(Settings settings)
        {
            _settings = settings;

            LoadTimeFileFullPath = $@"{_settings.programDirectoryFolderFullPath}\loadtime.json";

            _fileLogger = new FileLogger(_settings.logFileFullPath);

            Initialize();
        }

        public void Initialize()
        {
            if (!LoadTimeFileExist())
            {
                CreateLoadTimeFile();
                WriteBaseTime();
                _fileLogger.LogWarning($"{DateTime.Now} LoadTimeFile wasn't present and was created");
            }
            try
            {
                _loadTime = JsonConvert.DeserializeObject<LoadTimeModel>
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

            using NotifyIcon notification = new NotifyIcon()
            { Icon = SystemIcons.Exclamation, BalloonTipTitle = "AffairList", Visible = true };

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
                        TimeSpan now = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        notification.BalloonTipText = AffairWithoutTags(affair) +
                            $" - осталось {day - now}";
                    }
                    else if (daysLeft > 0)
                        notification.BalloonTipText = AffairWithoutTags(affair) +
                            $" - осталось {daysLeft} дней";
                    else
                        notification.BalloonTipText = AffairWithoutTags(affair) + " - просрочено";

                    notification.ShowBalloonTip(1000);
                }
            }
            _fileLogger.LogInformation($"{DateTime.Now} notified");
        }
        private bool ShouldNotificate()
        {
            return (GetPreviousLoadTime().Date != DateTime.Now.Date) || (DateTime.Now.Hour - GetPreviousLoadTime().Hour >= 8);
        }
        private string AffairWithoutTags(string affair)
        {
            return affair.Remove(0, 10).Replace(_priorityTag, _priorityWord);
        }
        private void WriteBaseTime()
        {
            _loadTime = new LoadTimeModel();
            File.WriteAllText(LoadTimeFileFullPath, JsonConvert.SerializeObject(_loadTime));
            _fileLogger.LogInformation($"{DateTime.Now} load time was set to base");
        }

        public async Task SaveTimeAsync()
        {
            SetPreviousLoadTime(DateTime.Now);
            await File.WriteAllTextAsync(LoadTimeFileFullPath, JsonConvert.SerializeObject(_loadTime));
        }
        public void SaveTime()
        {
            SetPreviousLoadTime(DateTime.Now);
            File.WriteAllText(LoadTimeFileFullPath, JsonConvert.SerializeObject(_loadTime));
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

using Newtonsoft.Json;
using System.Threading.Tasks;

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

        private NotifyIcon _notification;

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
        public async Task Notificate()
        {
            if (!_settings.DoesNotificate() || !ShouldNotificate()) return;

            _fileLogger.LogInformation($"{DateTime.Now} Starting notificating");

            _notification.BalloonTipIcon = ToolTipIcon.Info;

            await SaveTimeAsync();

            string[] profiles = Directory.GetFiles(_settings.listsDirectoryFullPath);

            for(int i = 0; i < profiles.Length; i++)
            {
                string[] affairs = File.ReadAllLines(profiles[i]);
                for(int j = 0; j < affairs.Length; j++)
                {
                    if (!affairs[j].StartsWith(_deadlineTag)) continue;

                    DateTime deadline = DateTime.Parse(affairs[j].Substring(10, 11));

                    int daysLeft = (deadline.Date - DateTime.Now.Date).Days;
                    if (daysLeft > _settings.GetNotificationDayDistance()) continue;

                    if (daysLeft == 0)
                    {
                        TimeSpan day = new TimeSpan(24, 0, 0);
                        TimeSpan now = new TimeSpan(
                            DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        _notification.BalloonTipIcon = ToolTipIcon.Info;
                        _notification.BalloonTipText = AffairWithoutTags(affairs[j]) +
                            $" - осталось {day - now}";
                    }
                    else if (daysLeft > 0)
                    {
                        _notification.BalloonTipIcon = ToolTipIcon.Info;
                        _notification.BalloonTipText = AffairWithoutTags(affairs[j]) +
                            $" - осталось {daysLeft} дней";
                    }
                    else
                    {
                        _notification.BalloonTipIcon = ToolTipIcon.Warning;
                        _notification.BalloonTipText = AffairWithoutTags(affairs[j]) + " - просрочено";
                    }
                    await Task.Delay(1500);
                    _notification.ShowBalloonTip(1000);
                    await Task.Delay(1500);
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

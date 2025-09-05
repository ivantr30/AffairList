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

        public LoadTimeManager(Settings settings)
        { 
            LoadTimeFileFullPath = $@"{settings._programDirectoryFolder}\loadtime.json";

            Task.Run(async () => await Initialize(settings));
        }

        public async Task Initialize(Settings settings)
        {
            if (!LoadTimeFileExist()) CreateLoadTimeFile();

            _loadTime = JsonConvert.DeserializeObject<LoadTimeModel>
                (await File.ReadAllTextAsync(LoadTimeFileFullPath))!;

            if (_loadTime == null) // Файл невалидный
            {
                await WriteBaseTime();
                await Initialize(settings);
                return;
            }

            await Notificate(settings);
        }
        public async Task Notificate(Settings settings)
        {
            if (!settings.DoesNotificate() || !ShouldNotificate()) return;

            using NotifyIcon notification = new NotifyIcon();
            notification.Icon = SystemIcons.Exclamation;
            notification.BalloonTipTitle = "AffairList";

            foreach (var profile in Directory.EnumerateFiles(settings.listsDirectoryFullPath))
            {
                await foreach (string affair in File.ReadLinesAsync(profile))
                {
                    if (!affair.StartsWith(_deadlineTag)) continue;

                    DateTime deadline = DateTime.Parse(affair.Substring(10, 11));

                    int daysLeft = (deadline.Date - DateTime.Now.Date).Days;
                    if (daysLeft > settings.GetNotificationDayDistance()) continue;

                    if (daysLeft == 0)
                    {
                        TimeSpan day = new TimeSpan(24, 0, 0);
                        TimeSpan now = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        notification.BalloonTipText = AffairWithoutTags(affair) + $" - осталось {day - now}";
                    }
                    else if (daysLeft > 0)
                        notification.BalloonTipText = AffairWithoutTags(affair) + $" - осталось {daysLeft} дней";
                    else
                        notification.BalloonTipText = AffairWithoutTags(affair) + " - просрочено";

                    notification.Visible = true;
                    notification.ShowBalloonTip(1);
                }
            }
        }

        private bool ShouldNotificate()
        {
            return (GetPreviousLoadTime().Date != DateTime.Now.Date) || (DateTime.Now.Hour - GetPreviousLoadTime().Hour >= 8);
        }
        private string AffairWithoutTags(string affair)
        {
            return affair.Remove(0, 10).Replace(_priorityTag, _priorityWord);
        }
        private async Task WriteBaseTime()
        {
            await File.WriteAllTextAsync(LoadTimeFileFullPath, 
                JsonConvert.SerializeObject(new LoadTimeModel()));
        }

        public async Task SaveTime()
        {
            SetPreviousLoadTime(DateTime.Now);
            await File.WriteAllTextAsync(LoadTimeFileFullPath, JsonConvert.SerializeObject(_loadTime));
        }

        public bool LoadTimeFileExist()
        {
            return File.Exists(LoadTimeFileFullPath);
        }
        public void CreateLoadTimeFile()
        {
            using (File.Create(LoadTimeFileFullPath)) { }
        }
        public DateTime GetPreviousLoadTime()
        {
            return _loadTime.previousLoadTime;
        }
        public void SetPreviousLoadTime(DateTime time) => _loadTime.previousLoadTime = time;
    }
}

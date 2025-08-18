using Newtonsoft.Json;

namespace AffairList
{
    public class LoadTimeManager
    {
        public readonly string LoadTimeFileFullPath;

        private string _priorityTag = "<priority>";
        private string _priorityWord = "\"Priority\"";
        private string _dealineTag = "<deadline>";

        private LoadTimeModel _loadTime;

        public LoadTimeManager(Settings settings)
        { 
            LoadTimeFileFullPath = Application.StartupPath + "loadtime.json";
            _loadTime = new LoadTimeModel();

            Task.Run(() => Initialize(settings));
        }
        public async Task Initialize(Settings settings)
        {
            if (!LoadTimeFileExist()) CreateLoadTimeFile();
            try
            {
                var readingLoadTimeFileResult = File.ReadAllTextAsync(LoadTimeFileFullPath);
                await readingLoadTimeFileResult;
                _loadTime = JsonConvert
                    .DeserializeObject<LoadTimeModel>(readingLoadTimeFileResult.Result)!;

                if (_loadTime == null) throw new Exception("loadfile is null");

                if ((GetPreviousLoadTime().Date != DateTime.Now.Date ||
                    DateTime.Now.Hour - GetPreviousLoadTime().Hour >= 8)
                    && settings.DoesNotificate())
                {
                    string[] profiles = Directory.GetFiles(settings.listsDirectoryFullPath);
                    foreach (var profile in profiles)
                    {
                        var readingFileResult = File.ReadAllLinesAsync(profile);
                        await readingFileResult;
                        string[] affairs = readingFileResult.Result;
                        foreach (string affair in affairs)
                        {
                            if (affair.StartsWith(_dealineTag))
                            {
                                DateTime deadline = DateTime.Parse(affair.Substring(10, 11));

                                int daysLeft = (deadline.Date - DateTime.Now.Date).Days;
                                if (daysLeft <= settings.GetNotificationDayDistance())
                                {
                                    using NotifyIcon notification = new NotifyIcon();
                                    notification.Icon = SystemIcons.Exclamation;
                                    notification.BalloonTipTitle = "AffairList";

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
                    }
                }
            }
            catch
            {
                WriteBaseTime();
                await Initialize(settings);
            }
        }

        private string AffairWithoutTags(string affair)
        {
            return affair.Substring(10).Replace(_priorityTag, _priorityWord);
        }
        private void WriteBaseTime()
        {
            File.WriteAllText(LoadTimeFileFullPath, JsonConvert.SerializeObject(new LoadTimeModel()));
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
        }
        public DateTime GetPreviousLoadTime()
        {
            return _loadTime.previousLoadTime;
        }
        public void SetPreviousLoadTime(DateTime time)
        {
            _loadTime.previousLoadTime = time;
        }
    }
}

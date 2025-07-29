using Newtonsoft.Json;

namespace AffairList
{
    public class LoadTimeManager
    {
        public readonly string LoadTimeFileFullPath;

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
                if ((GetPreviousLoadTime().Day != DateTime.Now.Day ||
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
                            if (affair.StartsWith("<deadline>"))
                            {
                                DateTime deadline = DateTime.Parse(affair.Substring(10, 11));
                                int daysLeft = 0;
                                daysLeft = deadline.Day - DateTime.Now.Day;
                                if (daysLeft <= settings.GetNotificationDayDistance())
                                {
                                    NotifyIcon notification = new NotifyIcon();
                                    notification.Icon = SystemIcons.Exclamation;
                                    notification.BalloonTipTitle = "AffairList";

                                    if (daysLeft < 1 && daysLeft >= 0)
                                        notification.BalloonTipText = affair.Substring(10) + $" - осталось {24 - DateTime.Now.Hour} часов";
                                    else if (daysLeft == 1)
                                        notification.BalloonTipText = affair.Substring(10) + " - остался 1 день";
                                    else if (daysLeft > 1)
                                        notification.BalloonTipText = affair.Substring(10) + $" - осталось {daysLeft} дней";
                                    else
                                        notification.BalloonTipText = affair.Substring(10) + " - просрочено";

                                    notification.Visible = true;
                                    notification.ShowBalloonTip(1000);
                                    notification.Dispose();
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

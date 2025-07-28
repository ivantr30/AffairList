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

            Initialize(settings);
        }
        public void Initialize(Settings settings)
        {
            if (!LoadTimeFileExist()) CreateLoadTimeFile();
            try
            {
                _loadTime = JsonConvert
                    .DeserializeObject<LoadTimeModel>(File.ReadAllText(LoadTimeFileFullPath))!;
                if (_loadTime == null) throw new Exception("loadfile is null");
                if (GetPreviousLoadTime().Day != DateTime.Now.Day && settings.DoesNotificate())
                {
                    string[] affairs = File.ReadAllLines(settings.GetCurrentProfile());
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

                                if(daysLeft < 1 && daysLeft >= 0)
                                    notification.BalloonTipText = affair.Substring(10) + $" - осталось {24 - DateTime.Now.Hour} часов";
                                else if(daysLeft == 1)
                                    notification.BalloonTipText = affair.Substring(10) + " - остался 1 день";
                                else if(daysLeft > 1)
                                    notification.BalloonTipText = affair.Substring(10) + $" - осталось {daysLeft} дней";
                                else
                                    notification.BalloonTipText = affair.Substring(10) + " - просрочено";

                                notification.Visible = true;
                                notification.ShowBalloonTip(3000);
                                notification.Dispose();

                            }
                        }
                    }
                }
            }
            catch
            {
                WriteBaseTime();
                Application.Restart();
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

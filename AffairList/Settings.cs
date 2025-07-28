using Microsoft.Win32;
using Newtonsoft.Json;
namespace AffairList
{
    public class Settings
    {
        private readonly string _defaultListFileFullPath;

        public readonly string listsDirectoryFullPath;
        public readonly string settingsFileFullPath;

        private const string _appName = "AffairList";
        private readonly string _exePath = Application.ExecutablePath;

        public readonly int width = Screen.PrimaryScreen.WorkingArea.Width;
        public readonly int height = Screen.PrimaryScreen.WorkingArea.Height;

        private SettingsModel _settings;

        public Settings()
        {
            listsDirectoryFullPath = Application.StartupPath + "profiles\\";
            _defaultListFileFullPath = listsDirectoryFullPath + "\\list.txt";
            settingsFileFullPath = Application.StartupPath + "settings.json";

            Initialize();
        }
        private void Initialize()
        {
            if(!SettingsFileExists()) CreateSettingsFile();
            if(!ListsDirectoryExists()) CreateListsDirectory();
            try
            {
                _settings = JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText(settingsFileFullPath))!;
                if (!File.Exists(GetCurrentProfile()))
                {
                    ChooseProfile();
                }
                if (DoesAutostart())
                {
                    EnableAutoStart(_appName, _exePath);
                }
                else
                {
                    DisableAutoStart(_appName);
                }
            }
            catch
            {
                WriteBaseSettings();
            }
        }
        public void WriteBaseSettings()
        {
            File.WriteAllText(settingsFileFullPath, JsonConvert.SerializeObject(new SettingsModel()));
        }
        private void ChooseProfile()
        {
            var profiles = Directory.GetFiles(listsDirectoryFullPath);
            if (profiles.Length > 0)
            {
                SetCurrentProfile(profiles[0]);
            }
        }
        private void EnableAutoStart(string appName, string exePath)
        {
            RegistryKey key = Registry.CurrentUser
                .OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true)!;

            key.SetValue(appName, $"\"{exePath}\"");
        }
        private void DisableAutoStart(string appName)
        {
            RegistryKey key = Registry.CurrentUser
                .OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true)!;

            key.DeleteValue(appName, false);
        }
        public bool ListFilesAvailable()
        {
            return Directory.GetFiles(listsDirectoryFullPath).Length > 0;
        }
        public bool SettingsFileExists()
        {
            return File.Exists(settingsFileFullPath);
        }
        public bool ListsDirectoryExists()
        {
            return Directory.Exists(listsDirectoryFullPath);
        }
        public bool CurrentListNotNull()
        {
            return File.Exists(GetCurrentProfile());
        }
        public void CreateFiles()
        {
            CreateSettingsFile();
            CreateListsDirectory();
        }
        public void CreateSettingsFile()
        {
            using (File.Create(settingsFileFullPath)) { }
            WriteBaseSettings();
        }
        public void CreateListsDirectory()
        {
            DirectoryInfo di = Directory.CreateDirectory(listsDirectoryFullPath);
        }
        public void CreateDefaultList()
        {
            using (File.Create(_defaultListFileFullPath)) { }
            SetCurrentProfile(_defaultListFileFullPath);
        }
        public void SaveSettings()
        {
            File.WriteAllText(settingsFileFullPath, JsonConvert.SerializeObject(_settings));
        }
        public void SetCurrentProfile(string fullPath)
        {
            _settings.currentListFileFullPath = fullPath;
        }
        public string GetCurrentProfile()
        {
            return _settings.currentListFileFullPath;
        }
        public void SetAutostart(bool autostart)
        {
            _settings.autostartState = autostart;
        }
        public bool DoesAutostart()
        {
            return _settings.autostartState;
        }
        public void SetAskToDelete(bool askToDelete)
        {
            _settings.askToDelete = askToDelete;
        }
        public bool DoesAskToDelete()
        {
            return _settings.askToDelete;
        }
        public void SetCloseKey(Keys key)
        {
            _settings.closeKey = key;
        }
        public Keys GetCloseKey()
        {
            return _settings.closeKey;
        }
        public void SetReturnKey(Keys key)
        {
            _settings.returnKey = key;
        }
        public Keys GetReturnKey()
        {
            return _settings.returnKey;
        }
        public void SetTextColor(Color color)
        {
            _settings.textColor = color;
        }
        public Color GetTextColor()
        {
            return _settings.textColor;
        }
        public void SetBgColor(Color color)
        {
            _settings.bgColor = color;
        }
        public Color GetBgColor()
        {
            return _settings.bgColor;
        }
        public void SetProfileX(int x)
        {
            _settings.x = x;
        }
        public int GetProfileX()
        {
            return _settings.x;
        }
        public void SetProfileY(int y)
        {
            _settings.y = y;
        }
        public int GetProfileY()
        {
            return _settings.y;
        }
        public bool DoesNotificate()
        {
            return _settings.DoesNotificate;
        }
        public void SetDoesNotificate(bool doesNotificate)
        {
            _settings.DoesNotificate = doesNotificate;
        }
        public int GetNotificationDayDistance()
        {
            return _settings.notificationDayDistance;
        }
        public void SetNotificationDayDistance(int notificationDistance)
        {
            _settings.notificationDayDistance = notificationDistance;
        }
    }
}

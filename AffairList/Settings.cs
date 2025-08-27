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

            Task.Run(() => Initialize());
        }
        private async Task Initialize()
        {
            if(!SettingsFileExists()) await CreateSettingsFile();
            if(!ListsDirectoryExists()) CreateListsDirectory();
            try
            {
                var settingsFileReadingResult = await File.ReadAllTextAsync(settingsFileFullPath);
                _settings = JsonConvert.DeserializeObject<SettingsModel>(settingsFileReadingResult)!;
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
                await WriteBaseSettings();
            }
        }
        public async Task WriteBaseSettings()
        {
            await File.WriteAllTextAsync(settingsFileFullPath, 
                JsonConvert.SerializeObject(new SettingsModel()));
        }
        private void ChooseProfile()
        {
            foreach (var profile in Directory.EnumerateFiles(listsDirectoryFullPath))
            {
                SetCurrentProfile(profile);
                break; // Получаем первый профиль и завершаем работу
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
            return Directory.EnumerateFiles(listsDirectoryFullPath).Count() > 0;
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
        public async Task CreateFiles()
        {
            await CreateSettingsFile();
            CreateListsDirectory();
        }
        public async Task CreateSettingsFile()
        {
            using (File.Create(settingsFileFullPath)) { }
            await WriteBaseSettings();
        }
        public void CreateListsDirectory()
        {
            Directory.CreateDirectory(listsDirectoryFullPath);
        }
        public void CreateDefaultList()
        {
            using (File.Create(_defaultListFileFullPath)) { }
            SetCurrentProfile(_defaultListFileFullPath);
        }
        public async Task SaveSettings()
        {
            await File.WriteAllTextAsync(settingsFileFullPath, JsonConvert.SerializeObject(_settings));
        }
        public void SetCurrentProfile(string fullPath) => _settings.currentListFileFullPath = fullPath;

        public string GetCurrentProfile()
        {
            return _settings.currentListFileFullPath;
        }
        public void SetAutostart(bool autostart) => _settings.autostartState = autostart;

        public bool DoesAutostart()
        {
            return _settings.autostartState;
        }
        public void SetAskToDelete(bool askToDelete) => _settings.askToDelete = askToDelete;

        public bool DoesAskToDelete()
        {
            return _settings.askToDelete;
        }
        public void SetCloseKey(Keys key) => _settings.closeKey = key;

        public Keys GetCloseKey()
        {
            return _settings.closeKey;
        }
        public void SetReturnKey(Keys key) => _settings.returnKey = key;

        public Keys GetReturnKey()
        {
            return _settings.returnKey;
        }

        public void SetTextColor(Color color) => _settings.textColor = color;

        public Color GetTextColor()
        {
            return _settings.textColor;
        }

        public void SetBgColor(Color color) => _settings.bgColor = color;

        public Color GetBgColor()
        {
            return _settings.bgColor;
        }
        public void SetProfileX(int x) => _settings.x = x;
        public int GetProfileX()
        {
            return _settings.x;
        }
        public void SetProfileY(int y) => _settings.y = y;
        public int GetProfileY()
        {
            return _settings.y;
        }
        public bool DoesNotificate()
        {
            return _settings.DoesNotificate;
        }
        public void SetDoesNotificate(bool doesNotificate) => _settings.DoesNotificate = doesNotificate;
        public uint GetNotificationDayDistance()
        {
            return _settings.notificationDayDistance;
        }
        public void SetNotificationDayDistance(uint notificationDistance)
        {
            _settings.notificationDayDistance = notificationDistance;
        }
    }
}

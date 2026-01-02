using Newtonsoft.Json;
using IWshRuntimeLibrary;
namespace AffairList
{
    public class Settings
    {
        private static readonly string _defaultListFileFullPath;

        public static readonly string programDirectoryFolderFullPath = Environment
            .GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\AffairList\";

        public static readonly string listsDirectoryFullPath;
        public static readonly string settingsFileFullPath;
        public static readonly string logFileFullPath;

        public readonly int screenWidth;
        public readonly int screenHeight;

        private SettingsModel _settings = null!;

        private FileLogger _fileLogger = null!;

        public Settings(bool initialize = true)
        {
            if (initialize)
            {
                screenWidth = Screen.PrimaryScreen!.WorkingArea.Width;
                screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

                _fileLogger = new FileLogger(logFileFullPath);

                Initialize();
            }
            else
            {
                _settings = new SettingsModel();
            }
        }
        static Settings()
        {
            if (AffairListDebug.DEBUG)
            {
                programDirectoryFolderFullPath += @"Debug\";
            }
            listsDirectoryFullPath = $@"{programDirectoryFolderFullPath}profiles\";
            _defaultListFileFullPath = $@"{listsDirectoryFullPath}list.txt";
            settingsFileFullPath = $@"{programDirectoryFolderFullPath}settings.json";
            logFileFullPath = $@"{programDirectoryFolderFullPath}logs.txt";
        }
        private void Initialize()
        {
            if (!ProgramDirectoryExists()) CreateProgramDirectory();
            if (!LogFileExists()) CreateLogFile();
            if (!SettingsFileExists()) CreateSettingsFile();
            if (!ListsDirectoryExists()) CreateListsDirectory();
            try
            {
                LoadSettings();
            }
            catch
            {
                WriteBaseSettings();
                _fileLogger.LogError($"{DateTime.Now} settings file was not valid");
            }
            if (!System.IO.File.Exists(GetCurrentProfile()) && 
                Directory.EnumerateFiles(listsDirectoryFullPath).FirstOrDefault() != default)
            {
                SelectFirstProfile();
            }
        }
        private void LoadSettings()
        {
            _settings = JsonConvert.DeserializeObject<SettingsModel>
                    (System.IO.File.ReadAllText(settingsFileFullPath))!;
            _fileLogger.LogInformation($"{DateTime.Now} settings were loaded succesfully");
        }
        public async Task WriteBaseSettingsAsync()
        {
            _settings = new SettingsModel();

            if (_settings.AutostartState) EnableAutoStart();
            else DisableAutoStart();

            await System.IO.File.WriteAllTextAsync(settingsFileFullPath,
                JsonConvert.SerializeObject(_settings));

            _fileLogger.LogInformation($"{DateTime.Now} settings were dropped to default succesfully");
        }
        public void WriteBaseSettings()
        {
            _settings = new SettingsModel();

            if(_settings.AutostartState) EnableAutoStart();
            else DisableAutoStart();

            System.IO.File.WriteAllText(settingsFileFullPath,
                JsonConvert.SerializeObject(_settings));

            _fileLogger.LogInformation($"{DateTime.Now} settings were dropped to default succesfully");
        }
        public async Task SelectFirstProfileAsync()
        {
            Directory.GetFiles(listsDirectoryFullPath);
            SetCurrentProfile(Directory.EnumerateFiles(listsDirectoryFullPath).First());
            await SaveSettingsAsync();
            _fileLogger.LogInformation($"{DateTime.Now} first profile was selected succesfully");
        }
        public void SelectFirstProfile()
        {
            Directory.GetFiles(listsDirectoryFullPath);
            SetCurrentProfile(Directory.EnumerateFiles(listsDirectoryFullPath).First());
            SaveSettings();
            _fileLogger.LogInformation($"{DateTime.Now} first profile was selected succesfully");
        }
        public void EnableAutoStart()
        {
            string shortcutPath = GetAutostartShortcut();

            if (System.IO.File.Exists(shortcutPath)) return;

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            
            shortcut.Description = "AffairList";
            shortcut.TargetPath = Application.ExecutablePath;    
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Arguments = "--autostart";

            shortcut.Save();
        }
        public void DisableAutoStart()
        {
            string shortcutPath = GetAutostartShortcut();
            if (System.IO.File.Exists(shortcutPath))
            {
                System.IO.File.Delete(shortcutPath);
            }
        }
        private string GetAutostartShortcut()
        {
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            return Path.Combine(startupFolderPath, Application.ProductName + ".lnk");
        }

        public bool ProgramDirectoryExists()
        {
            return Directory.Exists(programDirectoryFolderFullPath);
        }
        public bool ListFilesAvailable()
        {
            return Directory.EnumerateFiles(listsDirectoryFullPath).FirstOrDefault() != null;
        }
        public bool SettingsFileExists()
        {
            return System.IO.File.Exists(settingsFileFullPath);
        }
        public bool ListsDirectoryExists()
        {
            return Directory.Exists(listsDirectoryFullPath);
        }
        public bool LogFileExists()
        {
            return System.IO.File.Exists(logFileFullPath);
        }
        public bool CurrentListNotNull()
        {
            return System.IO.File.Exists(GetCurrentProfile());
        }
        public async Task CreateSettingsFileAsync()
        {
            using (System.IO.File.Create(settingsFileFullPath)) { }
            await WriteBaseSettingsAsync();
            _fileLogger.LogInformation(
                $"{DateTime.Now} settings was created");
        }
        public void CreateSettingsFile()
        {
            using (System.IO.File.Create(settingsFileFullPath)) { }
            _fileLogger.LogInformation(
                $"{DateTime.Now} settings was created");
            WriteBaseSettings();
        }
        public void CreateListsDirectory()
        {
            Directory.CreateDirectory(listsDirectoryFullPath);
            _fileLogger.LogInformation(
                $"{DateTime.Now} lists directory was created");
        }
        public async Task CreateDefaultListAsync()
        {
            using (System.IO.File.Create(_defaultListFileFullPath)) { }
            SetCurrentProfile(_defaultListFileFullPath);
            await SaveSettingsAsync();
            _fileLogger.LogInformation(
                $"{DateTime.Now} default list was created");
        }
        public void CreateProgramDirectory()
        {
            Directory.CreateDirectory(programDirectoryFolderFullPath);
        }
        public void CreateLogFile()
        {
            using(System.IO.File.Create(logFileFullPath)) { }
            _fileLogger.LogInformation(
                $"{DateTime.Now} log file was created");
        }
        public async Task SaveSettingsAsync()
        {
            await System.IO.File.WriteAllTextAsync(settingsFileFullPath, 
                JsonConvert.SerializeObject(_settings));
        }
        public void SaveSettings()
        {
            System.IO.File.WriteAllText(settingsFileFullPath, JsonConvert.SerializeObject(_settings));
        }
        public void SetCurrentProfile(string fullPath)
        {
            _settings.CurrentListFileFullPath = fullPath ?? "";
        }

        public string GetCurrentProfile()
        {
            return _settings.CurrentListFileFullPath;
        }
        public void SetAutostart(bool autostart, bool enableOrDisable = true)
        {
            _settings.AutostartState = autostart;
            if (enableOrDisable)
            {
                if (_settings.AutostartState) EnableAutoStart();
                else DisableAutoStart();
            }
        }

        public bool DoesAutostart()
        {
            return _settings.AutostartState;
        }
        public void SetAskToDelete(bool askToDelete) => _settings.AskToDelete = askToDelete;

        public bool DoesAskToDelete()
        {
            return _settings.AskToDelete;
        }
        public void SetCloseKey(Keys key) => _settings.CloseKey = key;

        public Keys GetCloseKey()
        {
            return _settings.CloseKey;
        }
        public void SetReturnKey(Keys key) => _settings.ReturnKey = key;

        public Keys GetReturnKey()
        {
            return _settings.ReturnKey;
        }

        public void SetTextColor(Color color) => _settings.TextColor = color;

        public Color GetTextColor()
        {
            return _settings.TextColor;
        }
        public void SetBgColor(Color color) => _settings.BgColor = color;

        public Color GetBgColor()
        {
            return _settings.BgColor;
        }
        public void SetProfileX(int x) => _settings.X = x;
        public int GetProfileX()
        {
            return _settings.X;
        }
        public void SetProfileY(int y) => _settings.Y = y;
        public int GetProfileY()
        {
            return _settings.Y;
        }
        public bool DoesNotificate()
        {
            return _settings.DoesNotificate;
        }
        public void SetDoesNotificate(bool doesNotificate) 
            => _settings.DoesNotificate = doesNotificate;
        public uint GetNotificationDayDistance()
        {
            return _settings.NotificationDayDistance;
        }
        public void SetNotificationDayDistance(uint notificationDayDistance)
        {
            _settings.NotificationDayDistance = notificationDayDistance;
        }
        public uint GetNotificationHourDistance()
        {
            return _settings.NotificationHourDistance;
        }
        public void SetNotificationHourDistance(uint notificationHourDistance)
        {
            _settings.NotificationHourDistance = notificationHourDistance;
        }
        /// <summary>
        /// For ToDoList.cs
        /// </summary>
        /// <returns></returns>
        public bool CanBeAlwaysReplaced()
        {
            return _settings.CanBeAlwaysReplaced;
        }
        /// <summary>
        /// For ToDoList.cs
        /// </summary>
        /// <returns></returns>
        public void SetCanBeAlwaysReplaced(bool canBeAlwaysReplace)
            => _settings.CanBeAlwaysReplaced = canBeAlwaysReplace;

        public Settings GetSettingsCopy()
        {
            Settings settingsCopy = new Settings(false);
            settingsCopy.SetProfileX(GetProfileX());
            settingsCopy.SetProfileY(GetProfileY());
            settingsCopy.SetNotificationHourDistance(GetNotificationHourDistance());
            settingsCopy.SetNotificationDayDistance(GetNotificationDayDistance()) ;
            settingsCopy.SetDoesNotificate(DoesNotificate());
            settingsCopy.SetCloseKey(GetCloseKey());
            settingsCopy.SetReturnKey(GetReturnKey());
            settingsCopy.SetAskToDelete(DoesAskToDelete());
            settingsCopy.SetTextColor(GetTextColor());
            settingsCopy.SetBgColor(GetBgColor());
            settingsCopy.SetCanBeAlwaysReplaced(CanBeAlwaysReplaced());
            settingsCopy.SetAutostart(DoesAutostart(), false);
            settingsCopy.SetCurrentProfile(GetCurrentProfile());
            return settingsCopy;
        }
        public void SetNewSettings(Settings newSettings)
        {
            SetProfileX(newSettings.GetProfileX());
            SetProfileY(newSettings.GetProfileY());
            SetAskToDelete(newSettings.DoesAskToDelete());
            SetAutostart(newSettings.DoesAutostart());
            SetDoesNotificate(newSettings.DoesNotificate());
            SetTextColor(newSettings.GetTextColor());
            SetBgColor(newSettings.GetBgColor());
            SetCurrentProfile(newSettings.GetCurrentProfile());
            SetCloseKey(newSettings.GetCloseKey());
            SetReturnKey(newSettings.GetReturnKey());
            SetNotificationDayDistance(newSettings.GetNotificationDayDistance());
            SetNotificationHourDistance(newSettings.GetNotificationHourDistance());
            SetCanBeAlwaysReplaced(newSettings.CanBeAlwaysReplaced());
        }
        public bool SettingsEqual(Settings otherSettings)
        {
            if (otherSettings == null) return false;

            if (GetProfileX() != otherSettings.GetProfileX()) return false;
            if (GetProfileY() != otherSettings.GetProfileY()) return false;
            if (DoesAskToDelete() != otherSettings.DoesAskToDelete()) return false;
            if (DoesAutostart() != otherSettings.DoesAutostart()) return false;
            if (GetNotificationDayDistance() != otherSettings.GetNotificationDayDistance()) return false;
            if (GetNotificationHourDistance() != otherSettings.GetNotificationHourDistance()) return false;
            if (CanBeAlwaysReplaced() != otherSettings.CanBeAlwaysReplaced()) return false;
            if (GetBgColor() != otherSettings.GetBgColor()) return false;
            if (GetTextColor() != otherSettings.GetTextColor()) return false;
            if (GetCloseKey() != otherSettings.GetCloseKey()) return false;
            if (GetCurrentProfile() != otherSettings.GetCurrentProfile()) return false;
            if (DoesNotificate() != otherSettings.DoesNotificate()) return false;
            if (GetReturnKey() != otherSettings.GetReturnKey()) return false;
            return true;
        }
    }
}

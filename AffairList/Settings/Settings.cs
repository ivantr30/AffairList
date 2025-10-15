using IWshRuntimeLibrary;
using System.Text.Json;

namespace AffairList
{
    public class Settings
    {
        private readonly string _defaultListFileFullPath;

        public readonly string programDirectoryFolderFullPath = Environment
            .GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\AffairList\";

        public readonly string listsDirectoryFullPath;
        public readonly string settingsFileFullPath;
        public readonly string logFileFullPath;

        public readonly int screenWidth;
        public readonly int screenHeight;

        private SettingsModel _settings = null!;

        private readonly FileLogger _fileLogger = null!;

        public Settings()
        {
            if (AffairListDebug.DEBUG)
            {
                programDirectoryFolderFullPath += @"Debug\";
            }
            listsDirectoryFullPath = $@"{programDirectoryFolderFullPath}profiles\";
            _defaultListFileFullPath = $@"{listsDirectoryFullPath}list.txt";
            settingsFileFullPath = $@"{programDirectoryFolderFullPath}settings.json";
            logFileFullPath = $@"{programDirectoryFolderFullPath}logs.txt";

            screenWidth = Screen.PrimaryScreen!.WorkingArea.Width;
            screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            _fileLogger = new FileLogger(logFileFullPath);

            Initialize();
        }
        private async Task InitializeAsync()
        {
            if (!LogFileExists()) CreateLogFile();
            if (!ProgramDirectoryExists()) CreateProgramDirectory();
            if (!SettingsFileExists()) await CreateSettingsFileAsync();
            if (!ListsDirectoryExists()) CreateListsDirectory();
            try
            {
                await LoadSettingsAsync();
            }
            catch
            {
                await WriteBaseSettingsAsync();
                _fileLogger.LogError($"{DateTime.Now} settings file was not valid");
            }
            if (!System.IO.File.Exists(GetCurrentProfile()))
            {
                await SelectFirstProfileAsync();
            }
        }
        private void Initialize()
        {
            if (!LogFileExists()) CreateLogFile();
            if (!ProgramDirectoryExists()) CreateProgramDirectory();
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
            if (!System.IO.File.Exists(GetCurrentProfile()))
            {
                SelectFirstProfile();
            }
        }
        private void LoadSettings()
        {
            _settings = JsonSerializer.Deserialize<SettingsModel>
                    (System.IO.File.ReadAllText(settingsFileFullPath))!;
            _fileLogger.LogInformation($"{DateTime.Now} settings were loaded succesfully");
        }
        private async Task LoadSettingsAsync()
        {
            _settings = JsonSerializer.Deserialize<SettingsModel>
                    (await System.IO.File.ReadAllTextAsync(settingsFileFullPath))!;
            _fileLogger.LogInformation($"{DateTime.Now} settings were loaded succesfully");
        }
        public async Task WriteBaseSettingsAsync()
        {
            _settings = new SettingsModel();

            if (_settings.autostartState) EnableAutoStart();
            else DisableAutoStart();

            await System.IO.File.WriteAllTextAsync(settingsFileFullPath,
                JsonSerializer.Serialize(_settings));

            _fileLogger.LogInformation($"{DateTime.Now} settings were dropped to default succesfully");
        }
        public void WriteBaseSettings()
        {
            _settings = new SettingsModel();

            if (_settings.autostartState) EnableAutoStart();
            else DisableAutoStart();

            System.IO.File.WriteAllText(settingsFileFullPath,
                JsonSerializer.Serialize(_settings));

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
        public static void EnableAutoStart()
        {
            string shortcutPath = GetAutostartShortcut();

            if (System.IO.File.Exists(shortcutPath)) return;

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            
            shortcut.Description = "AffairList";
            shortcut.TargetPath = Application.ExecutablePath;    
            shortcut.WorkingDirectory = Application.StartupPath;

            shortcut.Save();
        }
        public static void DisableAutoStart()
        {
            string shortcutPath = GetAutostartShortcut();
            if (System.IO.File.Exists(shortcutPath))
            {
                System.IO.File.Delete(shortcutPath);
            }
        }
        private static string GetAutostartShortcut()
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
        public void CreateDefaultList()
        {
            using (System.IO.File.Create(_defaultListFileFullPath)) { }
            SetCurrentProfile(_defaultListFileFullPath);
            SaveSettings();
            _fileLogger.LogInformation(
                $"{DateTime.Now} default list was created");
        }
        public void CreateProgramDirectory()
        {
            Directory.CreateDirectory(programDirectoryFolderFullPath);
            _fileLogger.LogInformation(
                $"{DateTime.Now} program directory was created");
        }
        public void CreateLogFile()
        {
            using(System.IO.File.Create(logFileFullPath))
            _fileLogger.LogInformation(
                $"{DateTime.Now} log file was created");
        }
        public async Task SaveSettingsAsync()
        {
            await System.IO.File.WriteAllTextAsync(settingsFileFullPath,
                JsonSerializer.Serialize(_settings));
        }
        public void SaveSettings()
        {
            System.IO.File.WriteAllText(settingsFileFullPath, JsonSerializer.Serialize(_settings));
        }
        public void SetCurrentProfile(string fullPath)
        {
            _settings.currentListFileFullPath = fullPath ?? "";
        }

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
        public void SetDoesNotificate(bool doesNotificate) 
            => _settings.DoesNotificate = doesNotificate;
        public uint GetNotificationDayDistance()
        {
            return _settings.notificationDayDistance;
        }
        public void SetNotificationDayDistance(uint notificationDayDistance)
        {
            _settings.notificationDayDistance = notificationDayDistance;
        }
        public uint GetNotificationHourDistance()
        {
            return _settings.notificationHourDistance;
        }
        public void SetNotificationHourDistance(uint notificationHourDistance)
        {
            _settings.notificationHourDistance = notificationHourDistance;
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
        public void SetAlwaysReplacing(bool canBeAlwaysReplace)
            => _settings.CanBeAlwaysReplaced = canBeAlwaysReplace;
    }
}

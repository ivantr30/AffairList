using AffairList.Services.Providers;
using IWshRuntimeLibrary;
using System.Text.Json;

namespace AffairList.Services.Managers
{
    public class Settings
    {
        private static readonly string _defaultListFileFullPath;

        public static readonly string programDirectoryFolderFullPath = Path.Combine(Environment
            .GetFolderPath(Environment.SpecialFolder.ApplicationData), "AffairList");

        public static readonly string listsDirectoryFullPath;
        public static readonly string settingsFileFullPath;
        public static readonly string logFileFullPath;

        public readonly int screenWidth;
        public readonly int screenHeight;

        public SettingsModel Data { get; set; }

        private FileLogger _fileLogger;

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
                Data = new SettingsModel();
            }
        }
        
        static Settings()
        {
            #if DEBUG
                programDirectoryFolderFullPath += @"Debug\";
            #endif
            listsDirectoryFullPath = Path.Combine(programDirectoryFolderFullPath, "profiles");
            _defaultListFileFullPath = Path.Combine(listsDirectoryFullPath, "list.txt");
            settingsFileFullPath = Path.Combine(programDirectoryFolderFullPath, "settings.json");
            logFileFullPath = Path.Combine(programDirectoryFolderFullPath, "logs.txt");
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
            SelectFirstProfile();
        }
        private bool ProfilesDirectoryEmpty()
        {
            return !(Directory.Exists(listsDirectoryFullPath) && Directory.EnumerateFiles(listsDirectoryFullPath).Any());
        }
        
        private void LoadSettings()
        {
            Data = SettingsProvider.LoadSettings(settingsFileFullPath);
            if(Data == null)
            {
                Data = new SettingsModel();
                _fileLogger.LogError($"{DateTime.Now} settings weren't loaded correctly and dropped to default");
            }
            Data.AutostartStateChanged += OnAutostartStateChanged;

            _fileLogger.LogInformation($"{DateTime.Now} settings were loaded succesfully");
        }
        public async Task WriteBaseSettingsAsync()
        {
            DropSettings();

            await SaveSettingsAsync().ConfigureAwait(false);

            _fileLogger.LogInformation($"{DateTime.Now} settings were dropped to default succesfully");
        }
        public void WriteBaseSettings()
        {
            DropSettings();

            SaveSettings();

            _fileLogger.LogInformation($"{DateTime.Now} settings were dropped to default succesfully");
        }
        private void DropSettings()
        {
            Data = new SettingsModel();
            Data.AutostartStateChanged += OnAutostartStateChanged;
            OnAutostartStateChanged();
        }
        public void SelectFirstProfile()
        {
            if (ProfilesDirectoryEmpty() || CurrentListExists()) return;
            try
            {
                Data.CurrentProfileFullPath = Directory.EnumerateFiles(listsDirectoryFullPath).First();
            }
            catch(Exception ex)
            {
                ShowCriticalErrorMessageAndExit("select first profile", ex);
            }

            SaveSettings();
            _fileLogger.LogInformation($"{DateTime.Now} first profile was selected succesfully");
        }
        public void EnableAutoStart()
        {
            string shortcutPath = GetAutostartShortcut();

            if (System.IO.File.Exists(shortcutPath)) return;

            try
            {
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

                shortcut.Description = "AffairList";
                shortcut.TargetPath = Application.ExecutablePath;
                shortcut.WorkingDirectory = Application.StartupPath;
                shortcut.Arguments = "--autostart";

                shortcut.Save();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error!!!, failed to create autostart shorcut, error code - {ex.ToString()}");
            }
        }
        public void DisableAutoStart()
        {
            string shortcutPath = GetAutostartShortcut();
            if (System.IO.File.Exists(shortcutPath))
            {
                try
                {
                    System.IO.File.Delete(shortcutPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error!!!, failed to delete autostart shorcut, error code - {ex.ToString()}");
                }
            }
        }
        private string GetAutostartShortcut()
        {
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            return Path.Combine(startupFolderPath, Application.ProductName + ".lnk");
        }
        public void SaveSettings()
        {
            SettingsProvider.SaveSettings(settingsFileFullPath, Data);
        }
        public async Task SaveSettingsAsync()
        {
            await SettingsProvider.SaveSettingsAsync(settingsFileFullPath, Data).ConfigureAwait(false);
        } 

        public bool ProgramDirectoryExists() => Directory.Exists(programDirectoryFolderFullPath);
        public bool ListFilesAvailable()
        {
            try
            {
                return Directory.EnumerateFiles(listsDirectoryFullPath).Any();
            }
            catch(Exception ex)
            {
                ShowCriticalErrorMessageAndExit("access lists directory", ex);
            }
            return false;
        }
        public bool SettingsFileExists() => System.IO.File.Exists(settingsFileFullPath);
        public bool ListsDirectoryExists() => Directory.Exists(listsDirectoryFullPath);
        public bool LogFileExists() => System.IO.File.Exists(logFileFullPath);
        public bool CurrentListExists() => System.IO.File.Exists(Data.CurrentProfileFullPath ?? "");
        public async Task CreateSettingsFileAsync()
        {
            CreateSettingsFile(false);

            await WriteBaseSettingsAsync().ConfigureAwait(false);
        }
        public void CreateSettingsFile(bool writeSettings = true)
        {
            try
            {
                using (System.IO.File.Create(settingsFileFullPath)) { }

                _fileLogger.LogInformation($"{DateTime.Now} settings file was created");
            }
            catch (Exception ex)
            {
                ShowCriticalErrorMessageAndExit("create settings file", ex);
            }

            if (writeSettings) WriteBaseSettings();
        }
        public void CreateListsDirectory()
        {
            try
            {
                Directory.CreateDirectory(listsDirectoryFullPath);
            }
            catch (Exception ex)
            {
                ShowCriticalErrorMessageAndExit("create lists directory", ex);
            }

            _fileLogger.LogInformation($"{DateTime.Now} lists directory was created");
        }
        public async Task CreateDefaultListAsync()
        {
            try
            {
                using (System.IO.File.Create(_defaultListFileFullPath)) { }
            }
            catch (Exception ex)
            {
                ShowCriticalErrorMessageAndExit("create default list", ex);
            }

            Data.CurrentProfileFullPath = _defaultListFileFullPath;
            await SaveSettingsAsync().ConfigureAwait(false);

            _fileLogger.LogInformation( $"{DateTime.Now} default list was created");
        }
        public void CreateProgramDirectory()
        {
            try
            {
                Directory.CreateDirectory(programDirectoryFolderFullPath);
            }
            catch(Exception ex)
            {
                ShowCriticalErrorMessageAndExit("create data directory", ex);
            }
        }
        public void CreateLogFile()
        {
            try
            {
                using (System.IO.File.Create(logFileFullPath)) { }
            }
            catch(Exception ex)
            {
                ShowCriticalErrorMessageAndExit("create log file", ex);
            }

            _fileLogger.LogInformation($"{DateTime.Now} log file was created");
        }
        public void OnAutostartStateChanged()
        {
            if (Data.AutostartState) EnableAutoStart();
            else DisableAutoStart();
        }
        private void ShowCriticalErrorMessageAndExit(string action, Exception ex)
        {
            MessageBox.Show($"Error!!!, failed to {action}, error code - {ex.ToString()}");
            MessageBox.Show("Exiting...");
            Environment.Exit(1);
        }

        public SettingsModel GetSettingsCopy()
        {
            return JsonSerializer.Deserialize<SettingsModel>(JsonSerializer.Serialize<SettingsModel>(Data))!;
        }
        public void SetNewSettings(SettingsModel newData)
        {
            Data.AutostartStateChanged -= OnAutostartStateChanged;
            Data = JsonSerializer.Deserialize<SettingsModel>(JsonSerializer.Serialize<SettingsModel>(newData))!;
            Data.AutostartStateChanged += OnAutostartStateChanged;
        }
        public bool SettingsEqual(SettingsModel otherData)
        {
            return JsonSerializer.Serialize<SettingsModel>(Data) == JsonSerializer.Serialize<SettingsModel>(otherData);
        }
    }
}

using Newtonsoft.Json;
using IWshRuntimeLibrary;
using System.Threading.Tasks;
namespace AffairList
{
    public class Settings
    {
        private readonly string _defaultListFileFullPath;

        public readonly string _programDirectoryFolder = Environment
            .GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\AffairList\";

        public readonly string listsDirectoryFullPath;
        public readonly string settingsFileFullPath;

        public readonly int screenWidth;
        public readonly int screenHeight;

        private SettingsModel _settings;

        public Settings()
        {
            listsDirectoryFullPath = $@"{_programDirectoryFolder}profiles\";
            _defaultListFileFullPath = $@"{listsDirectoryFullPath}list.txt";
            settingsFileFullPath = $@"{_programDirectoryFolder}settings.json";

            screenWidth = Screen.PrimaryScreen!.WorkingArea.Width;
            screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            InitializeAsync();
        }
        private async Task InitializeAsync()
        {
            if(!ProgramDirectoryExists()) CreateProgramDirectory();
            if(!SettingsFileExists()) await CreateSettingsFileAsync();
            if(!ListsDirectoryExists()) CreateListsDirectory();
            try
            {
                _settings = JsonConvert.DeserializeObject<SettingsModel>
                    (System.IO.File.ReadAllText(settingsFileFullPath))!;
            }
            catch
            {
                await WriteBaseSettingsAsync();
            }
            if (!System.IO.File.Exists(GetCurrentProfile()))
            {
                await SelectFirstProfileAsync();
            }
            if (DoesAutostart())
            {
                EnableAutoStart();
            }
            else
            {
                DisableAutoStart();
            }
        }
        public async Task WriteBaseSettingsAsync()
        {
            Task writingBaseSettings = System.IO.File.WriteAllTextAsync(settingsFileFullPath,
                JsonConvert.SerializeObject(new SettingsModel()));
            _settings = new SettingsModel();
            await writingBaseSettings;
        }
        public async Task SelectFirstProfileAsync()
        {
            SetCurrentProfile(Directory.EnumerateFiles(listsDirectoryFullPath).FirstOrDefault()!);
            await SaveSettingsAsync();
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
            return Directory.Exists(_programDirectoryFolder);
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
        public bool CurrentListNotNull()
        {
            return System.IO.File.Exists(GetCurrentProfile());
        }
        public async Task CreateSettingsFileAsync()
        {
            using (System.IO.File.Create(settingsFileFullPath)) { }
            await WriteBaseSettingsAsync();
        }
        public void CreateListsDirectory()
        {
            Directory.CreateDirectory(listsDirectoryFullPath);
        }
        public async Task CreateDefaultListAsync()
        {
            using (System.IO.File.Create(_defaultListFileFullPath)) { }
            SetCurrentProfile(_defaultListFileFullPath);
            await SaveSettingsAsync();
        }
        public void CreateProgramDirectory()
        {
            Directory.CreateDirectory(_programDirectoryFolder);
        }
        public async Task SaveSettingsAsync()
        {
            await System.IO.File.WriteAllTextAsync(settingsFileFullPath, 
                JsonConvert.SerializeObject(_settings));
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

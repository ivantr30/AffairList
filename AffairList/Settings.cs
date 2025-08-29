using Newtonsoft.Json;
using IWshRuntimeLibrary;
namespace AffairList
{
    public class Settings
    {
        private readonly string _defaultListFileFullPath;

        public readonly string _programDirectoryFolder = Environment
            .GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\AffairList\";

        public readonly string listsDirectoryFullPath;
        public readonly string settingsFileFullPath;

        public readonly int width = Screen.PrimaryScreen!.WorkingArea.Width;
        public readonly int height = Screen.PrimaryScreen.WorkingArea.Height;

        private SettingsModel _settings;

        public Settings()
        {
            listsDirectoryFullPath = $@"{_programDirectoryFolder}\profiles\";
            _defaultListFileFullPath = $@"{listsDirectoryFullPath}\list.txt";
            settingsFileFullPath = $@"{_programDirectoryFolder}\settings.json";

            Initialize();
        }
        private void Initialize()
        {
            if(!ProgramDirectoryExists()) CreateProgramDirectory();
            if(!SettingsFileExists()) CreateSettingsFile();
            if(!ListsDirectoryExists()) CreateListsDirectory();
            try
            {
                _settings = JsonConvert.DeserializeObject<SettingsModel>
                    (System.IO.File.ReadAllText(settingsFileFullPath))!;
                if (!System.IO.File.Exists(GetCurrentProfile()))
                {
                    ChooseProfile();
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
            catch
            {
                WriteBaseSettings();
            }
        }
        public async Task WriteBaseSettings()
        {
            await System.IO.File.WriteAllTextAsync(settingsFileFullPath, 
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
        private void EnableAutoStart()
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
        private void DisableAutoStart()
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
            return System.IO.File.Exists(_programDirectoryFolder);
        }
        public bool ListFilesAvailable()
        {
            return Directory.EnumerateFiles(listsDirectoryFullPath).Count() > 0;
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
        public async Task CreateFiles()
        {
            await CreateSettingsFile();
            CreateListsDirectory();
        }
        public async Task CreateSettingsFile()
        {
            using (System.IO.File.Create(settingsFileFullPath)) { }
            await WriteBaseSettings();
        }
        public void CreateListsDirectory()
        {
            Directory.CreateDirectory(listsDirectoryFullPath);
        }
        public void CreateDefaultList()
        {
            using (System.IO.File.Create(_defaultListFileFullPath)) { }
            SetCurrentProfile(_defaultListFileFullPath);
        }
        public void CreateProgramDirectory()
        {
            Directory.CreateDirectory(_programDirectoryFolder);
        }
        public async Task SaveSettings()
        {
            await System.IO.File.WriteAllTextAsync(settingsFileFullPath, JsonConvert.SerializeObject(_settings));
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

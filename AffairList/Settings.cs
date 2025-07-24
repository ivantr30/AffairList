using Microsoft.Win32;
using Newtonsoft.Json;
namespace AffairList
{
    public class Settings
    {
        public readonly string listsDirectoryFullPath;
        private readonly string defaultListFileFullPath;
        public readonly string settingsFileFullPath;

        private readonly string appName = "AffairList";
        private readonly string exePath = Application.ExecutablePath;

        public readonly int width = Screen.PrimaryScreen.WorkingArea.Width,
                   height = Screen.PrimaryScreen.WorkingArea.Height;

        private string currentParametr = "";

        private SettingsModel settings;

        public Settings()
        {
            settings = new SettingsModel();

            listsDirectoryFullPath = Application.StartupPath + "profiles\\";
            defaultListFileFullPath = listsDirectoryFullPath + "\\list.txt";
            settings.currentListFileFullPath = listsDirectoryFullPath + "\\list.txt";
            settingsFileFullPath = Application.StartupPath + "settings.json";
            Initialize();
        }
        private void Initialize()
        {
            if(!SettingsFileExists()) CreateSettingsFile();
            if(!ListsDirectoryExists()) CreateListsDirectory();
            try
            {
                settings = JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText(settingsFileFullPath));
                if (!File.Exists(GetCurrentProfile()))
                {
                    ChooseProfile();
                }
                if (DoesAutostart())
                {
                    EnableAutoStart(appName, exePath);
                }
                else
                {
                    DisableAutoStart(appName);
                }
            }
            catch
            {
                WriteBaseSettings();
            }
        }
        public void WriteBaseSettings()
        {
            File.WriteAllText(settingsFileFullPath, JsonConvert.SerializeObject(new BaseSettings()));
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
            using (File.Create(defaultListFileFullPath)) { }
            SetCurrentProfile(defaultListFileFullPath);
        }
        public void SaveSettings()
        {
            File.WriteAllText(settingsFileFullPath, JsonConvert.SerializeObject(settings));
        }
        public void SetCurrentProfile(string fullPath)
        {
            settings.currentListFileFullPath = fullPath;
        }
        public string GetCurrentProfile()
        {
            return settings.currentListFileFullPath;
        }
        public void SetAutostart(bool autostart)
        {
            settings.autostartState = autostart;
        }
        public bool DoesAutostart()
        {
            return settings.autostartState;
        }
        public void SetAskToDelete(bool askToDelete)
        {
            settings.askToDelete = askToDelete;
        }
        public bool DoesAskToDelete()
        {
            return settings.askToDelete;
        }
        public void SetCloseKey(Keys key)
        {
            settings.closeKey = key;
        }
        public Keys GetCloseKey()
        {
            return settings.closeKey;
        }
        public void SetReturnKey(Keys key)
        {
            settings.returnKey = key;
        }
        public Keys GetReturnKey()
        {
            return settings.returnKey;
        }
        public void SetTextColor(Color color)
        {
            settings.textColor = color;
        }
        public Color GetTextColor()
        {
            return settings.textColor;
        }
        public void SetBgColor(Color color)
        {
            settings.bgColor = color;
        }
        public Color GetBgColor()
        {
            return settings.bgColor;
        }
        public void SetProfileX(int x)
        {
            settings.x = x;
        }
        public int GetProfileX()
        {
            return settings.x;
        }
        public void SetProfileY(int y)
        {
            settings.y = y;
        }
        public int GetProfileY()
        {
            return settings.y;
        }
    }
}

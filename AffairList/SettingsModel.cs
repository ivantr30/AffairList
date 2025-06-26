
using Microsoft.Win32;

namespace AffairList
{
    public class SettingsModel
    {
        public readonly string listsDirectoryFullPath;
        private readonly string defaultListFileFullPath;

        public string currentListFileFullPath;
        public readonly string settingsFileFullPath;

        public Color textColor = Color.MediumSpringGreen;
        public Color bgtextColor = Color.Black;

        private readonly Color basetextColor = Color.MediumSpringGreen;
        private readonly Color basebgtextColor = Color.Black;

        public Keys closeKey = Keys.F7;
        public Keys returnKey = Keys.F6;

        public bool autostartState = true;
        public bool askToDelete = true;

        public int x, y;
        public int width = Screen.PrimaryScreen.WorkingArea.Width,
                   height = Screen.PrimaryScreen.WorkingArea.Height;

        private string[] settingLines;
        private string currentParametr = "";

        public SettingsModel()
        {
            listsDirectoryFullPath = Application.StartupPath + "profiles\\";
            defaultListFileFullPath = listsDirectoryFullPath + "\\list.txt";
            currentListFileFullPath = listsDirectoryFullPath + "\\list.txt";
            settingsFileFullPath = Application.StartupPath + "settings.txt";
            Initialize();
        }
        private void Initialize()
        {
            settingLines = File.ReadAllLines(settingsFileFullPath);
            if (settingLines.Length == 0)
            {
                WriteBaseSettings();
                return;
            }

            for (int i = 0; i < settingLines.Length; i++)
            {
                string line = settingLines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                int colonIndex = line.IndexOf(':');
                if (colonIndex < 0) continue;

                string key = line.Substring(0, colonIndex).Trim();
                string value = line.Substring(colonIndex + 1).Trim();
                string[] parameters = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                switch (key)
                {
                    case "x,y":
                        currentParametr = key;
                        x = int.Parse(parameters[0]);
                        y = int.Parse(parameters[1]);
                        break;

                    case "autostarts":
                        currentParametr = key;
                        autostartState = parameters[0].Contains("True");
                        if (autostartState)
                            EnableAutoStart("AffairList", Application.ExecutablePath);
                        else
                            DisableAutoStart("AffairList");
                        break;

                    case "textColor":
                        currentParametr = key;
                        textColor = Color.FromArgb(255,
                            int.Parse(parameters[0]),
                            int.Parse(parameters[1]),
                            int.Parse(parameters[2]));
                        break;

                    case "backTextColor":
                        currentParametr = key;
                        bgtextColor = Color.FromArgb(255,
                            int.Parse(parameters[0]),
                            int.Parse(parameters[1]),
                            int.Parse(parameters[2]));
                        break;

                    case "askToDelete":
                        currentParametr = key;
                        askToDelete = bool.Parse(parameters[0]);
                        break;

                    case "currentProfile":
                        currentParametr = key;
                        currentListFileFullPath = value;
                        if (!File.Exists(currentListFileFullPath))
                        {
                            ChooseProfile();
                        }
                        break;

                    case "closeKey":
                        currentParametr = key;
                        closeKey = (Keys)Enum.Parse(typeof(Keys), parameters[0]);
                        break;

                    case "returnKey":
                        currentParametr = key;
                        returnKey = (Keys)Enum.Parse(typeof(Keys), parameters[0]);
                        break;

                    default:
                        continue;
                }

                settingLines[i] = $"{currentParametr}:{value}";
            }

            File.WriteAllLines(settingsFileFullPath, settingLines);
        }
        public void WriteBaseSettings()
        {
            File.WriteAllText(settingsFileFullPath,
                $"x,y: {width - width / 6} {(height + height / 10) / 90}\n" +
                $"textColor: {basetextColor.R} {basetextColor.G} {basetextColor.B}\n" +
                $"backTextColor: {basebgtextColor.R} {basebgtextColor.G} {basebgtextColor.B}\n" +
                "autostarts: true\n" +
                "askToDelete: true\n" +
                "currentProfile: \n" +
                "closeKey: F7\n" +
                "returnKey: F6\n");
        }
        private void ChooseProfile()
        {
            var profiles = Directory.GetFiles(listsDirectoryFullPath);
            if (profiles.Length > 0)
            {
                currentListFileFullPath = profiles[0];
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
            return File.Exists(currentListFileFullPath);
        }
        public void CreateFiles()
        {
            CreateSettingsFile();
            CreateListsDirectory();
        }
        public void CreateSettingsFile()
        {
            if (!SettingsFileExists())
            {
                using (File.Create(settingsFileFullPath)) { }
                WriteBaseSettings();
            }
        }
        public void CreateListsDirectory()
        {
            if (!ListsDirectoryExists())
            {
                DirectoryInfo di = Directory.CreateDirectory(listsDirectoryFullPath);
            }
        }
        public void CreateDefaultList()
        {
            using (File.Create(defaultListFileFullPath)) { }
            currentListFileFullPath = defaultListFileFullPath;
        }
        public void SaveSettings()
        {
            if (autostartState) EnableAutoStart("AffairList", Application.ExecutablePath);
            else DisableAutoStart("AffairList");

            for (int i = 0; i < settingLines.Length; i++)
            {
                string line = settingLines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                string key = line.Contains(":")
                    ? line.Substring(0, line.IndexOf(':')).Trim()
                    : string.Empty;

                switch (key)
                {
                    case "x,y":
                        currentParametr = key;
                        settingLines[i] = $"{currentParametr}: {x} {y}";
                        break;

                    case "textColor":
                        currentParametr = key;
                        settingLines[i] = $"{currentParametr}: {textColor.R} {textColor.G} {textColor.B}";
                        break;

                    case "backTextColor":
                        currentParametr = key;
                        settingLines[i] = $"{currentParametr}: {bgtextColor.R} {bgtextColor.G} {bgtextColor.B}";
                        break;

                    case "autostarts":
                        currentParametr = key;
                        settingLines[i] = $"{currentParametr}: {autostartState}";
                        break;

                    case "askToDelete":
                        currentParametr = key;
                        settingLines[i] = $"{currentParametr}: {askToDelete}";
                        break;
                    case "currentProfile":
                        currentParametr = key;
                        settingLines[i] = $"{currentParametr}: {currentListFileFullPath}";
                        break;

                    default:
                        if (line.StartsWith("closeKey:"))
                        {
                            currentParametr = "closeKey";
                        }
                        else if (line.StartsWith("returnKey:"))
                        {
                            currentParametr = "returnKey";
                        }
                        break;
                }
            }

            File.WriteAllLines(settingsFileFullPath, settingLines);
        }
        public void SaveParametr<T>(string parametr, T firstValue, T secondValue)
        {
            SaveParametr(parametr, firstValue + " " + secondValue);
        }
        public void SaveParametr<T>(string parametr, T value)
        {
            for (int i = 0; i < settingLines.Length; i++)
            {
                if (settingLines[i].StartsWith(parametr))
                {
                    settingLines[i] = parametr + ": " + value;
                    break;
                }
            }
            File.WriteAllLines(settingsFileFullPath, settingLines);
        }
    }
}

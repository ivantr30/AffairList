using Microsoft.Win32;
namespace AffairList
{
    public class Config
    {
        public static string listsDirectoryFullPath = Application.StartupPath + "profiles";
        public static string currentListFileFullPath = listsDirectoryFullPath + "\\list.txt";
        public static string defaultListFileFullPath = listsDirectoryFullPath + "\\list.txt";
        public static string settingsFileFullPath = Application.StartupPath + "settings.txt";

        public static Color textColor = Color.MediumSpringGreen;
        public static Color bgtextColor = Color.Black;
        private static Color basetextColor = Color.MediumSpringGreen;
        private static Color basebgtextColor = Color.Black;

        public static Keys closeKey = Keys.F7;
        public static Keys returnKey = Keys.F6;

        public static bool isConfirmed = true;
        public static bool musicState = true;
        public static bool autostartState = true;
        public static bool askToDelete = true;

        public static int currentVolume = 0;
        public static int x, y;
        public static int width = Screen.PrimaryScreen.WorkingArea.Width,
                          height = Screen.PrimaryScreen.WorkingArea.Height;

        public static Point lastPoint;

        private static string[] settingLines;
        private static string currentParametr = "";

        public static void EnableAutoStart(string appName, string exePath)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue(appName, $"\"{exePath}\"");
        }
        public static void DisableAutoStart(string appName)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.DeleteValue(appName, false);
        }
        public static void Exit()
        {
            AffairList.trayIcon.Visible = false;
            Application.Exit();
        }
        public static void Restart()
        {
            AffairList.trayIcon.Visible = false;
            Application.Restart();
        }
        public static void CreateFiles()
        {
            if (!File.Exists(settingsFileFullPath))
            {
                using (File.Create(settingsFileFullPath))
                {

                }
                WriteBaseSettings();
            }
            if (!Directory.Exists(listsDirectoryFullPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(listsDirectoryFullPath);
            }
            settingLines = File.ReadAllLines(settingsFileFullPath);
        }
        public static void ChooseProfile()
        {
            var profiles = Directory.GetFiles(listsDirectoryFullPath);
            if (profiles.Length > 0)
            {
                currentListFileFullPath = profiles[0];
            }
        }
        public static void IfSettingsFileExists()
        {
            if (!File.Exists(settingsFileFullPath))
            {
                MessageBox.Show("Error, settings file does not exist");
                return;
            }
        }
        public static void IfListFileExists()
        {
            if (!File.Exists(settingsFileFullPath))
            {
                MessageBox.Show("Error, list file does not exist");
                return;
            }
        }
        public static void WriteBaseSettings()
        {
            File.WriteAllText(settingsFileFullPath,
                $"x,y: {width - width / 6} {(height + height / 10) / 90}" +
                    "\nmusicOn: true" +
                    $"\ntextColor: {basetextColor.R} {basetextColor.G} {basetextColor.B}" +
                    $"\nbackTextColor: {basebgtextColor.R} {basebgtextColor.G} {basebgtextColor.B}\n" +
                        "musicVolume: 35\n" +
                        "autostarts: true\n" +
                        "askToDelete: true\n" +
                        "currentProfile: \n"+
                        "closeKey: F7\n" +
                        "returnKey: F6\n");
        }
        public static void ConfigureSettings()
        {
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

                    case "musicOn":
                        currentParametr = key;
                        musicState = bool.Parse(parameters[0]);
                        break;

                    case "musicVolume":
                        currentParametr = key;
                        currentVolume = int.Parse(parameters[0]);
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
        public static void SaveSettings()
        {
            isConfirmed = true;
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

                    case "musicOn":
                        currentParametr = key;
                        settingLines[i] = $"{currentParametr}: {musicState}";
                        break;

                    case "autostarts":
                        currentParametr = key;
                        settingLines[i] = $"{currentParametr}: {autostartState}";
                        break;

                    case "musicVolume":
                        currentParametr = key;
                        settingLines[i] = $"{currentParametr}: {currentVolume}";
                        break;

                    case "askToDelete":
                        currentParametr = key;
                        settingLines[i] = $"{currentParametr}: {askToDelete}";
                        break;

                    // Для параметров без значения (например, currentProfile)
                    default:
                        if (line.StartsWith("currentProfile:"))
                        {
                            currentParametr = "currentProfile";
                            // Сохраняем без изменений или добавляем логику при необходимости
                        }
                        else if (line.StartsWith("closeKey:"))
                        {
                            currentParametr = "closeKey";
                            // Сохраняем без изменений
                        }
                        else if (line.StartsWith("returnKey:"))
                        {
                            currentParametr = "returnKey";
                            // Сохраняем без изменений
                        }
                        break;
                }
            }

            File.WriteAllLines(settingsFileFullPath, settingLines);
        }
        public static void SaveParametr<T>(string parametr, T firstValue, T secondValue)
        {
            for (int i = 0; i < settingLines.Length; i++)
            {
                if (settingLines[i].StartsWith(parametr))
                {
                    settingLines[i] = parametr + ": " + firstValue + " " + secondValue;
                    break;
                }
            }
            File.WriteAllLines(settingsFileFullPath, settingLines);
        }
    }
}

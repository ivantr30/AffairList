using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffairList
{
    public class Config
    {
        public static string listFileFullPath = Application.StartupPath + "\\list.txt";
        public static string settingsFileFullPath = Application.StartupPath + "\\settings.txt";

        public static Color textColor = Color.MediumSpringGreen;
        public static Color bgtextColor = Color.Black;
        private static Color basetextColor = Color.MediumSpringGreen;
        private static Color basebgtextColor = Color.Black;

        public static bool isConfirmed = true;
        public static bool musicState = true;
        public static bool autostartState = true;
        public static bool askToDelete = true;

        public static int currentVolume = 0;
        public static int x, y;
        public static int width = Screen.PrimaryScreen.WorkingArea.Width,
                          height = Screen.PrimaryScreen.WorkingArea.Height;

        public static Point lastPoint;

        private static string[] settingLines = File.ReadAllLines(settingsFileFullPath);
        private static string currentParametr = "";

        private static void EnableAutoStart(string appName, string exePath)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue(appName, $"\"{exePath}\"");
        }
        private static void DisableAutoStart(string appName)
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
            if (!File.Exists(listFileFullPath))
            {
                using (File.Create(listFileFullPath))
                {

                }
            }
            if (!File.Exists(settingsFileFullPath))
            {
                using (File.Create(settingsFileFullPath))
                {

                }
                WriteBaseSettings();
            }
        }
        public static void WriteBaseSettings()
        {
            File.WriteAllText(settingsFileFullPath, "x,y: \nmusicOn: true" +
                    $"\ntextColor: {basetextColor.R} {basetextColor.G} {basetextColor.B}" +
                    $"\nbackTextColor: {basebgtextColor.R} {basebgtextColor.G} {basebgtextColor.B}\n" +
                        "musicVolume: 35\n" +
                        "autostarts: true\n" +
                        "askToDelete: true\n");
        }
        public static void ConfigureSettings()
        {
            if(settingLines.Length == 0)
            {
                WriteBaseSettings();
                return;
            }
            for (int i = 0; i < settingLines.Length; i++)
            {
                string[] lineSplitted = settingLines[i].Substring((settingLines[i] + " ").IndexOf(":") + 1)
                    .Trim().Split(" ");
                if (settingLines[i].Contains("x,y"))
                {
                    currentParametr = "x,y";
                    if(lineSplitted[0].Length < 2)
                    {
                        x = width;
                        y = height + height / 10;
                        lineSplitted[0] = x - x / 6 + " " + y / 90;
                    }
                    else
                    {
                        x = int.Parse(lineSplitted[0]);
                        y = int.Parse(lineSplitted[1]);
                    }
                }
                if (settingLines[i].Contains("autostarts"))
                {
                    currentParametr = "autostarts";
                    if (lineSplitted[0].Contains("True"))
                    {
                        autostartState = true;
                        EnableAutoStart("AffairList", Application.ExecutablePath);
                    }
                    else
                    {
                        autostartState = false;
                        DisableAutoStart("AffairList");
                    }
                }
                if (settingLines[i].Contains("textColor"))
                {
                    currentParametr = "textColor";
                    textColor = Color.FromArgb(255, int.Parse(lineSplitted[0]), int.Parse(lineSplitted[1]),
                        int.Parse(lineSplitted[2]));
                }
                if (settingLines[i].Contains("backTextColor:"))
                {
                    currentParametr = "backTextColor";
                    bgtextColor = Color.FromArgb(255, int.Parse(lineSplitted[0]), int.Parse(lineSplitted[1]),
                        int.Parse(lineSplitted[2]));
                }
                if (settingLines[i].Contains("askToDelete:"))
                {
                    currentParametr = "askToDelete";
                    askToDelete = bool.Parse(lineSplitted[0]);
                }
                if (settingLines[i].Contains("musicOn:"))
                {
                    currentParametr = "musicOn";
                    musicState = bool.Parse(lineSplitted[0]);
                }
                if (settingLines[i].Contains("musicVolume:"))
                {
                    currentParametr = "musicVolume";
                    currentVolume = int.Parse(lineSplitted[0]);
                }
                settingLines[i] = currentParametr + ":" + string.Join(" ", lineSplitted);
            }
            File.WriteAllLines(settingsFileFullPath, settingLines);
        }
        public static void SaveSettings()
        {
            Config.isConfirmed = true;
            for (int i = 0; i < settingLines.Length; i++)
            {
                if (settingLines[i].Contains("x,y:"))
                {
                    currentParametr = "x,y";
                    settingLines[i] =  currentParametr + ": " + x + " " + y;
                }
                if (settingLines[i].Contains("textColor"))
                {
                    currentParametr = "textColor";
                    settingLines[i] = currentParametr + ": " + textColor.R + " " + textColor.G
                        + " " + textColor.B;
                }
                if (settingLines[i].Contains("backTextColor"))
                {
                    currentParametr = "backTextColor";
                    settingLines[i] = currentParametr + ": " + bgtextColor.R + " " + bgtextColor.G
                        + " " + bgtextColor.B;
                }
                if (settingLines[i].Contains("musicOn"))
                {
                    currentParametr = "musicOn";
                    settingLines[i] = currentParametr + ": " + musicState;
                }
                if (settingLines[i].Contains("autostarts"))
                {
                    currentParametr = "autostarts";
                    settingLines[i] = currentParametr + ": " + autostartState;
                }
                if (settingLines[i].Contains("musicVolume"))
                {
                    currentParametr = "musicVolume";
                    settingLines[i] = currentParametr + ": " + currentVolume;
                }
                if (settingLines[i].Contains("askToDelete"))
                {
                    currentParametr = "askToDelete";
                    settingLines[i] = currentParametr + ": " + askToDelete;
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

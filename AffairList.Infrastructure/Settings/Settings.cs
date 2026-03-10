using System.Text.Json;

using AffairList.Core;
using AffairList.Core.Settings.Models;

using IWshRuntimeLibrary;

namespace AffairList.Infrastructure.Settings;

public class Settings
{
    private static readonly string _defaultListFileFullPath;

    public static readonly string programDirectoryFolderFullPath = Environment
        .GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AffairList\";

    public static readonly string listsDirectoryFullPath;
    public static readonly string settingsFileFullPath;
    public static readonly string logFileFullPath;

    public readonly int screenWidth;
    public readonly int screenHeight;

    private SettingsModel _settings = null!;

    private readonly FileLogger _fileLogger = null!;

    public Settings(bool initialize = true)
    {
        if (initialize)
        {
            screenWidth = Screen.PrimaryScreen!.WorkingArea.Width;
            screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            _fileLogger = new FileLogger(logFileFullPath);
            _ = Initialize();
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

    private async Task Initialize()
    {
        if (!ProgramDirectoryExists()) CreateProgramDirectory();
        if (!LogFileExists()) CreateLogFile();
        if (!SettingsFileExists()) await CreateSettingsFileAsync();
        if (!ListsDirectoryExists()) CreateListsDirectory();
        try
        {
            LoadSettings();
        }
        catch
        {
            await WriteBaseSettingsAsync();
            _fileLogger.LogError($"{DateTime.Now} settings file was not valid");
        }
        if (!System.IO.File.Exists(GetCurrentProfile()) &&
            Directory.EnumerateFiles(listsDirectoryFullPath).FirstOrDefault() != default)
        {
            await SelectFirstProfileAsync();
        }
    }

    private void LoadSettings()
    {
        _settings = JsonSerializer.Deserialize<SettingsModel>(System.IO.File.ReadAllText(settingsFileFullPath))!;
        _fileLogger.LogInformation($"{DateTime.Now} settings were loaded succesfully");
    }

    public async Task WriteBaseSettingsAsync()
    {
        _settings = new SettingsModel();

        if (_settings.AutostartState) EnableAutoStart();
        else DisableAutoStart();

        await System.IO.File.WriteAllTextAsync(settingsFileFullPath, JsonSerializer.Serialize(_settings));

        _fileLogger.LogInformation($"{DateTime.Now} settings were dropped to default succesfully");
    }

    public async Task SelectFirstProfileAsync(bool logInfo = true)
    {
        Directory.GetFiles(listsDirectoryFullPath);
        SetCurrentProfile(Directory.EnumerateFiles(listsDirectoryFullPath).First());
        await SaveSettingsAsync();
        if (logInfo) _fileLogger.LogInformation($"{DateTime.Now} first profile was selected succesfully");
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
        shortcut.Arguments = "--autostart";

        shortcut.Save();
    }

    public static void DisableAutoStart()
    {
        string shortcutPath = GetAutostartShortcut();
        if (System.IO.File.Exists(shortcutPath))
            System.IO.File.Delete(shortcutPath);
    }

    private static string GetAutostartShortcut()
    {
        string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        return Path.Combine(startupFolderPath, Application.ProductName + ".lnk");
    }

    public static bool ProgramDirectoryExists()
        => Directory.Exists(programDirectoryFolderFullPath);

    public static bool ListFilesAvailable()
        => Directory.EnumerateFiles(listsDirectoryFullPath).FirstOrDefault() != null;

    public static bool SettingsFileExists()
        => System.IO.File.Exists(settingsFileFullPath);

    public static bool ListsDirectoryExists()
        => Directory.Exists(listsDirectoryFullPath);

    public static bool LogFileExists()
        => System.IO.File.Exists(logFileFullPath);

    public bool CurrentListNotNull()
        => System.IO.File.Exists(GetCurrentProfile());

    public async Task CreateSettingsFileAsync()
    {
        using (System.IO.File.Create(settingsFileFullPath)) { }
        await WriteBaseSettingsAsync();
        _fileLogger.LogInformation(
            $"{DateTime.Now} settings was created");
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

    public static void CreateProgramDirectory()
        => Directory.CreateDirectory(programDirectoryFolderFullPath);

    public void CreateLogFile()
    {
        using (System.IO.File.Create(logFileFullPath)) { }
        _fileLogger.LogInformation(
            $"{DateTime.Now} log file was created");
    }

    public async Task SaveSettingsAsync()
        => await System.IO.File.WriteAllTextAsync(settingsFileFullPath, JsonSerializer.Serialize(_settings));

    public void SetCurrentProfile(string fullPath)
        => _settings.CurrentListFileFullPath = fullPath ?? "";
    public string GetCurrentProfile()
        => _settings.CurrentListFileFullPath;
    
    public void SetAutostart(bool autostart, bool enableOrDisable = true)
    {
        _settings.AutostartState = autostart;
        if (enableOrDisable)
        {
            if (_settings.AutostartState) EnableAutoStart();
            else DisableAutoStart();
        }
    }
    public bool GetAutostart() => _settings.AutostartState;
    
    public void SetAskToDelete(bool askToDelete) => _settings.AskToDelete = askToDelete;
    public bool GetAskToDelete() => _settings.AskToDelete;
    
    public void SetCloseKey(Keys key) => _settings.CloseKey = key;
    public Keys GetCloseKey() => _settings.CloseKey;
    
    public void SetReturnKey(Keys key) => _settings.ReturnKey = key;
    public Keys GetReturnKey() => _settings.ReturnKey;

    public void SetTextColor(Color color) => _settings.TextColor = color;
    public Color GetTextColor() => _settings.TextColor;
    
    public void SetBgColor(Color color) => _settings.BgColor = color;
    public Color GetBgColor() => _settings.BgColor;

    public void SetProfileX(int x) => _settings.X = x;
    public int GetProfileX() => _settings.X;

    public void SetProfileY(int y) => _settings.Y = y;
    public int GetProfileY() => _settings.Y;

    public bool DoesNotificate() => _settings.DoesNotificate;
    public void SetDoesNotificate(bool doesNotificate)
        => _settings.DoesNotificate = doesNotificate;

    public uint GetNotificationDayDistance()
        => _settings.NotificationDayDistance;
    public void SetNotificationDayDistance(uint notificationDayDistance)
        => _settings.NotificationDayDistance = notificationDayDistance;

    public uint GetNotificationHourDistance()
        => _settings.NotificationHourDistance;
    public void SetNotificationHourDistance(uint notificationHourDistance)
        => _settings.NotificationHourDistance = notificationHourDistance;

    public bool GetCanBeAlwaysReplaced()
        => _settings.CanBeAlwaysReplaced;
    public void SetCanBeAlwaysReplaced(bool canBeAlwaysReplace)
        => _settings.CanBeAlwaysReplaced = canBeAlwaysReplace;

    public Settings GetSettingsCopy()
    {
        Settings settingsCopy = new Settings(false);
        settingsCopy.SetProfileX(GetProfileX());
        settingsCopy.SetProfileY(GetProfileY());
        settingsCopy.SetNotificationHourDistance(GetNotificationHourDistance());
        settingsCopy.SetNotificationDayDistance(GetNotificationDayDistance());
        settingsCopy.SetDoesNotificate(DoesNotificate());
        settingsCopy.SetCloseKey(GetCloseKey());
        settingsCopy.SetReturnKey(GetReturnKey());
        settingsCopy.SetAskToDelete(GetAskToDelete());
        settingsCopy.SetTextColor(GetTextColor());
        settingsCopy.SetBgColor(GetBgColor());
        settingsCopy.SetCanBeAlwaysReplaced(GetCanBeAlwaysReplaced());
        settingsCopy.SetAutostart(GetAutostart(), false);
        settingsCopy.SetCurrentProfile(GetCurrentProfile());
        return settingsCopy;
    }
    public void SetNewSettings(Settings newSettings)
    {
        SetProfileX(newSettings.GetProfileX());
        SetProfileY(newSettings.GetProfileY());
        SetAskToDelete(newSettings.GetAskToDelete());
        SetAutostart(newSettings.GetAutostart());
        SetDoesNotificate(newSettings.DoesNotificate());
        SetTextColor(newSettings.GetTextColor());
        SetBgColor(newSettings.GetBgColor());
        SetCurrentProfile(newSettings.GetCurrentProfile());
        SetCloseKey(newSettings.GetCloseKey());
        SetReturnKey(newSettings.GetReturnKey());
        SetNotificationDayDistance(newSettings.GetNotificationDayDistance());
        SetNotificationHourDistance(newSettings.GetNotificationHourDistance());
        SetCanBeAlwaysReplaced(newSettings.GetCanBeAlwaysReplaced());
    }
    public bool SettingsEqual(Settings otherSettings)
    {
        if (otherSettings == null) return false;
        if (GetProfileX() != otherSettings.GetProfileX()) return false;
        if (GetProfileY() != otherSettings.GetProfileY()) return false;
        if (GetAskToDelete() != otherSettings.GetAskToDelete()) return false;
        if (GetAutostart() != otherSettings.GetAutostart()) return false;
        if (GetNotificationDayDistance() != otherSettings.GetNotificationDayDistance()) return false;
        if (GetNotificationHourDistance() != otherSettings.GetNotificationHourDistance()) return false;
        if (GetCanBeAlwaysReplaced() != otherSettings.GetCanBeAlwaysReplaced()) return false;
        if (GetBgColor() != otherSettings.GetBgColor()) return false;
        if (GetTextColor() != otherSettings.GetTextColor()) return false;
        if (GetCloseKey() != otherSettings.GetCloseKey()) return false;
        if (GetCurrentProfile() != otherSettings.GetCurrentProfile()) return false;
        if (DoesNotificate() != otherSettings.DoesNotificate()) return false;
        if (GetReturnKey() != otherSettings.GetReturnKey()) return false;
        return true;
    }
}
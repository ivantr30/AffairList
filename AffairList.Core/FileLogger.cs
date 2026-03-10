using AffairList.Core.Enums;

namespace AffairList.Core;

public class FileLogger(string filePath)
{
    public void Log(LogType logType, string msg)
    {
        if (!File.Exists(filePath)) throw new FileNotFoundException();
        File.AppendAllText(filePath, $"{logType} | {msg} \n");
    }

    public void LogInformation(string msg)
        => Log(LogType.Information, msg);

    public void LogWarning(string msg)
        => Log(LogType.Warning, msg);

    public void LogError(string msg)
        => Log(LogType.Error, msg);

    public async Task LogAsync(LogType logType, string msg)
    {
        if (!File.Exists(filePath)) throw new FileNotFoundException();
        await File.WriteAllTextAsync(filePath, $"{logType} | {msg}");
    }

    public async Task LogInformationAsync(string msg)
        => await LogAsync(LogType.Information, msg);

    public async Task LogWarningAsync(string msg)
        => await LogAsync(LogType.Warning, msg);

    public async Task LogErrorAsync(string msg)
        => await LogAsync(LogType.Error, msg);
}
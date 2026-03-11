using AffairList.Core.Enums;

namespace AffairList.Core;

public class FileLogger(string filePath)
{
    private async Task LogAsync(LogType logType, string msg)
        => await File.AppendAllTextAsync(filePath, $"{logType} | {msg}\n");

    public async Task LogInformationAsync(string msg)
        => await LogAsync(LogType.Information, msg);

    public async Task LogWarningAsync(string msg)
        => await LogAsync(LogType.Warning, msg);

    public async Task LogErrorAsync(string msg)
        => await LogAsync(LogType.Error, msg);
}
namespace AffairList
{
    public enum LogType
    {
        Information,
        Warning,
        Error
    }
    public class FileLogger
    {
        private string _filePath;
        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }
        public void Log(LogType logType, string msg)
        {
            if (!File.Exists(_filePath)) throw new FileNotFoundException();
            File.AppendAllText(_filePath, $"{logType.ToString()} | {msg} \n");
        }
        public void LogInformation(string msg)
        {
            Log(LogType.Information, msg);
        }
        public void LogWarning(string msg)
        {
            Log(LogType.Warning, msg);
        }
        public void LogError(string msg)
        {
            Log(LogType.Error, msg);
        }
        public async Task LogAsync(LogType logType, string msg)
        {
            if (!File.Exists(_filePath)) throw new FileNotFoundException();
            await File.WriteAllTextAsync(_filePath, $"{logType.ToString()} | {msg}");
        }
        public async Task LogInformationAsync(string msg)
        {
            await LogAsync(LogType.Information, msg);
        }
        public async Task LogWarningAsync(string msg)
        {
            await LogAsync(LogType.Warning, msg);
        }
        public async Task LogErrorAsync(string msg)
        {
            await LogAsync(LogType.Error, msg);
        }
    }
}

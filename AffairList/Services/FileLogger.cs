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
        private SemaphoreSlim _semaphoreSlim = new(1,1);
        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }
        public void Log(LogType logType, string msg)
        {
            if (!File.Exists(_filePath))
            {
                try
                {
                    File.Create(_filePath);
                }
                catch(Exception e)
                {
                    MessageBox.Show($"Log file cannot be created, error - {e.ToString()}, exiting...");
                    Application.Exit();
                }
            }
            _semaphoreSlim.Wait();
            try
            { 
                File.AppendAllText(_filePath, $"{logType.ToString()} | {msg} \n");
            }
            finally
            {
                _semaphoreSlim.Release();
            }
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
            await _semaphoreSlim.WaitAsync();
            try
            {
                await File.AppendAllTextAsync(_filePath, $"{logType.ToString()} | {msg}").ConfigureAwait(false);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
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

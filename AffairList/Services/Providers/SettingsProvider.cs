using System.Text.Json;

namespace AffairList.Services.Providers
{
    public static class SettingsProvider
    {
        private static SemaphoreSlim _settingsFileLocker;
        static SettingsProvider()
        {
            _settingsFileLocker = new SemaphoreSlim(1,1);
        }
        public static async Task SaveSettingsAsync(string fileToSave, SettingsModel settings)
        {
            await _settingsFileLocker.WaitAsync();
            try
            {
                await File.WriteAllTextAsync(fileToSave, JsonSerializer.Serialize(settings)).ConfigureAwait(false);
            }
            finally
            {
                _settingsFileLocker.Release();
            }
        }
        public static void SaveSettings(string fileToSave, SettingsModel settings)
        {
            _settingsFileLocker.Wait();
            try
            {
                File.WriteAllText(fileToSave, JsonSerializer.Serialize(settings));
            }
            finally
            {
                _settingsFileLocker.Release();
            }
        }
        public static SettingsModel LoadSettings(string fileToLoadFrom)
        {
            _settingsFileLocker.Wait();
            try
            {
                return JsonSerializer.Deserialize<SettingsModel>(File.ReadAllText(fileToLoadFrom));
            }
            catch(Exception)
            {
                MessageBox.Show("Error!!!, cannot load settings from file, settings are dropped to defaults");
                return new SettingsModel();
            }
            finally
            {
                _settingsFileLocker.Release();
            }
        }
    }
}

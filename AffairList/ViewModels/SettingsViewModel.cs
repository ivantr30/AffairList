using System.Threading.Tasks;

using AffairList.Infrastructure.Settings;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AffairList.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly Settings _settings;

    [ObservableProperty]
    public partial bool AutostartState { get; set; }

    [ObservableProperty]
    public partial bool AskToDelete { get; set; }

    [ObservableProperty]
    public partial bool DoesNotificate { get; set; }

    public SettingsViewModel(Settings settings)
    {
        _settings = settings;
        LoadCurrentSettings();
    }

    private void LoadCurrentSettings()
    {
        AutostartState = _settings.GetAutostart();
        AskToDelete = _settings.GetAskToDelete();
        DoesNotificate = _settings.DoesNotificate();
    }

    [RelayCommand]
    private async Task SaveSettingsAsync()
    {
        _settings.SetAutostart(AutostartState, true);
        _settings.SetAskToDelete(AskToDelete);
        _settings.SetDoesNotificate(DoesNotificate);
        await _settings.SaveSettingsAsync();
    }
}
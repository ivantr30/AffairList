using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

using AffairList.Infrastructure.Settings;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AffairList.ViewModels;

public partial class ProfilesViewModel : ObservableObject
{
    private readonly Settings _settings;

    [ObservableProperty]
    public partial ObservableCollection<string> Profiles { get; set; } = [];

    [ObservableProperty]
    public partial string? SelectedProfile { get; set; }

    [ObservableProperty]
    public partial string NewProfileName { get; set; } = string.Empty;

    public ProfilesViewModel(Settings settings)
    {
        _settings = settings;
        LoadProfiles();
    }

    [RelayCommand]
    private void LoadProfiles()
    {
        Profiles.Clear();
        if (!Settings.ListsDirectoryExists()) return;

        foreach (var profile in Directory.EnumerateFiles(Settings.listsDirectoryFullPath))
        {
            Profiles.Add(Path.GetFileName(profile));
        }

        string current = Path.GetFileName(_settings.GetCurrentProfile());
        if (Profiles.Contains(current))
        {
            SelectedProfile = current;
        }
    }

    [RelayCommand]
    private async Task ChangeProfileAsync()
    {
        if (string.IsNullOrEmpty(SelectedProfile)) return;

        string fullPath = Path.Combine(Settings.listsDirectoryFullPath, SelectedProfile);
        _settings.SetCurrentProfile(fullPath);
        await _settings.SaveSettingsAsync();
    }

    [RelayCommand]
    private async Task CreateProfileAsync()
    {
        if (string.IsNullOrWhiteSpace(NewProfileName)) return;

        string newFileName = NewProfileName.EndsWith(".txt") ? NewProfileName : NewProfileName + ".txt";
        string fullPath = Path.Combine(Settings.listsDirectoryFullPath, newFileName);

        if (!File.Exists(fullPath))
        {
            using (File.Create(fullPath)) { }
            Profiles.Add(newFileName);
            SelectedProfile = newFileName;
            await ChangeProfileAsync();
            NewProfileName = string.Empty;
        }
    }
}
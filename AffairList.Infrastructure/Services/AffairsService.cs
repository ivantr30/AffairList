using AffairList.Core.Interfaces;

namespace AffairList.Infrastructure.Services;

public class AffairsService(Settings.Settings settings) : IAffairsService
{
    public async Task<List<string>> LoadAffairsAsync()
    {
        if (!settings.CurrentListNotNull()) return [];
        var lines = await File.ReadAllLinesAsync(settings.GetCurrentProfile());
        return [.. lines];
    }

    public async Task SaveAffairsAsync(List<string> affairs)
    {
        if (!settings.CurrentListNotNull()) return;
        await File.WriteAllLinesAsync(settings.GetCurrentProfile(), affairs);
    }

    public async Task<string> AddAffairAsync(string affair)
    {
        if (string.IsNullOrWhiteSpace(affair)) return string.Empty;
        string inputText = char.ToUpper(affair[0]) + affair[1..];
        await File.AppendAllTextAsync(settings.GetCurrentProfile(), inputText + "\n");
        return inputText;
    }

    public async Task<bool> DeleteAffairAsync(string affair)
    {
        var lines = await LoadAffairsAsync();

        int index = lines.IndexOf(affair);
        if (index == -1) return false;

        lines.RemoveAt(index);
        await SaveAffairsAsync(lines);
        return true;
    }

    public async Task<string> RenameAffairAsync(string oldAffair, string newAffair)
    {
        var lines = await LoadAffairsAsync();

        int index = lines.IndexOf(oldAffair);
        if (index == -1) return string.Empty;

        lines[index] = newAffair;

        await SaveAffairsAsync(lines);
        return newAffair;
    }
}
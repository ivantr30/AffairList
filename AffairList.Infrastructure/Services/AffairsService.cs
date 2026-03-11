using System.Text.Json;

using AffairList.Core.Interfaces;
using AffairList.Core.Models;

namespace AffairList.Infrastructure.Services;

public class AffairsService(Settings.Settings settings) : IAffairsService
{
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public async Task<List<Affair>> LoadAffairsAsync()
    {
        if (!settings.CurrentListNotNull()) return [];
        var content = await File.ReadAllTextAsync(settings.GetCurrentProfile());
        if (string.IsNullOrWhiteSpace(content)) return [];

        try
        {
            return JsonSerializer.Deserialize<List<Affair>>(content, _jsonOptions) ?? [];
        }
        catch
        {
            var lines = content.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
            var list = new List<Affair>();
            foreach (var line in lines)
            {
                var affair = new Affair();
                string temp = line.Trim();

                if (temp.EndsWith("<priority>"))
                {
                    affair.IsPriority = true;
                    temp = temp.Replace("<priority>", "").Trim();
                }
                if (temp.StartsWith("<deadline>") && temp.Length > 21)
                {
                    if (DateTime.TryParse(temp.AsSpan(10, 10), out DateTime dt))
                        affair.Deadline = dt;
                    temp = temp[21..].Trim();
                }

                affair.Title = temp;
                list.Add(affair);
            }

            await SaveAffairsAsync(list);
            return list;
        }
    }

    public async Task SaveAffairsAsync(List<Affair> affairs)
    {
        if (!settings.CurrentListNotNull()) return;
        var json = JsonSerializer.Serialize(affairs, _jsonOptions);
        await File.WriteAllTextAsync(settings.GetCurrentProfile(), json);
    }

    public async Task<Affair?> AddAffairAsync(Affair affair)
    {
        if (string.IsNullOrWhiteSpace(affair.Title)) return null;
        affair.Title = char.ToUpper(affair.Title[0]) + affair.Title[1..];

        var affairs = await LoadAffairsAsync();
        affairs.Add(affair);
        await SaveAffairsAsync(affairs);
        return affair;
    }

    public async Task<bool> DeleteAffairAsync(Affair affair)
    {
        var affairs = await LoadAffairsAsync();
        int index = affairs.FindIndex(a => a.Id == affair.Id);
        if (index == -1) return false;

        affairs.RemoveAt(index);
        await SaveAffairsAsync(affairs);
        return true;
    }

    public async Task<Affair?> UpdateAffairAsync(Affair affair)
    {
        var affairs = await LoadAffairsAsync();
        int index = affairs.FindIndex(a => a.Id == affair.Id);
        if (index == -1) return null;

        affairs[index] = affair;
        await SaveAffairsAsync(affairs);
        return affair;
    }
}
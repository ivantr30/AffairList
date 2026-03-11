using AffairList.Core.Models;

namespace AffairList.Core.Interfaces;

public interface IAffairsService
{
    Task<List<Affair>> LoadAffairsAsync();
    Task SaveAffairsAsync(List<Affair> affairs);
    Task<Affair?> AddAffairAsync(Affair affair);
    Task<bool> DeleteAffairAsync(Affair affair);
    Task<Affair?> UpdateAffairAsync(Affair affair);
}
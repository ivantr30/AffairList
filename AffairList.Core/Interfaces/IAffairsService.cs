namespace AffairList.Core.Interfaces;

public interface IAffairsService
{
    Task<List<string>> LoadAffairsAsync();
    Task SaveAffairsAsync(List<string> affairs);
    Task<string> AddAffairAsync(string affair);
    Task<bool> DeleteAffairAsync(string affair);
    Task<string> RenameAffairAsync(string oldAffair, string newAffair);
}
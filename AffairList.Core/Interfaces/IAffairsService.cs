namespace AffairList.Core.Interfaces;

public interface IAffairsService
{
    Task<string> AddAffairAsync(string affair, bool clearInputLine = true);
    Task<int> DeleteAffairAsync(string affair);
    Task<string> RenameAffairAsync(string affair, string baseAffair = "", bool check = true);
}
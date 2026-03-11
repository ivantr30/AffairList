using AffairList.Core.Enums;
using AffairList.Core.Interfaces;

namespace AffairList.Infrastructure.Classes.Commands.AffairsManagerCommands;

public class RenameAffairCommand(IAffairsService affairsService, string oldAffair, string newAffair) : IAsyncCommandAf
{
    public int Execute() => ExecuteAsync().Result;

    public async Task<int> ExecuteAsync()
    {
        string result = await affairsService.RenameAffairAsync(oldAffair, newAffair);
        return string.IsNullOrEmpty(result) ? (int)MethodResults.Error : (int)MethodResults.Success;
    }

    public int Redo() => RedoAsync().Result;
    public async Task<int> RedoAsync() => await ExecuteAsync();

    public int Undo() => UndoAsync().Result;
    public async Task<int> UndoAsync()
    {
        string result = await affairsService.RenameAffairAsync(newAffair, oldAffair);
        return string.IsNullOrEmpty(result) ? (int)MethodResults.Error : (int)MethodResults.Success;
    }
}
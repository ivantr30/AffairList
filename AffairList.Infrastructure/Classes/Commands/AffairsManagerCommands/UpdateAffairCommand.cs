using AffairList.Core.Enums;
using AffairList.Core.Interfaces;
using AffairList.Core.Models;

namespace AffairList.Infrastructure.Classes.Commands.AffairsManagerCommands;

public class UpdateAffairCommand(IAffairsService affairsService, Affair oldAffair, Affair newAffair) : IAsyncCommandAf
{
    public int Execute() => ExecuteAsync().Result;
    public async Task<int> ExecuteAsync()
    {
        var result = await affairsService.UpdateAffairAsync(newAffair);
        return result == null ? (int)MethodResults.Error : (int)MethodResults.Success;
    }

    public int Redo() => RedoAsync().Result;
    public async Task<int> RedoAsync() => await ExecuteAsync();

    public int Undo() => UndoAsync().Result;
    public async Task<int> UndoAsync()
    {
        var result = await affairsService.UpdateAffairAsync(oldAffair);
        return result == null ? (int)MethodResults.Error : (int)MethodResults.Success;
    }
}
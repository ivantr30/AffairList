using AffairList.Core.Enums;
using AffairList.Core.Interfaces;
using AffairList.Core.Models;

namespace AffairList.Infrastructure.Classes.Commands.AffairsManagerCommands;

public class DeleteAffairCommand(IAffairsService affairsService, Affair affair) : IAsyncCommandAf
{
    public int Execute() => ExecuteAsync().Result;
    public int Redo() => RedoAsync().Result;
    public int Undo() => UndoAsync().Result;

    public async Task<int> ExecuteAsync()
    {
        bool success = await affairsService.DeleteAffairAsync(affair);
        return success ? (int)MethodResults.Success : (int)MethodResults.Error;
    }

    public async Task<int> UndoAsync()
    {
        var result = await affairsService.AddAffairAsync(affair);
        return result == null ? (int)MethodResults.Error : (int)MethodResults.Success;
    }

    public async Task<int> RedoAsync() => await ExecuteAsync();
}
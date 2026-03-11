using AffairList.Core.Enums;
using AffairList.Core.Interfaces;

namespace AffairList.Infrastructure.Classes.Commands.AffairsManagerCommands;

public class DeleteAffairCommand(IAffairsService affairsService, string affair) : IAsyncCommandAf
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
        string result = await affairsService.AddAffairAsync(affair);
        return string.IsNullOrEmpty(result) ? (int)MethodResults.Error : (int)MethodResults.Success;
    }

    public async Task<int> RedoAsync() => await ExecuteAsync();
}
using AffairList.Core.Enums;
using AffairList.Core.Interfaces;

namespace AffairList.Infrastructure.Classes.Commands.AffairsManagerCommands;

public class AddAffairCommand(IAffairsService affairsService, string affair) : IAsyncCommandAf
{
    public int Execute() => ExecuteAsync().Result;

    public async Task<int> ExecuteAsync()
    {
        string result = await affairsService.AddAffairAsync(affair);
        return string.IsNullOrEmpty(result) ? (int)MethodResults.Error : (int)MethodResults.Success;
    }

    public int Redo() => RedoAsync().Result;
    public async Task<int> RedoAsync() => await ExecuteAsync();

    public int Undo() => UndoAsync().Result;
    public async Task<int> UndoAsync()
    {
        bool success = await affairsService.DeleteAffairAsync(affair);
        return success ? (int)MethodResults.Success : (int)MethodResults.Error;
    }
}
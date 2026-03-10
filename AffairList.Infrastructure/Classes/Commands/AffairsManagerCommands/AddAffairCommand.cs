using AffairList.Core.Enums;
using AffairList.Core.Interfaces;

namespace AffairList.Infrastructure.Classes.Commands.AffairsManagerCommands;

public class AddAffairCommand(IAffairsService affairsService, string affair) : IAsyncCommandAf
{
    public int Execute() => ExecuteAsync().Result;
    public async Task<int> ExecuteAsync()
    {
        affair = await affairsService.AddAffairAsync(affair);
        if (affair == string.Empty) return (int)MethodResults.Error;
        return (int)MethodResults.Success;
    }

    public int Redo() => RedoAsync().Result;
    public async Task<int> RedoAsync()
    {
        affair = await affairsService.AddAffairAsync(affair, false);
        if (affair == string.Empty) return (int)MethodResults.Error;
        return (int)MethodResults.Success;
    }

    public int Undo() => UndoAsync().Result;
    public async Task<int> UndoAsync() => await affairsService.DeleteAffairAsync(affair);
}
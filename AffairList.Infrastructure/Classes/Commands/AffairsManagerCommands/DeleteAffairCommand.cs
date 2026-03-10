using AffairList.Core.Enums;
using AffairList.Core.Interfaces;

namespace AffairList.Infrastructure.Classes.Commands.AffairsManagerCommands;

public class DeleteAffairCommand(IAffairsService affairsService, string affair) : IAsyncCommandAf
{
    public int Execute() => ExecuteAsync().Result;
    public int Redo() => RedoAsync().Result;
    public int Undo() => UndoAsync().Result;

    public async Task<int> ExecuteAsync()
        => await affairsService.DeleteAffairAsync(affair);

    public async Task<int> UndoAsync()
        => string.IsNullOrEmpty(await affairsService.AddAffairAsync(affair, false))
        ? (int)MethodResults.Error : (int)MethodResults.Success;

    public async Task<int> RedoAsync() => await affairsService.DeleteAffairAsync(affair);
}
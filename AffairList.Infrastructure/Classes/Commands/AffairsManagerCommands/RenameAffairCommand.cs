using AffairList.Core.Enums;
using AffairList.Core.Interfaces;

namespace AffairList.Infrastructure.Classes.Commands.AffairsManagerCommands;

public class RenameAffairCommand : IAsyncCommandAf
{
    private string _currentaffair;
    private string _newAffair = null!;
    private readonly string _baseAffair;
    private readonly IAffairsService _affairsService;

    public RenameAffairCommand(IAffairsService affairsService, ref string affair)
    {
        _currentaffair = affair;
        _baseAffair = new string(_currentaffair);
        _affairsService = affairsService;
    }

    public int Execute() => ExecuteAsync().Result;
    public async Task<int> ExecuteAsync()
    {
        string newAffair = await _affairsService.RenameAffairAsync(_currentaffair);
        if (string.IsNullOrEmpty(newAffair)) return (int)MethodResults.Error;
        _currentaffair = newAffair;
        _newAffair = newAffair;
        return (int)MethodResults.Success;
    }

    public int Redo() => RedoAsync().Result;
    public async Task<int> RedoAsync()
    {
        await _affairsService.RenameAffairAsync(_newAffair, _baseAffair, false);
        return (int)MethodResults.Success;
    }

    public int Undo() => UndoAsync().Result;
    public async Task<int> UndoAsync()
    {
        await _affairsService.RenameAffairAsync(_baseAffair, _newAffair, false);
        return (int)MethodResults.Success;
    }
}
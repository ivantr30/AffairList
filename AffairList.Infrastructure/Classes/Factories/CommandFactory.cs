using AffairList.Core.Interfaces;
using AffairList.Infrastructure.Classes.Commands.AffairsManagerCommands;

namespace AffairList.Infrastructure.Classes.Factories;

public static class CommandFactory
{
    public static AddAffairCommand CreateAddAffairCommand(IAffairsService affairsService, string affair)
        => new(affairsService, affair);

    public static DeleteAffairCommand CreateDeleteAffairCommand(IAffairsService affairsService, string affair)
        => new(affairsService, affair);

    public static RenameAffairCommand CreateRenameAffairCommand(IAffairsService affairsService, string oldAffair, string newAffair)
        => new(affairsService, oldAffair, newAffair);
}
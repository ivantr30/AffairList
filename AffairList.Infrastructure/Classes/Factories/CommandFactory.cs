using AffairList.Core.Interfaces;
using AffairList.Core.Models;
using AffairList.Infrastructure.Classes.Commands.AffairsManagerCommands;

namespace AffairList.Infrastructure.Classes.Factories;

public static class CommandFactory
{
    public static AddAffairCommand CreateAddAffairCommand(IAffairsService affairsService, Affair affair)
        => new(affairsService, affair);

    public static DeleteAffairCommand CreateDeleteAffairCommand(IAffairsService affairsService, Affair affair)
        => new(affairsService, affair);

    public static UpdateAffairCommand CreateUpdateAffairCommand(IAffairsService affairsService, Affair oldAffair, Affair newAffair)
        => new(affairsService, oldAffair, newAffair);
}
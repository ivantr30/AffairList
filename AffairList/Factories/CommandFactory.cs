using AffairList.Commands.AffairCommands;
using AffairList.Enums;
using AffairList.Services.Models;

namespace AffairList.Classes.Factories
{
    public static class CommandFactory
    {
        public static AddAffairCommand CreateAddAffairCommand
            (AffairsManager affairsManager, string affair)
        {
            return new AddAffairCommand(affairsManager, affair);
        }
        public static DeleteAffairCommand CreateDeleteAffairCommand
            (AffairsManager affairsManager, Affair affair)
        {
            return new DeleteAffairCommand(affairsManager, affair);
        }
        public static RenameAffairCommand CreateRenameAffairCommand
            (AffairsManager affairsManager, Affair affair)
        {
            return new RenameAffairCommand(affairsManager, affair);
        }
        public static SwitchAffairsCommand CreatSwitchAffairCommand
            (AffairsManager affairsManager, Affair firstAffair, Affair secondAffair)
        {
            return new SwitchAffairsCommand(affairsManager, firstAffair, secondAffair);
        }
        public static ToggleAffairPriorityCommand CreatToggleAffairPriorityCommand
            (AffairsManager affairsManager, Affair affair)
        {
            return new ToggleAffairPriorityCommand(affairsManager, affair);
        }
        public async static Task<ManageAffairDeadlineCommand> CreatManageAffairDeadlineCommandAsync
            (AffairsManager affairsManager, Affair affair)
        {
            var action = await affairsManager.DetermineDeadlineActionAsync(affair);
            return new ManageAffairDeadlineCommand(affairsManager, affair, action);
        }
    }
}

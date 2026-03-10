using AffairList.Classes.Commands.AffairsManagerCommands;

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
            (AffairsManager affairsManager, string affair)
        {
            return new DeleteAffairCommand(affairsManager, affair);
        }
        public static RenameAffairCommand CreateRenameAffairCommand
            (AffairsManager affairsManager, ref string affair)
        {
            return new RenameAffairCommand(affairsManager, ref affair);
        }
    }
}

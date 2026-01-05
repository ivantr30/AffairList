using AffairList.Interfaces;

namespace AffairList.Classes.Commands.AffairsManagerCommands
{
    public class AddAffairCommand : ICommandAf
    {
        private AffairsManager _affairsManager = null!;
        private string _affair;
        public AddAffairCommand(AffairsManager affairsManager, string affair)
        {
            _affairsManager = affairsManager;
            _affair = affair;
        }
        public async void Execute()
        {
            _affairsManager.AddAffairAsync(_affair);
        }

        public async void Redo()
        {
            _affairsManager.AddAffairAsync(_affair, false);
        }

        public async void Undo()
        {
            await _affairsManager.DeleteAffairAsync(_affair);
        }
    }
}

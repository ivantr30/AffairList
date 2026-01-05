using AffairList.Interfaces;

namespace AffairList.Classes.Commands.AffairsManagerCommands
{
    public class AddAffairCommand : ICommandAf
    {
        private AffairsManager _affairsManager = null!;
        private string _affair;
        private int _affairIndex;
        public AddAffairCommand(AffairsManager affairsManager, string affair)
        {
            _affairsManager = affairsManager;
            _affair = affair;
        }
        public async void Execute()
        {
            _affairIndex = await _affairsManager.AddAffairAsync(_affair);
        }

        public async void Redo()
        {
            _affairIndex = await _affairsManager.AddAffairAsync(_affair, false);
        }

        public async void Undo()
        {
            await _affairsManager.DeleteAffairAsync(_affairIndex);
        }
    }
}

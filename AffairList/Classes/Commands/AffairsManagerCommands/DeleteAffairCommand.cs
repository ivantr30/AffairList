using AffairList.Interfaces;

namespace AffairList.Classes.Commands.AffairsManagerCommands
{
    public class DeleteAffairCommand : ICommandAf
    {
        private string _affair;
        private AffairsManager _affairsManager;
        public DeleteAffairCommand(AffairsManager affairsManager, string affair) 
        {
            _affairsManager = affairsManager;
            _affair = affair;
        }
        public async void Execute()
        {
            await _affairsManager.DeleteAffairAsync(_affair);
        }

        public async void Redo()
        {
            await _affairsManager.DeleteAffairAsync(_affair);
        }

        public async void Undo()
        {
            if (_affair.EndsWith("."))
                _affairsManager.AddAffairAsync(_affair.Remove(_affair.Length - 1), false);
            else _affairsManager.AddAffairAsync(_affair, false);
        }
    }
}

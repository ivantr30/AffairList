using AffairList.Interfaces;

namespace AffairList.Classes.Commands.AffairsManagerCommands
{
    public class DeleteAffairCommand : IAsyncCommandAf
    {
        private string _affair;
        private AffairsManager _affairsManager;
        public DeleteAffairCommand(AffairsManager affairsManager, string affair) 
        {
            _affairsManager = affairsManager;
            _affair = affair;
        }
        public int Execute()
        {
            return _affairsManager.DeleteAffairAsync(_affair).Result;
        }

        public int Redo()
        {
            return _affairsManager.DeleteAffairAsync(_affair).Result;
        }

        public int Undo()
        {
            return _affairsManager.AddAffairAsync(_affair, false).Result;
        }

        public async Task<int> ExecuteAsync()
        {
            return await _affairsManager.DeleteAffairAsync(_affair);
        }

        public async Task<int> UndoAsync()
        {
            return await _affairsManager.AddAffairAsync(_affair, false);
        }

        public async Task<int> RedoAsync()
        {
            return await _affairsManager.DeleteAffairAsync(_affair);
        }
    }
}

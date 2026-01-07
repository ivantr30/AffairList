using AffairList.Enums;
using AffairList.Interfaces;

namespace AffairList.Classes.Commands.AffairsManagerCommands
{
    public class AddAffairCommand : IAsyncCommandAf
    {
        private AffairsManager _affairsManager = null!;
        private string _affair;
        public AddAffairCommand(AffairsManager affairsManager, string affair)
        {
            _affairsManager = affairsManager;
            _affairsManager.affairAdded += OnAffairAdded;
            _affair = affair;
        }

        public int Execute()
        {
            return _affairsManager.AddAffairAsync(_affair).Result;
        }

        public async Task<int> ExecuteAsync()
        {
            int result = await _affairsManager.AddAffairAsync(_affair);
            if (result == (int)MethodResults.Success)
                _affairsManager.affairAdded -= OnAffairAdded;
            return result;
        }
        private void OnAffairAdded(string affair)
        {
            _affair = affair;
        }

        public int Redo()
        {
            return _affairsManager.AddAffairAsync(_affair, false).Result;
        }

        public async Task<int> RedoAsync()
        {
            return await _affairsManager.AddAffairAsync(_affair, false);
        }

        public int Undo()
        {
            return _affairsManager.DeleteAffairAsync(_affair).Result;
        }

        public async Task<int> UndoAsync()
        {
            return await _affairsManager.DeleteAffairAsync(_affair);
        }
    }
}

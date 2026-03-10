using AffairList.Enums;
using AffairList.Interfaces;

namespace AffairList.Classes.Commands.AffairsManagerCommands
{
    public class RenameAffairCommand : IAsyncCommandAf
    {
        private string _currentaffair;
        private string _newAffair;
        private string _baseAffair;
        private AffairsManager _affairsManager;
        public RenameAffairCommand(AffairsManager affairsManager, ref string affair)
        {
            _currentaffair = affair;
            _baseAffair = new string(_currentaffair);
            _affairsManager = affairsManager;
        }
        public int Execute()
        {
            return ExecuteAsync().Result;
        }

        public async Task<int> ExecuteAsync()
        {
            string newAffair = await _affairsManager.RenameAffairAsync(_currentaffair);
            if (string.IsNullOrEmpty(newAffair)) return (int)MethodResults.Error;
            _currentaffair = newAffair;
            _newAffair = newAffair;
            return (int)MethodResults.Success;
        }

        public int Redo()
        {
            return RedoAsync().Result;
        }

        public async Task<int> RedoAsync()
        {
            await _affairsManager.RenameAffairAsync(_newAffair, _baseAffair, false);
            return (int)MethodResults.Success;
        }

        public int Undo()
        {
            return UndoAsync().Result;
        }

        public async Task<int> UndoAsync()
        {
            await _affairsManager.RenameAffairAsync(_baseAffair, _newAffair, false);
            return (int)MethodResults.Success;
        }
    }
}

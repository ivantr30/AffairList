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
            _affair = affair;
        }

        public int Execute()
        {
            return ExecuteAsync().Result;
        }

        public async Task<int> ExecuteAsync()
        {
            _affair = await _affairsManager.AddAffairAsync(_affair);
            if (_affair == string.Empty) return (int)MethodResults.Error;
            return (int)MethodResults.Success;
        }

        public int Redo()
        {
            return RedoAsync().Result;
        }

        public async Task<int> RedoAsync()
        {
            _affair = await _affairsManager.AddAffairAsync(_affair, false);
            if (_affair == string.Empty) return (int)MethodResults.Error;
            return (int)MethodResults.Success;
        }

        public int Undo()
        {
            return UndoAsync().Result;
        }

        public async Task<int> UndoAsync()
        {
            return await _affairsManager.DeleteAffairAsync(_affair);
        }
    }
}

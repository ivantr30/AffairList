using AffairList.Enums;
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
            return ExecuteAsync().Result;
        }

        public int Redo()
        {
            return RedoAsync().Result;
        }

        public int Undo()
        {
            return UndoAsync().Result;
        }

        public async Task<int> ExecuteAsync()
        {
            return await _affairsManager.DeleteAffairAsync(_affair);
        }

        public async Task<int> UndoAsync()
        {
            return  string.IsNullOrEmpty(await _affairsManager.AddAffairAsync(_affair, false)) ? 
                (int)MethodResults.Error : (int)MethodResults.Success;
        }

        public async Task<int> RedoAsync()
        {
            return await _affairsManager.DeleteAffairAsync(_affair);
        }
    }
}

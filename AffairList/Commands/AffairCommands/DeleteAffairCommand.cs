using AffairList.Enums;
using AffairList.Interfaces;
using AffairList.Services.Models;

namespace AffairList.Commands.AffairCommands
{
    public class DeleteAffairCommand : IAsyncCommand
    {
        private Affair _affair;
        private AffairsManager _affairsManager;
        public DeleteAffairCommand(AffairsManager affairsManager, Affair affair) 
        {
            _affairsManager = affairsManager;
            _affair = affair;
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

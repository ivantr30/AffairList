using AffairList.Interfaces;
using AffairList.Services.Models;

namespace AffairList.Commands.AffairCommands
{
    public class AddAffairCommand : IAsyncCommand
    {
        private AffairsManager _affairsManager = null!;
        private Affair _affair;
        public AddAffairCommand(AffairsManager affairsManager, string affair)
        {
            _affairsManager = affairsManager;
            _affair = new Affair(affair);
        }

        public async Task<int> ExecuteAsync()
        {
            int operationResult = await _affairsManager.AddAffairAsync(_affair);
            return operationResult;
        }
        public async Task<int> RedoAsync()
        {
            int operationResult = await _affairsManager.AddAffairAsync(_affair, false);
            return operationResult;
        }

        public async Task<int> UndoAsync()
        {
            return await _affairsManager.DeleteAffairAsync(_affair);
        }
    }
}

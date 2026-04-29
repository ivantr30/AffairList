using AffairList.Interfaces;
using AffairList.Services.Models;

namespace AffairList.Commands.AffairCommands
{
    public class ToggleAffairPriorityCommand : IAsyncCommand
    {
        private Affair _affair;
        private AffairsManager _affairsManager;

        public ToggleAffairPriorityCommand(AffairsManager affairsManager, Affair affair)
        {
            _affair = affair;
            _affairsManager = affairsManager;
        }

        public async Task<int> ExecuteAsync()
        {
            return await _affairsManager.ToggleAffairPriority(_affair);
        }

        public async Task<int> RedoAsync()
        {
            return await ExecuteAsync();
        }

        public async Task<int> UndoAsync()
        {
            return await ExecuteAsync();
        }
    }
}

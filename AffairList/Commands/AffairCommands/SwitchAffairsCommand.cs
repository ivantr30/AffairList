using AffairList.Interfaces;
using AffairList.Services.Models;

namespace AffairList.Commands.AffairCommands
{
    public class SwitchAffairsCommand : IAsyncCommand
    {
        private Affair _firstAffair;
        private Affair _secondAffair;
        private AffairsManager _affairsManager;
        public SwitchAffairsCommand(AffairsManager affairsManager, Affair firstAffair, Affair secondAffair)
        {
            _firstAffair = firstAffair;
            _secondAffair = secondAffair;
            _affairsManager = affairsManager;
        }
        public async Task<int> ExecuteAsync()
        {
            return await _affairsManager.SwitchAffairsAsync(_firstAffair, _secondAffair);
        }

        public async Task<int> RedoAsync()
        {
            return await _affairsManager.SwitchAffairsAsync(_firstAffair, _secondAffair);
        }

        public async Task<int> UndoAsync()
        {
            return await _affairsManager.SwitchAffairsAsync(_secondAffair, _firstAffair);
        }
    }
}

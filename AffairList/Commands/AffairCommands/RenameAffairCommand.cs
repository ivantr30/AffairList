using AffairList.Enums;
using AffairList.Interfaces;
using AffairList.Services.Models;

namespace AffairList.Commands.AffairCommands
{
    public class RenameAffairCommand : IAsyncCommand
    {
        private Affair _currentaffair;
        private string _newAffair;
        private string _baseAffair;
        private AffairsManager _affairsManager;
        public RenameAffairCommand(AffairsManager affairsManager, Affair affair)
        {
            _currentaffair = affair;
            _baseAffair = _currentaffair.InnerText;
            _affairsManager = affairsManager;
        }
        public async Task<int> ExecuteAsync()
        {
            await _affairsManager.RenameAffairAsync(_currentaffair);
            _newAffair = _currentaffair.InnerText;
            return (int)MethodResults.Success;
        }

        public async Task<int> RedoAsync()
        {
            _currentaffair.InnerText = _newAffair;
            await _affairsManager.UpdateAffairInnerText(_currentaffair, _newAffair);
            return (int)MethodResults.Success;
        }

        public async Task<int> UndoAsync()
        {
            await _affairsManager.UpdateAffairInnerText(_currentaffair, _baseAffair);
            return (int)MethodResults.Success;
        }
    }
}

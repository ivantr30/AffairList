using AffairList.Enums;
using AffairList.Interfaces;
using AffairList.Services.Models;

namespace AffairList.Commands.AffairCommands
{
    public class ManageAffairDeadlineCommand : IAsyncCommand
    {
        private AffairsManager _affairsManager;
        private Affair _affair;
        private DateOnly? _deadline;
        private DeadlineActions _action;

        public ManageAffairDeadlineCommand(AffairsManager affairsManager, Affair affair, DeadlineActions action)
        {
            _affair = affair;
            _affairsManager = affairsManager;
            _action = action;
        }

        public async Task<int> ExecuteAsync()
        {
            switch (_action)
            {
                case DeadlineActions.Add:
                    _action = DeadlineActions.Add;
                    _deadline = await _affairsManager.AddDeadlineAsync(_affair, _deadline);
                    break;
                case DeadlineActions.Delete:
                    _action = DeadlineActions.Delete;
                    _deadline = await _affairsManager.DeleteDeadlineAsync(_affair);
                    break;
                case DeadlineActions.Update:
                    _deadline = await _affairsManager.UpdateDeadlineAsync(_affair, _deadline);
                    _action = DeadlineActions.Update;
                    break;
                case DeadlineActions.None:
                    return (int)MethodResults.NothingHappened;
            }
            if (_action == DeadlineActions.None) return (int)MethodResults.Error;
            return (int)MethodResults.Success;
        }

        public async Task<int> UndoAsync()
        {
            _action = DetermineNextAction();
            return await ExecuteAsync();
        }

        public async Task<int> RedoAsync()
        {
            _action = DetermineNextAction();
            return await ExecuteAsync();
        }

        private DeadlineActions DetermineNextAction()
        {
            return _action switch
            {
                DeadlineActions.Add => DeadlineActions.Delete,
                DeadlineActions.Delete => DeadlineActions.Add,
                DeadlineActions.Update => DeadlineActions.Update,
            };
        }
    }
}

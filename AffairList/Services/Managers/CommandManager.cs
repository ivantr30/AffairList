using AffairList.Enums;
using AffairList.Interfaces;

namespace AffairList.Services.Managers
{
    public class CommandManager
    {
        private LimitedStack<IAsyncCommand> _undoOperations;
        private LimitedStack<IAsyncCommand> _redoOperations;

        public CommandManager()
        {
            _undoOperations = new(25);
            _redoOperations = new(25);
        }

        public async Task<int> ExecuteAsync(IAsyncCommand asyncCommand)
        {
            int result = await asyncCommand.ExecuteAsync().ConfigureAwait(false);
            if (result != (int)MethodResults.Success) return (int)MethodResults.NothingHappened;
            _redoOperations.Clear();
            _undoOperations.Push(asyncCommand);
            return result;
        }
        public async Task<int> UndoAsync()
        {
            if (_undoOperations.Count == 0)
                return (int)MethodResults.NothingHappened;

            IAsyncCommand commandAsync = _undoOperations.Pop();
            int result = await commandAsync.UndoAsync();

            if (result != (int)MethodResults.Success)
                return (int)MethodResults.NothingHappened;

            _redoOperations.Push(commandAsync);
            return result;
        }

        public async Task<int> RedoAsync()
        {
            if (_redoOperations.Count == 0)
                return (int)MethodResults.NothingHappened;

            IAsyncCommand commandAsync = _redoOperations.Pop();
            int result = await commandAsync.RedoAsync();

            if (result != (int)MethodResults.Success)
                return (int)MethodResults.NothingHappened;

            return result;
        }
    }
}

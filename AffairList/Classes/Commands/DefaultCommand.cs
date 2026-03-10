

using AffairList.Interfaces;

namespace AffairList.Classes.Commands
{
    public class DefaultCommandAsync : IAsyncCommandAf
    {
        public int Execute()
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public int Redo()
        {
            throw new NotImplementedException();
        }

        public Task<int> RedoAsync()
        {
            throw new NotImplementedException();
        }

        public int Undo()
        {
            throw new NotImplementedException();
        }

        public Task<int> UndoAsync()
        {
            throw new NotImplementedException();
        }
    }
}

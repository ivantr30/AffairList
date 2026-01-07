namespace AffairList.Interfaces
{
    public interface IAsyncCommandAf : ICommandAf
    {
        Task<int> ExecuteAsync();
        Task<int> UndoAsync();
        Task<int> RedoAsync();
    }
}

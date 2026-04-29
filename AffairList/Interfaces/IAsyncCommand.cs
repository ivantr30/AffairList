namespace AffairList.Interfaces
{
    public interface IAsyncCommand
    {
        Task<int> ExecuteAsync();
        Task<int> UndoAsync();
        Task<int> RedoAsync();
    }
}

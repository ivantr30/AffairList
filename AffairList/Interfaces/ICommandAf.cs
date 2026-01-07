namespace AffairList.Interfaces
{
    public interface ICommandAf
    {
        int Execute();
        int Undo();
        int Redo();
    }
}

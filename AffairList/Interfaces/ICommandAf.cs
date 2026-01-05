namespace AffairList.Interfaces
{
    public interface ICommandAf
    {
        void Execute();
        void Undo();
        void Redo();
    }
}

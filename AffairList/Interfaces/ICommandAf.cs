namespace AffairList.Interfaces
{
    public interface ICommandAf
    {
        void Execute(); // переписать с возвращаемым результатом в виде int
        void Undo();
        void Redo();
    }
}

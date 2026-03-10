namespace AffairList.Core.Interfaces;

public interface ICommandAf
{
    int Execute();
    int Undo();
    int Redo();
}
namespace AffairList
{
    public interface IKeyPreviewable
    {
        KeyEventHandler KeyDownHandlers { get; }
        KeyPressEventHandler KeyPressHandlers { get; }
        KeyEventHandler KeyUpHandlers { get; }
    }
}

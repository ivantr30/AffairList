namespace AffairList
{
    public interface IChildable
    {
        IParentable ParentElement { get; }
        void OnAddition();
    }
}

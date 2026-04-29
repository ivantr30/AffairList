namespace AffairList
{
    public interface IChildable
    {
        IParentable ParentElement { get; }
        Task OnAdditionAsync();
        Task<bool> OnRemovingAsync(bool closing = false);
    }
}

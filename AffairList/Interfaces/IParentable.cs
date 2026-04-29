namespace AffairList
{
    public interface IParentable
    {
        Task SetControlAsync(Control control);
        Task OpenFormAsync(Form form, bool asDialog);
        void Exit();
        Task ReturnAsync();
        void MinimizeForm();
        void SetLastPoint(MouseEventArgs e);
        void MoveForm(MouseEventArgs e);
        void MoveChildForm(MouseEventArgs e);
    }
}

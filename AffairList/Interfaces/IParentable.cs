namespace AffairList
{
    public interface IParentable
    {
        void SetControl(Control control);
        void OpenForm(Form form);
        void Exit();
        void Return();
        void MinimizeForm();
        void SetLastPoint(MouseEventArgs e);
        void MoveForm(MouseEventArgs e);
    }
}

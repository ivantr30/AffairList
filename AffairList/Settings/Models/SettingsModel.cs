namespace AffairList
{
    public class SettingsModel
    {
        public string currentListFileFullPath { get; set; } = "";

        public Color textColor { get; set; } = Color.MediumSpringGreen;
        public Color bgColor { get; set; } = Color.Black;

        public Keys closeKey { get; set; } = Keys.F7;
        public Keys returnKey { get; set; } = Keys.F6;

        public bool autostartState { get; set; } = false;
        public bool askToDelete { get; set; } = true;
        public bool DoesNotificate { get; set; } = true;
        // For ToDoList.cs
        public bool CanBeAlwaysReplaced { get; set; } = true;

        public int x { get; set; } = 0;
        public int y { get; set; } = 0;
        public uint notificationDayDistance { get; set; } = 1;
        public uint notificationHourDistance { get; set; } = 8;

    }
}

namespace AffairList
{
    public class SettingsModel
    {
        public string CurrentListFileFullPath { get; set; } = "";

        public Color TextColor { get; set; } = Color.MediumSpringGreen;
        public Color BgColor { get; set; } = Color.Black;

        public Keys CloseKey { get; set; } = Keys.F7;
        public Keys ReturnKey { get; set; } = Keys.F6;

        public bool AutostartState { get; set; } = false;
        public bool AskToDelete { get; set; } = true;
        public bool DoesNotificate { get; set; } = true;
        // For ToDoList.cs
        public bool CanBeAlwaysReplaced { get; set; } = true;

        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public uint NotificationDayDistance { get; set; } = 1;
        public uint NotificationHourDistance { get; set; } = 8;

    }
}

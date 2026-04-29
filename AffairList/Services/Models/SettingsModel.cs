using System.Text.Json.Serialization;

namespace AffairList
{
    public class SettingsModel
    {
        public event Action AutostartStateChanged;
        public string CurrentProfileFullPath { get; 
            set
            {
                field = value ?? "";
            }
        } = "";

        [JsonIgnore]
        public Color TextColor { get; set; } = Color.MediumSpringGreen;
        [JsonIgnore]
        public Color BgColor { get; set; } = Color.Black;
        [JsonPropertyName("TextColorArgb")]
        public int TextColorArgb
        {
            get => TextColor.ToArgb();
            set => TextColor = Color.FromArgb(value);
        }

        [JsonPropertyName("BgColorArgb")]
        public int BgColorArgb
        {
            get => BgColor.ToArgb();
            set => BgColor = Color.FromArgb(value);
        }

        public Keys CloseKey { get; set; } = Keys.F7;
        public Keys ReturnKey { get; set; } = Keys.F6;

        public bool AutostartState {
            get;
            set
            {
                field = value;
                AutostartStateChanged?.Invoke();
            }
        } = false;
        public bool AskToDelete { get; set; } = true;
        public bool DoesNotificate { get; set; } = true;
        // For ToDoList.cs
        public bool CanBeAlwaysReplaced { get; set; } = true;

        public int TodoListX { get; set; } = 0;
        public int ToDoListY { get; set; } = 0;
        public uint NotificationDayDistance { get; set; } = 1;
        public uint NotificationHourDistance { get; set; } = 8;

    }
}

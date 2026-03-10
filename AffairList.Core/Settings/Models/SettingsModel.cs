namespace AffairList.Core.Settings.Models;

public class SettingsModel
{
    public string CurrentListFileFullPath { get; set; } = "";

    public string TextColorHex { get; set; } = "#00FA9A"; // MediumSpringGreen
    public string BgColorHex { get; set; } = "#000000"; // Black

    public int CloseKeyId { get; set; } = 118; // F7
    public int ReturnKeyId { get; set; } = 117; // F6

    public bool AutostartState { get; set; } = false;
    public bool AskToDelete { get; set; } = true;
    public bool DoesNotificate { get; set; } = true;
    public bool CanBeAlwaysReplaced { get; set; } = true;

    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;

    public uint NotificationDayDistance { get; set; } = 1;
    public uint NotificationHourDistance { get; set; } = 8;
}
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AffairList.Core.Models;

public class Affair : INotifyPropertyChanged
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set { field = value; OnPropertyChanged(); } } = string.Empty;
    public bool IsPriority { get; set { field = value; OnPropertyChanged(); } }
    public DateTime? Deadline { get; set { field = value; OnPropertyChanged(); } }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public Affair Clone() => new()
    {
        Id = Id,
        Title = Title,
        IsPriority = IsPriority,
        Deadline = Deadline
    };
}
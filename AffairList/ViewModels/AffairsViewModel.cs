using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using AffairList.Core.Enums;
using AffairList.Core.Interfaces;
using AffairList.Infrastructure.Classes.Factories;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AffairList.ViewModels;

public partial class AffairItem(string original) : ObservableObject
{
    [ObservableProperty]
    public partial string OriginalString { get; set; } = original;

    [ObservableProperty]
    public partial string DisplayText { get; set; } = original.Replace(" <priority>", "").Replace("<priority>", "").Trim();

    [ObservableProperty]
    public partial bool IsPriority { get; set; } = original.Contains("<priority>");
}

public partial class AffairsViewModel : ObservableObject
{
    private readonly IAffairsService _affairsService;
    private readonly Stack<ICommandAf> _undoOperations = new();
    private readonly Stack<ICommandAf> _redoOperations = new();
    private const string PriorityTag = "<priority>";

    [ObservableProperty]
    public partial ObservableCollection<AffairItem> Affairs { get; set; } = [];

    [ObservableProperty]
    public partial string InputText { get; set; } = string.Empty;

    [ObservableProperty]
    public partial AffairItem? SelectedAffair { get; set; }

    public AffairsViewModel(IAffairsService affairsService)
    {
        _affairsService = affairsService;
        _ = LoadAffairsAsync();
    }

    [RelayCommand]
    private async Task LoadAffairsAsync()
    {
        var loadedAffairs = await _affairsService.LoadAffairsAsync();
        Affairs.Clear();
        foreach (var affair in loadedAffairs)
            Affairs.Add(new AffairItem(affair));
    }

    private async Task ExecuteCommandAsync(IAsyncCommandAf command)
    {
        int result = await command.ExecuteAsync();
        if (result == (int)MethodResults.Success)
        {
            _undoOperations.Push(command);
            _redoOperations.Clear();
            await LoadAffairsAsync();
        }
    }

    [RelayCommand]
    private async Task AddAffairAsync()
    {
        if (string.IsNullOrWhiteSpace(InputText)) return;
        var cmd = CommandFactory.CreateAddAffairCommand(_affairsService, InputText);
        await ExecuteCommandAsync(cmd);
        InputText = string.Empty;
    }

    [RelayCommand]
    private async Task DeleteAffairAsync()
    {
        if (SelectedAffair == null) return;
        var cmd = CommandFactory.CreateDeleteAffairCommand(_affairsService, SelectedAffair.OriginalString);
        await ExecuteCommandAsync(cmd);
    }

    public async Task RenameAffairAsync(string oldNameOriginal, string newDisplayName)
    {
        if (string.IsNullOrWhiteSpace(newDisplayName)) return;

        bool hadPriority = oldNameOriginal.Contains(PriorityTag);
        string newNameWithTags = hadPriority ? newDisplayName + " " + PriorityTag : newDisplayName;

        if (oldNameOriginal == newNameWithTags) return;

        var cmd = CommandFactory.CreateRenameAffairCommand(_affairsService, oldNameOriginal, newNameWithTags);
        await ExecuteCommandAsync(cmd);
    }

    [RelayCommand]
    private async Task TogglePriorityAsync()
    {
        if (SelectedAffair == null) return;

        string oldName = SelectedAffair.OriginalString;
        string newName = oldName.Contains(PriorityTag)
            ? oldName.Replace(" " + PriorityTag, "").Replace(PriorityTag, "")
            : oldName + " " + PriorityTag;

        var cmd = CommandFactory.CreateRenameAffairCommand(_affairsService, oldName, newName);
        await ExecuteCommandAsync(cmd);
    }

    [RelayCommand]
    private async Task UndoAsync()
    {
        if (_undoOperations.Count == 0) return;
        var cmd = _undoOperations.Pop();
        int result = cmd is IAsyncCommandAf asyncCmd ? await asyncCmd.UndoAsync() : cmd.Undo();
        if (result == (int)MethodResults.Success) { _redoOperations.Push(cmd); await LoadAffairsAsync(); }
    }

    [RelayCommand]
    private async Task RedoAsync()
    {
        if (_redoOperations.Count == 0) return;
        var cmd = _redoOperations.Pop();
        int result = cmd is IAsyncCommandAf asyncCmd ? await asyncCmd.RedoAsync() : cmd.Redo();
        if (result == (int)MethodResults.Success) { _undoOperations.Push(cmd); await LoadAffairsAsync(); }
    }
}
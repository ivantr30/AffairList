using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using AffairList.Core.Enums;
using AffairList.Core.Interfaces;
using AffairList.Core.Models;
using AffairList.Infrastructure.Classes.Factories;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AffairList.ViewModels;

public partial class AffairsViewModel : ObservableObject
{
    private readonly IAffairsService _affairsService;
    private readonly Stack<ICommandAf> _undoOperations = new();
    private readonly Stack<ICommandAf> _redoOperations = new();

    [ObservableProperty]
    public partial ObservableCollection<Affair> Affairs { get; set; } = [];

    [ObservableProperty]
    public partial string InputText { get; set; } = string.Empty;

    [ObservableProperty]
    public partial Affair? SelectedAffair { get; set; }

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
            Affairs.Add(affair);
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
        var newAffair = new Affair { Title = InputText };
        var cmd = CommandFactory.CreateAddAffairCommand(_affairsService, newAffair);

        await ExecuteCommandAsync(cmd);
        InputText = string.Empty;
    }

    [RelayCommand]
    private async Task DeleteAffairAsync()
    {
        if (SelectedAffair == null) return;
        var cmd = CommandFactory.CreateDeleteAffairCommand(_affairsService, SelectedAffair);
        await ExecuteCommandAsync(cmd);
    }

    public async Task RenameAffairAsync(string newTitle)
    {
        if (SelectedAffair == null || string.IsNullOrWhiteSpace(newTitle) || SelectedAffair.Title == newTitle) return;

        var oldAffair = SelectedAffair.Clone();
        var newAffair = SelectedAffair.Clone();
        newAffair.Title = newTitle;

        var cmd = CommandFactory.CreateUpdateAffairCommand(_affairsService, oldAffair, newAffair);
        await ExecuteCommandAsync(cmd);
    }

    [RelayCommand]
    private async Task TogglePriorityAsync()
    {
        if (SelectedAffair == null) return;

        var oldAffair = SelectedAffair.Clone();
        var newAffair = SelectedAffair.Clone();
        newAffair.IsPriority = !newAffair.IsPriority;

        var cmd = CommandFactory.CreateUpdateAffairCommand(_affairsService, oldAffair, newAffair);
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
using System;

using AffairList.ViewModels;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace AffairList.Views;

public sealed partial class AffairsPage : Page
{
    public AffairsViewModel ViewModel { get; }

    public AffairsPage()
    {
        InitializeComponent();
        ViewModel = App.Current.Services.GetRequiredService<AffairsViewModel>();
    }

    private void InputTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Enter)
            if (ViewModel.AddAffairCommand.CanExecute(null))
                ViewModel.AddAffairCommand.Execute(null);
    }

    private void TogglePriority_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        SelectContextItem(sender);
        if (ViewModel.TogglePriorityCommand.CanExecute(null))
            ViewModel.TogglePriorityCommand.Execute(null);
    }

    private void Delete_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        SelectContextItem(sender);
        if (ViewModel.DeleteAffairCommand.CanExecute(null))
            ViewModel.DeleteAffairCommand.Execute(null);
    }

    private async void Rename_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        SelectContextItem(sender);

        if (ViewModel.SelectedAffair == null) return;

        RenameTextBox.Text = ViewModel.SelectedAffair.DisplayText;
        RenameDialog.XamlRoot = XamlRoot;

        var result = await RenameDialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
            await ViewModel.RenameAffairAsync(ViewModel.SelectedAffair.OriginalString, RenameTextBox.Text);
    }

    private void SelectContextItem(object sender)
    {
        if (sender is MenuFlyoutItem item && item.DataContext is AffairItem affairItem)
            ViewModel.SelectedAffair = affairItem;
    }
}
using AffairList.Views;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AffairList;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ExtendsContentIntoTitleBar = true;
        NavView.SelectedItem = NavView.MenuItems[0];
    }

    private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            ContentFrame.Navigate(typeof(SettingsPage));
        }
        else
        {
            var navItemTag = args.SelectedItemContainer.Tag?.ToString();
            if (navItemTag == "AffairsPage")
                ContentFrame.Navigate(typeof(AffairsPage));
            else if (navItemTag == "ProfilesPage")
                ContentFrame.Navigate(typeof(ProfilesPage));
        }
    }
}
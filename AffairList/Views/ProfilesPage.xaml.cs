using AffairList.ViewModels;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace AffairList.Views;

public sealed partial class ProfilesPage : Page
{
    public ProfilesViewModel ViewModel { get; }

    public ProfilesPage()
    {
        InitializeComponent();
        ViewModel = App.Current.Services.GetRequiredService<ProfilesViewModel>();
    }
}
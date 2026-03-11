using System;

using AffairList.Core.Interfaces;
using AffairList.Infrastructure.Services;
using AffairList.Infrastructure.Settings;
using AffairList.ViewModels;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace AffairList;

public partial class App : Application
{
    public IServiceProvider Services { get; }
    public static new App Current => (App)Application.Current;

    private Window? _window;

    public App()
    {
        InitializeComponent();
        Services = ConfigureServices();
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<Settings>();
        services.AddTransient<IAffairsService, AffairsService>();

        services.AddSingleton<AffairsViewModel>();
        services.AddTransient<ProfilesViewModel>();
        services.AddTransient<SettingsViewModel>();

        return services.BuildServiceProvider();
    }

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        var settings = Services.GetRequiredService<Settings>();
        await settings.InitializeAsync();

        _window = new MainWindow();
        _window.Activate();
    }
}

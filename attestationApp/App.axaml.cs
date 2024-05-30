using attestationApp.ViewModels;
using attestationApp.Views;
using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Splat.Autofac;
using VolunteerHub;

namespace attestationApp;

public partial class App : Application
{
    public override void Initialize()
    {
       
        AvaloniaXamlLoader.Load(this);
        Bootstrapper.Init();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var DataContext = GetRequiredService<MainViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                DataContext = DataContext
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = DataContext
            };
        }

        DataContext.Router.NavigateAndReset.Execute(GetRequiredService<AuthViewModel>());
        base.OnFrameworkInitializationCompleted();
    }

    private static T GetRequiredService<T>() => Splat.Locator.Current.GetRequiredService<T>();
}

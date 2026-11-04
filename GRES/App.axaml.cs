using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GRES.Routing;
using GRES.Services;
using GRES.ViewModels;
using GRES.ViewModels.Routing;
using GRES.Views;
using Microsoft.Extensions.DependencyInjection;

namespace GRES;

public partial class App : Application
{
    private ServiceProvider? _serviceProvider = null;
    public override void Initialize()
    {
        _serviceProvider = InjectionServices();
       
        AvaloniaXamlLoader.Load(this);
#if DEBUG
        this.AttachDeveloperTools();
#endif
    }

    public override void OnFrameworkInitializationCompleted()
    {

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };

            desktop.Exit += DestroyServices;
        }
        else if (ApplicationLifetime is IActivityApplicationLifetime singleViewFactoryApplicationLifetime)
        {
            singleViewFactoryApplicationLifetime.MainViewFactory = () => new MainView { DataContext = new MainViewModel() };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }


    private static ServiceProvider InjectionServices()
    {
        var services = new ServiceCollection();

        return services.BuildServiceProvider();
    }

    private T? GetService<T>()
    {
        if(_serviceProvider == null)
            return default;

        return _serviceProvider.GetService<T>();
    }

    internal ViewModelBase? GetViewModelService<TViewModel>()
            where TViewModel: ViewModelBase
    {
        var vm = GetService<TViewModel>();
        return vm;
    }

    private void DestroyServices(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        _serviceProvider?.Dispose();
    }
}
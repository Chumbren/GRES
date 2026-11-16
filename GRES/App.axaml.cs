using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GRES.Services.Lifetime;
using GRES.Services.Lifetime.PlatformServices;
using GRES.Services.Lifetime.Windows;
using GRES.Services.PreInitilization;
using GRES.ViewModels;
using GRES.Views;
using Microsoft.Extensions.DependencyInjection;

namespace GRES;

public partial class App : Application
{
    private Platform? _platform = null; 
    private MainViewModel? _mainViewModel = null;
    private ServiceProvider? _preInitilizationServiceProvider = null;
    private ServiceProvider? _lifetimeServiceProvider = null;



    public override void Initialize()
    {
        _preInitilizationServiceProvider = InjectionPreInitilizationServices();

        AvaloniaXamlLoader.Load(this);
        #if DEBUG
             this.AttachDeveloperTools();
        #endif
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _platform = GetDesktopPlatform();
              
            MainWindow window = new();
            WindowService windowService = new(_platform, window);
            _mainViewModel = new MainViewModel(_platform, windowService);
            window.DataContext = _mainViewModel;
            desktop.MainWindow = window;
            
            desktop.Exit += DestroyServiceProviders;
        }
        else if (ApplicationLifetime is IActivityApplicationLifetime singleViewFactoryApplicationLifetime)
        {
            singleViewFactoryApplicationLifetime.MainViewFactory = () => new MainView { DataContext = null };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            _platform = GetMobilePlatform();
            _mainViewModel = new MainViewModel(_platform, null);
            singleViewPlatform.MainView = GetMobileView(singleViewPlatform, _mainViewModel);
        }
        
        _lifetimeServiceProvider = InjectionLifetimeServices();

        base.OnFrameworkInitializationCompleted();
    }

    private static Platform GetDesktopPlatform()
    {
        OS? os = null;
        TypePlatform typePlatform = TypePlatform.Desktop;
        if(OperatingSystem.IsWindows())
            os = OS.Android;
        else if(OperatingSystem.IsLinux())
            os = OS.Linux;
        else if(OperatingSystem.IsMacOS()) 
            os = OS.MacOS;
        
        if(os == null)
            throw new PlatformNotSupportedException("ОС не поддерживается");
        
        var platform = new Platform(typePlatform, (OS)os);
        return platform;
    }

    private static MainView GetMobileView(ISingleViewApplicationLifetime singleViewPlatform, MainViewModel vm)
    {
        return new MainView
        {
            DataContext = vm
        };
    }

    private static Platform GetMobilePlatform()
    {
        OS? os = null;
        TypePlatform typePlatform = TypePlatform.Mobile;
        if(OperatingSystem.IsAndroid())
            os = OS.Android;
        else if(OperatingSystem.IsIOS())
            os = OS.IOS;
        
        if(os == null)
            throw new ArgumentNullException("ОС не поддерживается");
        
        var platform = new Platform(typePlatform, (OS)os);
        return platform;
    }

    private static ServiceProvider InjectionPreInitilizationServices()
    {
        var services = new ServiceCollection();
        return services.BuildServiceProvider();
    }

    private ServiceProvider InjectionLifetimeServices()
    {
        if(_platform == null || _mainViewModel == null)
            throw new PlatformNotSupportedException();

        var services = new ServiceCollection();
        services.AddSingleton(_platform);
        services.AddSingleton(_mainViewModel);
        return services.BuildServiceProvider();
    }


    private ILifetimeService? GetLifetimeService<TLifetime>() 
        where TLifetime : ILifetimeService
    {
        if(_lifetimeServiceProvider == null)
            return null;

        return _lifetimeServiceProvider.GetService<TLifetime>();
    }

    internal MainViewModel GetMainViewModel()
    {
        ArgumentNullException.ThrowIfNull(_mainViewModel);
        return (MainViewModel)_mainViewModel;
    }

    internal Platform GetPlatform()
    {
        ArgumentNullException.ThrowIfNull(_platform);
        return _platform;
    }

    private IPreInitilizationService? GetPreInitilizationService<TPreInitilization>() 
        where TPreInitilization : IPreInitilizationService
    {
        if(_preInitilizationServiceProvider == null)
            return null;

        return _preInitilizationServiceProvider.GetService<TPreInitilization>();
    }

    

    private void DestroyServiceProviders(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        _preInitilizationServiceProvider?.Dispose();
        _lifetimeServiceProvider?.Dispose();
    }
}
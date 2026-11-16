using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GRES.Services.Lifetime;
using GRES.Services.Lifetime.PlatformServices;
using GRES.Services.Lifetime.Windows;
using GRES.ViewModels.Auth;
using GRES.Views;
using GRES.Views.Auth;

namespace GRES.ViewModels;


public partial class MainViewModel(Platform platform, WindowService? windowService) 
    : ViewModelBase, ILifetimeService
{
    private readonly WindowService? _windowService = windowService;
    private readonly Platform _platform = platform;

    public Platform Platform => _platform;

    public void ToggleMaximize()
    {
        _windowService?.ToggleMaximize();
    }

    public void Minimize()
    {
        _windowService?.Minimize();
    }

    public void Close()
    {
        if(Application.Current is not {} application)
            return;
        
        if(application.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
            _windowService?.Close();
        else if(application.ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            singleViewPlatform.MainView = null;
        else
            Environment.Exit(0);
    }
}

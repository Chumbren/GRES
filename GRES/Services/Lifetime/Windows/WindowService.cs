using System;
using Avalonia.Controls;
using GRES.Services.Lifetime.PlatformServices;
using GRES.ViewModels;

namespace GRES.Services.Lifetime.Windows;

public class WindowService(Platform platform, Window window) 
    : ILifetimeService
{
    private readonly Window _currentWindow = window;

    public Platform Platform => platform;


    public void ToggleMaximize()
    {
        _currentWindow.WindowState = _currentWindow.WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
    }
    public void Minimize()
    {
        _currentWindow.WindowState = WindowState.Minimized;
    }

    public void Close()
    {
        _currentWindow.Close(0);
    }

}

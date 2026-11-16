using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.Input;
using GRES.Views;

namespace GRES.ViewModels.Home;

public partial class HomeViewModel : ViewModelBase
{

    [RelayCommand]
    public static void ToggleMaximize()
    {
        MainViewModel? mainViewModel = null;
        if(Application.Current is {} application)
            mainViewModel = ((App)application).GetMainViewModel();

        mainViewModel?.ToggleMaximize();
    }

    [RelayCommand]
    public static void Minimize()
    {
        MainViewModel? mainViewModel = null;
        if(Application.Current is {} application)
            mainViewModel = ((App)application).GetMainViewModel();

        mainViewModel?.Minimize();
    }

    [RelayCommand]
    public static void Close()
    {
        MainViewModel? mainViewModel = null;
        if(Application.Current is {} application)
            mainViewModel = ((App)application).GetMainViewModel();

        mainViewModel?.Close();
    }
}

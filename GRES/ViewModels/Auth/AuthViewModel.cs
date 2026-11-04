using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GRES.Views.Auth;

namespace GRES.ViewModels.Auth;

public partial class AuthViewModel : ViewModelBase
{

    [ObservableProperty]
    public partial ViewModelBase Content { get; set; } = new AuthorizationViewModel();

    [ObservableProperty]
    public partial string TextRequestButton { get; set; } = "Авторизироваться";
}

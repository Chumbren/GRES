using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GRES.Views.Auth;

namespace GRES.ViewModels.Auth;

public partial class AuthViewModel : ViewModelBase
{
    private const string TEXT_REQUEST_BUTTON_AUTHORIZATION = "Авторизироваться";
    private const string TEXT_REQUEST_BUTTON_REGISTRATION = "Зарегистрироваться";

    private const string TEXT_AUTH_TYPE_BUTTON_AUTHORIZATION = "Зарегистрируйтесь";
    private const string TEXT_AUTH_TYPE_BUTTON_REGISTRATION = "Авторизируйтесь";


    [ObservableProperty]
    public partial bool IsRegistration { get; set; } = false;

    [ObservableProperty]
    public partial string TextSetAuthTypeButton { get; set; } = TEXT_AUTH_TYPE_BUTTON_AUTHORIZATION;
    [ObservableProperty]
    public partial string TextRequestButton {get; set;} = TEXT_REQUEST_BUTTON_AUTHORIZATION;
    

    

    [RelayCommand]
    public void SetAuthType()
    {
        TextRequestButton = IsRegistration 
            ? TEXT_REQUEST_BUTTON_REGISTRATION
            : TEXT_REQUEST_BUTTON_AUTHORIZATION;

        TextSetAuthTypeButton = IsRegistration 
            ? TEXT_AUTH_TYPE_BUTTON_REGISTRATION
            : TEXT_AUTH_TYPE_BUTTON_AUTHORIZATION;
        
        IsRegistration = !IsRegistration;
    }

    [RelayCommand]
    public async Task Authorization()
    {
        
    }

    [RelayCommand]
    public async Task Registration()
    {
        
    }
}

using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GRES.Services;
using GRES.ViewModels;

namespace GRES.ViewModels.Routing;

public partial class LinkViewModel(string to, IRouterService? service) : ViewModelBase
{
    private readonly IRouterService? _service = service;
    public readonly string To = to;
    public LinkViewModel() : this(string.Empty, null)
    {
        
    }

    [RelayCommand]
    public void Lead()
    {
        _service?.To(To);    
    }
}

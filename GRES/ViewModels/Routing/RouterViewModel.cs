using System;
using CommunityToolkit.Mvvm.ComponentModel;
using GRES.Routing;
using GRES.Services;

namespace GRES.ViewModels.Routing;

public partial class RouterViewModel(): ViewModelBase, INavigationObject
{
    [ObservableProperty]
    public partial string Url { get; set; }
}

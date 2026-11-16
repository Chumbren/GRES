using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GRES.ViewModels.Loaders;

public partial class DataLoaderViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial bool IsLoading {get; set;} = false;

    public async Task Load()
    {
        
    }
}

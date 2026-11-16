using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GRES.Controls;
using GRES.ViewModels.Form;
using GRES.Views.Form;

namespace GRES.Controls.Form;

public partial class Form : BaseContainer
{
    public Form() : base(new FormViewModel())
    {
        DataContext = new FormViewModel();
    }

    protected override void OnLoaded(Control control)
    {
        if(control is Input input)
        {
            
        }
        else if(control is Submit submit)
        {
            
        }
    }
}
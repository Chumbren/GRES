using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GRES.Services;
using GRES.ViewModels.Routing;

namespace GRES.Views.Routing;

public partial class LinkView : UserControl
{
    /// <summary>
    /// To StyledProperty definition
    /// indicates .
    /// </summary>
    public static readonly StyledProperty<string> ToProperty =
        AvaloniaProperty.Register<LinkView, string>(nameof(To));
    
    /// <summary>
    /// Gets or sets the To property. This StyledProperty
    /// indicates .
    /// </summary>
    public string To
    {
       get => this.GetValue(ToProperty);
       set => SetValue(ToProperty, value);
    }
    
    public LinkView()
    {
        InitializeComponent();
    }
}
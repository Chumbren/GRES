using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GRES.Views.Routing;

public partial class RouteView : UserControl
{
    /// <summary>
    /// Link StyledProperty definition
    /// indicates .
    /// </summary>
    public static readonly StyledProperty<string> MyPropertyProperty =
        AvaloniaProperty.Register<RouteView, string>(nameof(Link));
    
    /// <summary>
    /// Link or sets the Link property. This StyledProperty
    /// indicates .
    /// </summary>
    public string Link
    {
       get => this.GetValue(MyPropertyProperty);
       set => SetValue(MyPropertyProperty, value);
    }
    
    public RouteView()
    {
        InitializeComponent();
    }
}
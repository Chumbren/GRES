using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GRES.Services.Lifetime.PlatformServices;

namespace GRES.Controls.Platform;

public partial class BaseCrossPlatformControl : UserControl
{
    public BaseCrossPlatformControl(TypePlatform typePlatform, OS os)
    {
        var application = App.Current;
        if(application == null || application is not App app)
            return;

        var currentPlatform = app.GetPlatform();
        bool isEquilsPlatform = currentPlatform.Type == typePlatform && 
                            (os == OS.All || currentPlatform.OS == os); 

        if(Design.IsDesignMode || isEquilsPlatform)
            InitializeComponent();
    }
}
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GRES.Services.Lifetime.PlatformServices;

namespace GRES.Controls.Platform;

public partial class AndroidOSControl()
    : BaseCrossPlatformControl(TypePlatform.Mobile, OS.Android)
{
}
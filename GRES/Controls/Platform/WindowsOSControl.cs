using System;
using GRES.Services.Lifetime.PlatformServices;

namespace GRES.Controls.Platform;

public class WindowsOSControl() 
    : BaseCrossPlatformControl(TypePlatform.Desktop, OS.Windows)
{
}

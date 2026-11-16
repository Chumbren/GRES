using System;
using GRES.Services.Lifetime.PlatformServices;

namespace GRES.Controls.Platform;

public class MacOSControl() 
    : BaseCrossPlatformControl(TypePlatform.Desktop, OS.MacOS)
{
}

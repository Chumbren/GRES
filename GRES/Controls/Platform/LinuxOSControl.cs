using System;
using GRES.Services.Lifetime.PlatformServices;

namespace GRES.Controls.Platform;

public class LinuxOSControl() 
    : BaseCrossPlatformControl(TypePlatform.Desktop, OS.Linux)
{
}

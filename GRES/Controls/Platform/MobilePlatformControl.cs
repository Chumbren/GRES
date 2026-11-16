using System;
using GRES.Services.Lifetime.PlatformServices;

namespace GRES.Controls.Platform;

public class MobilePlatformControl() 
    : BaseCrossPlatformControl(TypePlatform.Mobile, OS.All)
{
}

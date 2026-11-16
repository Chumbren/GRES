using System;
using GRES.Services.Lifetime.PlatformServices;

namespace GRES.Controls.Platform;

public class PlatformControl(TypePlatform type, OS os) 
    : BaseCrossPlatformControl(type, os)
{
}

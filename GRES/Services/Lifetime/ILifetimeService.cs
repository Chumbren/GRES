using System;
using GRES.Services.Lifetime.PlatformServices;

namespace GRES.Services.Lifetime;

public interface ILifetimeService
{
    Platform Platform { get; }
}

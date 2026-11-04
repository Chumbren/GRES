using System;

namespace GRES.Routing;

internal readonly ref struct RouterConfig(INavigationObject navigation, 
                                        ReadOnlySpan<string> routes)
{
    internal readonly INavigationObject Navigation = navigation;
    internal readonly ReadOnlySpan<string> Routes = routes;
}

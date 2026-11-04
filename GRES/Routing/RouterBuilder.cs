using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using GRES.Services;

namespace GRES.Routing;

internal class RouterBuilder(INavigationObject navigation)
{
    private readonly INavigationObject _navigation = navigation;
    private readonly List<string> _routes = [];
    internal RouterConfig Build()
    {
        ReadOnlySpan<string> routes = CollectionsMarshal.AsSpan(_routes);
        RouterConfig result = new(_navigation, routes);
        return result;
    }

    internal RouterBuilder AddRoute(string url)
    {
        if(!_routes.Contains(url))
             _routes.Add(url);

        return this;
    }
}

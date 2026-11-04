using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using GRES.Exceptions;
using GRES.Routing;
using GRES.ViewModels;


namespace GRES.Services;



public class Router
{
    private readonly ReadOnlyCollection<string> _routes;
    private readonly INavigationObject _navigation;
    private static Router? _router = null; 

    internal static Router? Instance => _router;

    private Router(in RouterConfig config)
    {
        _navigation = config.Navigation;
        _routes = Array.AsReadOnly(config.Routes.ToArray());
    }

    internal static Router CreateInstance(RouterBuilder builder)
    {
        if(_router != null)
            throw new MultiplyInsteanceSingletonObjectException(nameof(Router));

        RouterConfig config = builder.Build();
        Router result = new(config); 
        _router = result;
        return _router;
    }

    internal static void ThrowIfNotNullInstance()
    {
        if(_router != null)
            throw new MultiplyInsteanceSingletonObjectException(nameof(Router));
    }

    internal void ThrowIfNotExistRoute(string url)
    {
        if(!_routes.Contains(url))
            throw new NotExistRouteException(url);
    }

    internal void NavigateTo(string url)
    {
        _navigation.Update(url);
    }
}

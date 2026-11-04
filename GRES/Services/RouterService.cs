using System;
using GRES.Routing;

namespace GRES.Services;

public class RouterService(Router router) : IRouterService
{
    private readonly Router _router = router;

    public void To(string url, params object[] args)
    {
        _router.ThrowIfNotExistRoute(url);
        _router.NavigateTo(url);
    }
}

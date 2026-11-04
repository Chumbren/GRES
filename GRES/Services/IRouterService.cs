using System;
using GRES.Routing;

namespace GRES.Services;

public interface IRouterService
{
    void To(string url, params object[] args);
}

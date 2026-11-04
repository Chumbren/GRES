using System;

namespace GRES.Exceptions;

public class NotExistRouteException(string url) : InvalidOperationException(
    $"Маршрут {(string.IsNullOrEmpty(url) || string.IsNullOrEmpty(url) || url.Length == 0 ? url : "Empty Url")} не существует")
{

}

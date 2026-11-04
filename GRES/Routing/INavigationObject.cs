using System;
using GRES.ViewModels;

namespace GRES.Routing;

public interface INavigationObject
{
    string Url { get; set; }
    
    void Update(string url)
    {
        Url = url;
    } 
}

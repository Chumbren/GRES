using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CommunityToolkit.Mvvm.ComponentModel;
using GRES.Routing;
using GRES.Routing.Address;

namespace GRES.Views.Routing;

internal class RouteTemplateSelector : IDataTemplate
{
    public Url? Link {get; set;}

    public IDataTemplate? RouteTemplate {get; set;}


    public Control? Build(object? param)
    {
        if(param is RouteContext context)
        {
            
        }
    }

    public bool Match(object? data) => data is RouteContext;
}

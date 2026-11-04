using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using GRES.Converters;
using GRES.Routing;
using GRES.Services;
using GRES.ViewModels;
using GRES.ViewModels.Routing;

namespace GRES.Views.Routing;

[DebuggerDisplay("{GetType().Name,nq} (Name = {Name})")]
public partial class RouterView : UserControl
{
    /// <summary>
    /// Start StyledProperty definition
    /// indicates .
    /// </summary>
    public static readonly StyledProperty<string> StartProperty =
        AvaloniaProperty.Register<RouterView, string>(nameof(Start), "");

    /// <summary>
    /// Gets or sets the Start property. This StyledProperty
    /// indicates .
    /// </summary>
    public string Start
    {
       get => this.GetValue(StartProperty);
       set => SetValue(StartProperty, value);
    }

    
    private readonly RouterViewModel _vm;
    
    private readonly RouterBuilder _routerBuilder;

    private readonly List<LinkView> _links = [];


    public RouterView()
    {
        InitializeComponent();
        ArgumentNullException.ThrowIfNull(DataContext);
        Router.ThrowIfNotNullInstance();

        if(DataContext is not RouterViewModel vm || DataContext is not INavigationObject)
        {
            throw new InvalidCastException($"{nameof(DataContext)} не является {nameof(RouterViewModel)}");
        }

        _vm =  vm;
        _routerBuilder = new(_vm);
        
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        RegisterRoutes();

        ContentPresenter contentPresenter = new()
        {
          Content = this.Content  
        };

        this.Content = contentPresenter;
        _vm.Url = Start;
    }

    /// <summary>
    /// Метод регистрации маршрутов путем перебора всего вложенного контента
    /// </summary>
    private void RegisterRoutes()
    {
        if(Content == null)
            return;

        IterationContent(Content);
        var router = Router.CreateInstance(_routerBuilder);
        InitializeLinks(router);
    }

    /// <summary>
    ///  Метод выбора итерации по типу контента и вызов соответствующего
    /// метода итерации. 
    /// Если ContentControl, Decorator то IterationContent
    /// Если LinkView то добьавляется ссылка в общий список ссылок
    /// Если Panel то IterationPanel
    /// Если RouteView то IterationRouteView
    /// </summary>
    /// <param name="object">Проверяемый объект</param>
    private void IterationContent(object? @object)
    {
        if(@object is RouteView route)
            IterationRouteView(route);
        else if(@object is LinkView link)
            AddLink(link);
        else if(@object is ContentControl contentControl)
            IterationContent(contentControl.Content);
        else if(@object is Decorator decorator)
            IterationContent(decorator.Child);
        else if(@object is Panel panel)
            IterationPanel(panel);

    }

    private void AddLink(LinkView link) => _links.Add(link);

    private void InitializeLinks(Router router)
    {
        foreach (var link in _links)
        {
            var service = new RouterService(router);
            var vm = new LinkViewModel(link.To, service);
            link.DataContext = vm;
        }
    }

    /// <summary>
    /// Метод итерации по переданной панели разметки
    /// </summary>
    /// <param name="panel">Панель разметки</param>
    private void IterationPanel(Panel panel)
    {
        foreach (var child in panel.Children)
            IterationContent(child);
    }


    /// <summary>
    /// Метод итерации по маршруту
    /// </summary>
    /// <param name="route">Страница маршрута</param>

    private void IterationRouteView(RouteView route)
    {
        _routerBuilder.AddRoute(route.Link);
        IterationContent(route.Content);
        BindContentRouteViewToUrl(route);
    }

    /// <summary>
    /// Метод привязки текущего маршрута к 
    /// отображению контента с помощью конвертера
    /// </summary>
    /// <param name="route">Страница маршрута</param>
    private void BindContentRouteViewToUrl(RouteView route)
    {
        var link = route.Link;
        var content = route.Content;
        route.Content = null;
        route.DataContext = _vm;
        var binding = new Binding("Url")
        {
            Converter = new UrlToContentConverter(link, content)
        };
        route.Bind(ContentControl.ContentProperty, binding);
    }

}
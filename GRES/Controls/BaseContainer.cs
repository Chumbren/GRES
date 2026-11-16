using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GRES.Controls;

public abstract class BaseContainer : StackPanel
{
    private readonly Action<Control>[]? _loadedActions;
    private readonly bool _isSingleLoaded;
    public BaseContainer(object? dataContext, 
        Action<Control>[]? loadedActions = null)
    {
        Loaded += ContainerLoaded;
        _loadedActions = loadedActions;
        if(dataContext != null)
            DataContext = dataContext;
    }

    public virtual void ContainerLoaded(object? sender, RoutedEventArgs e)
    {
        Loaded -= ContainerLoaded;

        foreach (var child in Children)
        {
            if(child != null)
            {
                OnLoaded(child);
                InvokeLoadedActions(child);
            }
        }
    }

    private void InvokeLoadedActions(Control control)
    {
        if(_loadedActions == null)
            return;
        
        foreach (var action in _loadedActions)
            action(control);
    }

    protected abstract void OnLoaded(Control control);
}

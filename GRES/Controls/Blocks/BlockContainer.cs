using System;
using Avalonia.Controls;

namespace GRES.Controls.Blocks;

public class BlockContainer(object? dataContext, 
         Action<Control>[]? loadedActions = null) 
    : BaseContainer(dataContext, loadedActions)
{
    public BlockContainer() : this(null, null)
    {}

    protected override void OnLoaded(Control control)
    {}
}

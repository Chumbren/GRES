using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GRES.Views.Home;

public partial class MenuPanel : UserControl
{
    public MenuPanel()
    {
        InitializeComponent();
    }

    private void OnTitleBarPointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        // Проверяем, что нажата именно левая кнопка мыши
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            if (TopLevel.GetTopLevel(this) is Window window)
            {
                if (e.ClickCount == 2)
                {
                    window.WindowState = window.WindowState == WindowState.Maximized
                        ? WindowState.Normal
                        : WindowState.Maximized;
                }
                else
                {
                    window.BeginMoveDrag(e);
                }
            }
        }
    }
}
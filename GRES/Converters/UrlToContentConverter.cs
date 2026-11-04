using System;
using System.Globalization;
using Avalonia.Controls.Templates;
using Avalonia.Data.Converters;
using GRES.Routing.Address;

namespace GRES.Converters;

public class UrlToContentConverter(Url url, object? content) : IValueConverter
{
    private readonly Url _url = url;
    private readonly object? _content = content;
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value is not Url address)
            return null;
        
        
        if(address != _url)
            return null;
        
        return _content;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

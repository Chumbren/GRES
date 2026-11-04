using System;
using System.ComponentModel;
using System.Globalization;
using GRES.Routing.Address;

namespace GRES.Views.Routing;

internal class RouteTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if(sourceType == typeof(string)) return true;
        return base.CanConvertFrom(context, sourceType);    
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if(value is string @string)
        {
            /
            Url? url = new(@string);
            return url;
        }
        return base.ConvertFrom(context, culture, value);
    }
}

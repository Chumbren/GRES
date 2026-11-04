using System;
using GRES.Routing.Address;

namespace GRES.Routing.Address;

internal record class Parameter(string Name, TypeParameter Type, string Source, object? Value)
            : IAddress
{
    internal Parameter() : this(string.Empty, TypeParameter.None, string.Empty, null)
    {}

    internal Parameter(string name, TypeParameter type, string source) 
        : this(name, type, source, null)
    {}
}

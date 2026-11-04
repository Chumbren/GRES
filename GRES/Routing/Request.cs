using System;
using GRES.Routing.Address;
using GRES.Routing.Parameters;

namespace GRES.Routing;

internal record class Request(Url Url, Parameter[] Parameters, object?[]? Body);

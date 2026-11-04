using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GRES.ViewModels;

namespace GRES.Routing;

public record class Route(in string Url, 
                        in INavigationObject Navigation);

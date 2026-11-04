using System;

namespace GRES.Exceptions;

internal class NotValidateUrlException(string url, ) : InvalidOperationException()
{

}

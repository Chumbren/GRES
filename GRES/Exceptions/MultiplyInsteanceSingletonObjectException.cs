using System;

namespace GRES.Exceptions;

public class MultiplyInsteanceSingletonObjectException(string name) 
: ArgumentOutOfRangeException($"Синглтон объект {name} создался больше одного раза")
{

}

using System;
using System.Runtime.CompilerServices;

[Serializable]
public class NoValueException: System.Exception
{
    public NoValueException(String message)
        : base(message)
    {}
}

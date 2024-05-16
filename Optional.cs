using System;
using System.Diagnostics;
using System.Reflection;

public class Optional<T> 
{
    private bool _isPresent = false;

    private T _value;

    private Optional()
    {

    }
    private Optional(T value)
    {
        this._value = value;
        _isPresent = true;
    }

    public static Optional<T> Of(T value)
    {
        return new Optional<T>(value);
    }
    
    public static Optional<T> Empty()
    {
        return new Optional<T>();
    }

    public T GetValue() 
    {
        if (!_isPresent)
        {
            throw new NoValueException("Optional doesn't contain a value.");
        }
        else
        {
            return _value;
        }
    }

    public T OrElse(T value)
    {
        return _isPresent ? this._value : value;
    }

    public bool IsPresent()
    {
        return _isPresent;
    }

    public void IfPresent(Action<T> action)
    {
        if (_isPresent)
        {
            action(this._value);
        }

    }
}
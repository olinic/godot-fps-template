
using System.Collections.Generic;
using System.Dynamic;
using Godot;

public class InputWrapper
{
    private static readonly HashSet<string> _actions = new();

    public static void ActionPress(string action)
    {
        _actions.Add(action);
        Input.ActionPress(action);
    }

    public static void Clear()
    {
        foreach (string action in _actions)
        {
            Input.ActionRelease(action);
        }
        _actions.Clear();
    }
}
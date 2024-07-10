using FPS.Characters.Player;
using Godot;
using System;

public partial class Main : Node
{
    public override void _Input(InputEvent @event)
    {
        if(@event.IsActionPressed("exit"))
		{
			GetTree().Quit();
		}
    }
}


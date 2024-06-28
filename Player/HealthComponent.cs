using Godot;
using System;

public partial class HealthComponent : Node3D, ICanTakeDamage
{
	[Export] private int MAX_HEALTH = 1000;
	
	[Signal]
	public delegate void health_depletedEventHandler();
	public void ApplyDamage(Attack attack)
	{
		_health -= attack.Damage; 
		if(_health <= 0)
		{
			EmitSignal(SignalName.health_depleted);
		}
	}

	private int _health;
	public int Health 
	{
		get{ return _health; }
	}

	public override void _Ready()
	{
		_health = MAX_HEALTH;
	}
	
	
}

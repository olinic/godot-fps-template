using Godot;

public partial class HealthComponent : Node3D, ICanTakeDamage
{
	[Export] private int MAX_HEALTH = 1000;
	
	[Signal]
	public delegate void health_depletedEventHandler();

	[Signal]
	public delegate void health_changedEventHandler(Health health);

	private Health _health;

	public override void _Ready()
	{
		_health = new Health() 
		{
			Value = MAX_HEALTH,
			Max = MAX_HEALTH
		};
	}

	public void ApplyDamage(Attack attack)
	{
		_health.Value -= attack.Damage;
		_health.Value = Mathf.Clamp(_health.Value, 0, _health.Max);
		EmitSignal(SignalName.health_changed, _health);
		if(_health.Value == 0)
		{
			EmitSignal(SignalName.health_depleted);
		}
	}

	
	public Health Health 
	{
		get{ return _health; }
	}
}

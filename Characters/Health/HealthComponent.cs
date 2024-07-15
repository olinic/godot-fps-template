using Godot;

namespace FPS.Characters.Health;
public partial class HealthComponent : Node3D, ICanTakeDamage
{
	[Export] private int MAX_HEALTH = 1000;
	
	[Signal]
	public delegate void health_depletedEventHandler();

	[Signal]
	public delegate void health_changedEventHandler(Health health);

	public Health Health { get; private set; }

	public override void _Ready()
	{
		Health = new Health() 
		{
			Value = MAX_HEALTH,
			Max = MAX_HEALTH
		};
		EmitSignal(SignalName.health_changed, Health);
	}

	public void ApplyDamage(Attack attack)
	{
		Health.Value -= attack.Damage;
		Health.Value = Mathf.Clamp(Health.Value, 0, Health.Max);
		EmitSignal(SignalName.health_changed, Health);
		if(Health.Value == 0)
		{
			EmitSignal(SignalName.health_depleted);
		}
	}
}

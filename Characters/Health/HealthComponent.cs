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
		Health = new Health() { Max = MAX_HEALTH };
		AdjustHealth(MAX_HEALTH);
	}

	public void ApplyDamage(Attack attack)
	{
		AdjustHealth(Health.Value - attack.Damage);
	}

	private void AdjustHealth(int value)
	{
		Health.Value = Mathf.Clamp(value, 0, Health.Max);
		EmitSignal(SignalName.health_changed, Health);
		if(Health.Value == 0)
		{
			EmitSignal(SignalName.health_depleted);
		}
	}
}

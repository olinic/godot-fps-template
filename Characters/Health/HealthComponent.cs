using Godot;

namespace FPS.Characters.Health;
public partial class HealthComponent : Node3D, ICanTakeDamage
{
	[Export] private Timer _RegenTimer;
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
			Max = MAX_HEALTH,
			Value = MAX_HEALTH
		};
		EmitSignal(SignalName.health_changed, Health);
	}

    public override void _PhysicsProcess(double delta)
    {
		ApplyHealthRegen();
    }

    public void ApplyDamage(Attack attack)
	{
		_RegenTimer.Start();
		AdjustHealth(Health.Value - attack.Damage);
	}

	private void AdjustHealth(int value)
	{
		int clamped = Mathf.Clamp(value, 0, MAX_HEALTH);

		if(clamped != Health.Value)
		{
			Health = new Health()
			{
				Max = MAX_HEALTH,
				Value = clamped
			};
			EmitSignal(SignalName.health_changed, Health);
			if(Health.Value == 0)
			{
				EmitSignal(SignalName.health_depleted);
			}
		}	
	}

	private void ApplyHealthRegen()
	{
		if(_RegenTimer.IsStopped())
		{
			AdjustHealth(Health.Value + 5);
		}
	}
}

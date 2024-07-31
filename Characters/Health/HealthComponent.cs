using Godot;

namespace FPS.Characters.Health;
public partial class HealthComponent : Node3D, ICanTakeDamage
{
	public Timer _RegenTimer;
	//[Export] private float MAX_HEALTH = 1000;
	public float MAX_HEALTH { get; init; } = 1000;

	[Export] private float REGEN_DURATION;
	private float REGEN_RATE;
	
	[Signal]
	public delegate void health_depletedEventHandler();

	[Signal]
	public delegate void health_changedEventHandler(Health health);

	public Health Health { get; private set; }

	public override void _Ready()
	{
		_RegenTimer = new() { OneShot = true, Autostart = false, WaitTime = 2.0f};
		AddChild(_RegenTimer);
		//duration is 2 sec
		//health is 600
		//rate 10
		REGEN_RATE = MAX_HEALTH/REGEN_DURATION;
		Health = new Health()
		{
			Max = MAX_HEALTH,
			Value = MAX_HEALTH
		};
		EmitSignal(SignalName.health_changed, Health);
	}

    public override void _PhysicsProcess(double delta)
    {
		ApplyHealthRegen(delta);
    }

    public void ApplyDamage(Attack attack)
	{
		_RegenTimer.Start();
		AdjustHealth(Health.Value - attack.Damage);
	}

	private void AdjustHealth(float value)
	{
		float clamped = Mathf.Clamp(value, 0, MAX_HEALTH);

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

	private void ApplyHealthRegen(double delta)
	{
		if(_RegenTimer.IsStopped())
		{
			AdjustHealth(Health.Value + REGEN_RATE * (float) delta);
		}
	}
}

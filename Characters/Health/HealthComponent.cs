using Godot;

namespace FPS.Characters.Health;
public partial class HealthComponent : Node3D, ICanTakeDamage
{
	public Timer _regenTimer;
	public float MaxHealth { get; set; } = 1000;
	public float RegenDurationSeconds { 
		get => _regenDurationSeconds; 
		set
		{
			_regenDurationSeconds = value;
			SetRegenRate();
		}
	}
	public Health Health { get; private set; }

	[Signal] public delegate void health_depletedEventHandler();
	[Signal] public delegate void health_changedEventHandler(Health health);

	private float _regenDurationSeconds = 2;
	private float _regenRate;
	

	public HealthComponent()
	{
		SetRegenRate();
	}

	private void SetRegenRate()
	{
		_regenRate = MaxHealth/RegenDurationSeconds;
	}

	public override void _Ready()
	{
		_regenTimer = new() { OneShot = true, Autostart = false, WaitTime = 2.0f};
		AddChild(_regenTimer);
		Health = new Health()
		{
			Max = MaxHealth,
			Value = MaxHealth
		};
		EmitSignal(SignalName.health_changed, Health);
	}

    public override void _PhysicsProcess(double delta)
    {
		ApplyHealthRegen(delta);
    }

	private void ApplyHealthRegen(double delta)
	{
		if(_regenTimer.IsStopped())
		{
			AdjustHealth(Health.Value + _regenRate * (float) delta);
		}
	}

    public void ApplyDamage(Attack attack)
	{
		_regenTimer.Start();
		AdjustHealth(Health.Value - attack.Damage);
	}

	private void AdjustHealth(float value)
	{
		float clamped = Mathf.Clamp(value, 0, MaxHealth);

		if(clamped != Health.Value)
		{
			Health = new Health()
			{
				Max = MaxHealth,
				Value = clamped
			};
			EmitSignal(SignalName.health_changed, Health);
			if(Health.Value == 0)
			{
				EmitSignal(SignalName.health_depleted);
			}
		}	
	}
}

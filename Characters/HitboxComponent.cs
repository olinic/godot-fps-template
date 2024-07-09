using Godot;

public partial class HitboxComponent : Node3D, ICanTakeDamage
{
	[Export] public HealthComponent health;

	public void OnHitboxComponentBodyEntered(Node3D body)
	{
		if (body is ICanAttack attacker)
		{
			health.ApplyDamage(attacker.GetAttack());
		}
	}

	public void ApplyDamage(Attack attack)
	{
		health.ApplyDamage(attack);
	}
}

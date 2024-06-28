using Godot;

public partial class HitboxComponent : Node3D
{
	[Export]
	public HealthComponent health;

	public void OnHitboxComponentBodyEntered(Node3D body)
	{
		
		if (body is ICanAttack attacker)
		{
			//GD.Print("Doing Damage!");
			health.ApplyDamage(attacker.GetAttack());
		}
	}
	public void ApplyDamage(Attack attack)
	{
		
			GD.Print("Doing Damage!");
			health.ApplyDamage(attack);
		
	}
}

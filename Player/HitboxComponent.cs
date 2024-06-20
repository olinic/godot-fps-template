using Godot;

public partial class HitboxComponent : Node3D
{
	[Export]
	public HealthComponent health;

	public void OnHitboxComponentBodyEntered(Node3D body)
	{
		GD.Print("Entered Area");
		if (body is ICanAttack attacker)
		{
			health.ApplyDamage(attacker.GetAttack());
		}
	}
}

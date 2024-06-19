using Godot;

public partial class HitboxComponent : Node3D
{
	[Export]
	public HealthComponent health;

	public void ApplyDamage(Attack attack)
	{
		// Requirements: Player needs the 
		// Option 1: Two CollisionShapes (one for hitbox, one for the player)
		// Option 2: Upon collision, look for the hitbox component among the direct children

		health.ApplyDamage(attack);
	}
}


using FPS.Weapons;
using Godot;

namespace FPS.Characters.Enemy;
public partial class EnemyWeaponControls: Node3D
{
    [Export] private WeaponHandler WeaponHandler;
	[Export] private Timer ReloadTimer;

    public override void _PhysicsProcess(double delta)
    {
		if(ReloadTimer.IsStopped())
		{
            WeaponHandler.Shoot(delta);
        	HandleReload();
		}
    }

    private void HandleReload()
    {
        if (WeaponHandler.EquippedWeapon.GetCurrentAmmo() == 0)
        {
            WeaponHandler.Reload();
			GD.Print("Enemy Reloaded");
			ReloadTimer.Start(1.5f);
        }
    }
}
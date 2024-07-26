
using FPS.Weapons;
using Godot;

namespace FPS.Characters.Enemy;
public partial class EnemyWeaponControls: Node3D
{
    [Export] private WeaponHandler _WeaponHandler;
	[Export] private Timer _ReloadTimer;


    public override void _PhysicsProcess(double delta)
    {
		if(_ReloadTimer.IsStopped())
		{
            _WeaponHandler.Shoot(delta);
        	HandleReload();
		}
    }

    private void HandleReload()
    {
        if (_WeaponHandler.EquippedWeapon.GetCurrentAmmo() == 0)
        {
            _WeaponHandler.Reload();
			GD.Print("Enemy Reloaded");
			_ReloadTimer.Start(1.5f);
        }
    }
}

using FPS.Weapons;
using Godot;

namespace FPS.Characters.Enemy;
public partial class EnemyWeaponControls: Node3D
{
    [Export] private WeaponHandler _weaponHandler;
	[Export] private Timer _ReloadTimer;


    public override void _PhysicsProcess(double delta)
    {
        HandleShoot(delta);
		if(_ReloadTimer.IsStopped())
		{
        	HandleReload();
		}
    }

    private void HandleReload()
    {
        if (_weaponHandler.EquippedWeapon.CurrentAmmo == 0)
        {
            _weaponHandler.Reload();
			GD.Print("Enemy Reloaded");
			_ReloadTimer.Start(2.0f);
        }
    }

    private void HandleShoot(double delta)
    {
		_weaponHandler.Shoot(delta);
    }
}
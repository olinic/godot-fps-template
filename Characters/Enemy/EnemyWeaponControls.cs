
using FPS.Weapons;
using Godot;

namespace FPS.Characters.Enemy;
public partial class EnemyWeaponControls: Node3D
{
    [Export] private WeaponHandler _weaponHandler;

    public override void _PhysicsProcess(double delta)
    {
        HandleShoot(delta);
        HandleReload();
    }

    private void HandleReload()
    {
        if (true /*out of ammo*/)
        {
            _weaponHandler.Reload();
        }
    }

    private void HandleShoot(double delta)
    {
        if (_weaponHandler.GetFireMode() == FireMode.Auto)
        {
            if(true /*cooldown timer is stopped*/)
            {
                _weaponHandler.Shoot(delta);
            }
        }
        else
        {
            if(true)
            {
                _weaponHandler.Shoot(delta);
            }
        }
    }
}
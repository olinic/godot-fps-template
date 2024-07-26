
using FPS.Weapons;
using Godot;

namespace FPS.Characters.Player;
public partial class WeaponControls: Node3D
{
    [Export] private WeaponHandler _weaponHandler;
    

    public override void _PhysicsProcess(double delta)
    {
        HandleShoot(delta);
        HandleReload();
        HandleSwapWeapon();
    }

    private void HandleReload()
    {
        if (Input.IsActionJustPressed("reload"))
        {
            _weaponHandler.Reload();
        }
    }

    private void HandleShoot(double delta)
    {
        if (_weaponHandler.GetFireMode() == FireMode.Auto)
        {
            if(Input.IsActionPressed("fire"))
            {
                _weaponHandler.Shoot(delta);
            }
        }
        else
        {
            if(Input.IsActionJustPressed("fire"))
            {
                _weaponHandler.Shoot(delta);
            }
        }
    }

    private void HandleSwapWeapon()
    {
        if(Input.IsActionPressed("weapon_1"))
        {
            _weaponHandler.EquipType(WeaponType.Primary);
        }
        if(Input.IsActionPressed("weapon_2"))
        {
            _weaponHandler.EquipType(WeaponType.Secondary);
        }
        if(Input.IsActionPressed("controller_switch_weapons"))
        {
            _weaponHandler.SwapWeapon();
        }
    }
}

using FPS.Weapons;
using Godot;

namespace FPS.Characters.Player;
public partial class WeaponControls: Node3D
{
    [Export] private WeaponHandler WeaponHandler;

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
            WeaponHandler.Reload();
        }
    }

    private void HandleShoot(double delta)
    {
        if (WeaponHandler.GetFireMode() == FireMode.Auto)
        {
            if(Input.IsActionPressed("fire"))
            {
                WeaponHandler.Shoot(delta);
            }
        }
        else
        {
            if(Input.IsActionJustPressed("fire"))
            {
                WeaponHandler.Shoot(delta);
            }
        }
    }

    private void HandleSwapWeapon()
    {
        if(Input.IsActionPressed("weapon_1"))
        {
            WeaponHandler.EquipType(WeaponType.Primary);
        }
        if(Input.IsActionPressed("weapon_2"))
        {
            WeaponHandler.EquipType(WeaponType.Secondary);
        }
        if(Input.IsActionPressed("controller_switch_weapons"))
        {
            WeaponHandler.SwapWeapon();
        }
    }
}
using System.Collections.Generic;
using Godot;

namespace FPS.Weapons;
public partial class WeaponHandler: Node3D
{
    [Export] private Timer _CooldownTimer;

    [Signal] public delegate void ammo_updateEventHandler(int ammoCount);

    private List<HitscanWeapon> Weapons = new();
    public HitscanWeapon EquippedWeapon { get; private set; }

    public override void _Ready()
    {
        HitscanWeapon primary = InstaniateWeapon("SMG");
        HitscanWeapon secondary = InstaniateWeapon("Rifle");
        Weapons.Add(primary);
        Weapons.Add(secondary);
        Weapons.ForEach(weapon => {
            AddChild(weapon);
        });
        EquipType(WeaponType.Primary);
    }

    private static HitscanWeapon InstaniateWeapon(string name)
    {
        return (HitscanWeapon) GD.Load<PackedScene>("res://Weapons/" + name + ".tscn").Instantiate();
    }

    public void EquipType(WeaponType type)
    {
        Equip(Weapons.Find(weapon => weapon.WeaponType == type));
    }

    public FireMode GetFireMode()
    {
        return EquippedWeapon.FireMode;
    }

    public void Reload()
    {
        EquippedWeapon.Reload();
        EmitAmmoUpdate();
    }

    public void Shoot(double delta)
    {
        if(_CooldownTimer.IsStopped() && EquippedWeapon.CurrentAmmo > 0)
        {
            EquippedWeapon.Shoot(delta);
            _CooldownTimer.Start(1.0f / EquippedWeapon.FireRate);
        }
        EmitAmmoUpdate();
    }

    public void SwapWeapon()
    {
        Equip(Weapons.Find(weapon => weapon != EquippedWeapon));
    }

    public void Equip(HitscanWeapon active_weapon)
    {
        active_weapon.Visible = true;
        active_weapon.SetProcess(true);
        Weapons.FindAll(weapon => weapon != active_weapon).ForEach(UnEquip);
        EquippedWeapon = active_weapon;
        EmitAmmoUpdate();
    }

    private void UnEquip(HitscanWeapon inactive_weapon)
    {  
        inactive_weapon.Visible = false;
        inactive_weapon.SetProcess(false);
    }

    public void EmitAmmoUpdate()
    {
        EmitSignal(SignalName.ammo_update, EquippedWeapon.GetAmmoCapacity(), EquippedWeapon.GetCurrentAmmo());
    }
}
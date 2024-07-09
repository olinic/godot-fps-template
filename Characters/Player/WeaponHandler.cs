using System.Collections.Generic;
using Godot;

public partial class WeaponHandler: Node3D
{
    [Export] private Timer _CooldownTimer;

    [Signal] public delegate void ammo_updateEventHandler(int ammoCount);

    private List<hitscan_weapon> Weapons = new();
    public hitscan_weapon EquippedWeapon { get; private set; }

    public override void _Ready()
    {
        hitscan_weapon primary = InstaniateWeapon("SMG");
        hitscan_weapon secondary = InstaniateWeapon("Rifle");
        Weapons.Add(primary);
        Weapons.Add(secondary);
        Weapons.ForEach(weapon => {
            AddChild(weapon);
        });
        EquipType(WeaponType.Primary);
    }

    private static hitscan_weapon InstaniateWeapon(string name)
    {
        return (hitscan_weapon) GD.Load<PackedScene>("res://Weapons/" + name + ".tscn").Instantiate();
    }

    private void EquipType(WeaponType type)
    {
        Equip(Weapons.Find(weapon => weapon.WeaponType == type));
    }

    public override void _PhysicsProcess(double delta)
    {

        if(EquippedWeapon.FireMode == FireMode.Auto)
        {
            if(Input.IsActionPressed("fire"))
            {
                shoot(delta);
            }
        }
        else
        {
            if(Input.IsActionJustPressed("fire"))
            {
               shoot(delta);
            }
        }
        if(Input.IsActionJustPressed("reload"))
        {
            EquippedWeapon.Reload();
            EmitAmmoUpdate();
        }
    }
    public void shoot(double delta)
    {
        if(_CooldownTimer.IsStopped() && EquippedWeapon.CurrentAmmo > 0)
        {
            EquippedWeapon.Shoot(delta);
            _CooldownTimer.Start(1.0f / EquippedWeapon.FireRate);
        }
        EmitAmmoUpdate();
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event.IsActionPressed("weapon_1"))
        {
            EquipType(WeaponType.Primary);
        }
        if(@event.IsActionPressed("weapon_2"))
        {
            EquipType(WeaponType.Secondary);
        }
        if(@event.IsActionPressed("controller_switch_weapons"))
        {
            SwapWeapon();
        }
    }

    public void SwapWeapon()
    {
        Equip(Weapons.Find(weapon => weapon != EquippedWeapon));
    }

    public void Equip(hitscan_weapon active_weapon)
    {
        active_weapon.Visible = true;
        active_weapon.SetProcess(true);
        Weapons.FindAll(weapon => weapon != active_weapon).ForEach(UnEquip);
        EquippedWeapon = active_weapon;
        EmitAmmoUpdate();
    }

    private void UnEquip(hitscan_weapon inactive_weapon)
    {  
        inactive_weapon.Visible = false;
        inactive_weapon.SetProcess(false);
    }

    public void EmitAmmoUpdate()
    {
        EmitSignal(SignalName.ammo_update, EquippedWeapon.GetAmmoCapacity(), EquippedWeapon.GetCurrentAmmo());
    }
}
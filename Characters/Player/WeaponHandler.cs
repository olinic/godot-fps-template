using System.Collections.Generic;
using Godot;


public partial class WeaponHandler: Node3D
{
    [Export] private Timer _CooldownTimer;

    [Signal] public delegate void ammo_updateEventHandler(int ammoCount);

    private List<hitscan_weapon> Weapons = new();
    private hitscan_weapon _EquippedWeapon;


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

    private hitscan_weapon InstaniateWeapon(string name)
    {
        return (hitscan_weapon) GD.Load<PackedScene>("res://Weapons/" + name + ".tscn").Instantiate();
    }

    private void EquipType(WeaponType type)
    {
        Equip(Weapons.Find(weapon => weapon.WeaponType == type));
    }

    public override void _PhysicsProcess(double delta)
    {

        if(_EquippedWeapon.FireMode == FireMode.Auto)
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
            _EquippedWeapon.Reload();
            UpdateAmmoLabel(_EquippedWeapon.GetAmmoCapacity(), _EquippedWeapon.GetCurrentAmmo());
        }
    }
    public void shoot(double delta)
    {
        if(_CooldownTimer.IsStopped() && _EquippedWeapon.CurrentAmmo > 0)
        {
            _EquippedWeapon.Shoot(delta);
            _CooldownTimer.Start(1.0f / _EquippedWeapon.FireRate);
        }
        UpdateAmmoLabel(_EquippedWeapon.GetAmmoCapacity(), _EquippedWeapon.GetCurrentAmmo());
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
            Equip(Weapons.Find(weapon => weapon != _EquippedWeapon));
        }
    }
    public void Equip(hitscan_weapon active_weapon)
    {
        _EquippedWeapon = active_weapon;
        UpdateAmmoLabel(active_weapon.GetAmmoCapacity(), active_weapon.GetCurrentAmmo());
        active_weapon.Visible = true;
        active_weapon.SetProcess(true);
        Weapons.FindAll(weapon => weapon != active_weapon).ForEach(UnEquip);
    }

    private void UnEquip(hitscan_weapon inactive_weapon)
    {  
        inactive_weapon.Visible = false;
        inactive_weapon.SetProcess(false);
    }

    public void UpdateAmmoLabel(int AmmoCapacity, int CurrentAmmo)
    {
        EmitSignal(SignalName.ammo_update, AmmoCapacity, CurrentAmmo);
    }
}
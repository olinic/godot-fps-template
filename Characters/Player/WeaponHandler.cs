using Godot;


public partial class WeaponHandler: Node3D
{
    
  
    private hitscan_weapon primary;
 
    private hitscan_weapon secondary;
    [Export]
    private Timer _CooldownTimer;
    private hitscan_weapon _EquippedWeapon;

    [Signal]
	public delegate void ammo_updateEventHandler(int ammoCount);

    public override void _Ready()
    {
        primary = (hitscan_weapon) GD.Load<PackedScene>("res://Weapons/SMG.tscn").Instantiate();
        secondary = (hitscan_weapon) GD.Load<PackedScene>("res://Weapons/Rifle.tscn").Instantiate();
        AddChild(primary);
        AddChild(secondary);

        Equip(primary);
        UnEquip(secondary);
    }
    public override void _PhysicsProcess(double delta)
    {

        if(_EquippedWeapon.WeaponType == FireMode.Auto)
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
    }
    public void shoot(double delta)
    {
        if(_CooldownTimer.IsStopped() && _EquippedWeapon.AmmoCapacity > 0)
        {
            _EquippedWeapon.Shoot(delta);
            _CooldownTimer.Start(1.0f / _EquippedWeapon.FireRate);
        }
        UpdateAmmoLabel(_EquippedWeapon.GetAmmoCount());
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event.IsActionPressed("weapon_1"))
        {
            Equip(primary);
            UnEquip(secondary);
        }
        if(@event.IsActionPressed("weapon_2"))
        {
            Equip(secondary);
            UnEquip(primary);
        }
        if(@event.IsActionPressed("controller_switch_weapons"))
        {
            ControllerEquip();
        }
    }
    public void Equip(hitscan_weapon active_weapon)
    {
        _EquippedWeapon = active_weapon;
        UpdateAmmoLabel(active_weapon.GetAmmoCount());
        active_weapon.Visible = true;
        active_weapon.SetProcess(true);
    }
    public void UnEquip(hitscan_weapon inactive_weapon)
    {  
        inactive_weapon.Visible = false;
        inactive_weapon.SetProcess(false);
    }
        
    
    public void ControllerEquip()
    {
        // GD.Print("Equip Method");
        // foreach(hitscan_weapon child in GetChildren())
        // {
        //     if(child.Visible == true)
        //     {
        //         child.Visible = false;
        //         child.SetProcess(false);
        //     }
        //     else
        //     {
        //         child.Visible = true;
        //         child.SetProcess(true);
        //     }
            
        // }
    }
    public void UpdateAmmoLabel(int ammoCount)
    {
        //AmmoLabel.Text = ammoCount.ToString();
        EmitSignal(SignalName.ammo_update, ammoCount);
    }
    
}
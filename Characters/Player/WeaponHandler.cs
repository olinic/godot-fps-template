using Godot;


public partial class WeaponHandler: Node3D
{
    [ExportGroup("My Weapons")]
    [Export] 
    private hitscan_weapon primary;
    [Export] 
    private hitscan_weapon secondary;

    private hitscan_weapon _EquippedWeapon;
    
    [Export]
    public Label AmmoLabel;

    public override void _Ready()
    {
        Equip(primary);
        UnEquip(secondary);
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
        AmmoLabel.Text = ammoCount.ToString();
    }
}
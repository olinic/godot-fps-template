using Godot;


public partial class WeaponHandler: Node3D
{
    [ExportGroup("My Weapons")]
    [Export] 
    private Node3D weapon_1;
    [Export] 
    private Node3D weapon_2;

    public override void _Ready()
    {
        Equip(weapon_1);
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event.IsActionPressed("weapon_1"))
        {
            Equip(weapon_1);
        }
        if(@event.IsActionPressed("weapon_2"))
        {
            Equip(weapon_2);
        }
        if(@event.IsActionPressed("controller_switch_weapons"))
        {
            ControllerEquip();
        }
    }
    public void Equip(Node3D active_weapon)
    {
        foreach(Node3D child in GetChildren())
        {
            if(child == active_weapon)
            {
                child.Visible = true;
                child.SetProcess(true);
            }
            else
            {
                child.Visible = false;
                child.SetProcess(false);
            }
        }
    }
    public void ControllerEquip()
    {
        GD.Print("Equip Method");
        foreach(Node3D child in GetChildren())
        {
            if(child.Visible == true)
            {
                child.Visible = false;
                child.SetProcess(false);
            }
            else
            {
                child.Visible = true;
                child.SetProcess(true);
            }
            
        }
    }
}
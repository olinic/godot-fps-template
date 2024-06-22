using Godot;

public partial class DamageArea : Node3D, ICanAttack
{
    public Attack GetAttack()
    {
        return new Attack() 
        { 
            Damage = 10000 
        };
    }
}

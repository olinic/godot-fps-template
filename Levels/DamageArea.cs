using Godot;

public partial class DamageArea : Area3D, ICanAttack
{
    public Attack GetAttack()
    {
        return new Attack() 
        { 
            Damage = 10000 
        };
    }
}

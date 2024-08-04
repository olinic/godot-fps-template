using Godot;

namespace FPS.Weapons;

public partial class HitscanWeapon : Node3D, IGun, ICanAttack
{
    [Export] public Node3D WeaponMesh;
    [Export] public RayCast3D RayCast;

	public float RoundsPerSecond;
    public float Recoil;
    public int AmmoCapacity;
    public int Damage;
    public FireMode FireMode;
    public WeaponType WeaponType;

    private int _CurrentAmmo;
    private Vector3 _WeaponPosition;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_CurrentAmmo = AmmoCapacity;
        _WeaponPosition = WeaponMesh.Position;

        RayCast.CollideWithAreas = true;
        RayCast.CollideWithBodies = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        WeaponMesh.Position = WeaponMesh.Position.Lerp(_WeaponPosition, (float)delta * 10.0f);  
	}

	public void Shoot(double delta)
    {
        
        _CurrentAmmo--;
        if(RayCast.GetCollider() is ICanTakeDamage target)
        {
            GD.PrintT("Weapon Fired!", RayCast.GetCollider());
            
            target.ApplyDamage(GetAttack());
        }
        WeaponMesh.Position = WeaponMesh.Position with { Z = WeaponMesh.Position.Z + Recoil};
        
    }
    public void Reload()
    {
        _CurrentAmmo = AmmoCapacity;
    }

    public Attack GetAttack()
    {
        return new Attack() { Damage = Damage};
    }

    public int GetCurrentAmmo()
    {
        return _CurrentAmmo;
    }
    public int GetAmmoCapacity()
    {
        return AmmoCapacity;
    }
    
}

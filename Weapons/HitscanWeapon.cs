using Godot;

namespace FPS.Weapons;
public enum FireMode {Auto, Semi};
public partial class HitscanWeapon : Node3D, IGun, ICanAttack
{
	[Export] public float FireRate;
    [Export] public float _Recoil;
    [Export] public Node3D WeaponMesh;
   
    [Export] public int AmmoCapacity;
    [Export] private int _Damage;
    [Export] public FireMode FireMode;
    [Export] public WeaponType WeaponType;
    [Export] public RayCast3D RayCast;

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
        WeaponMesh.Position = WeaponMesh.Position with { Z = WeaponMesh.Position.Z + _Recoil};
        
    }
    public void Reload()
    {
        _CurrentAmmo = AmmoCapacity;
    }

    public Attack GetAttack()
    {
        return new Attack() { Damage = _Damage};
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

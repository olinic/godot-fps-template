using Godot;
public enum FireMode {Auto, Semi};
public partial class hitscan_weapon : Node3D, IGun, ICanAttack
{
	[Export]
    public float FireRate;
    [Export]
    public float _Recoil;
    [Export]
    private Node3D _WeaponMesh;
   
    [Export]
    public int AmmoCapacity;
    [Export]
    private int _Damage;
    [Export]
    public FireMode WeaponType;

    public int CurrentAmmo;
    private Vector3 _WeaponPosition;
    private RayCast3D _RayCast3D;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CurrentAmmo = AmmoCapacity;
        _WeaponPosition = _WeaponMesh.Position;
        _RayCast3D = GetNode<RayCast3D>("RayCast3D");

        _RayCast3D.CollideWithAreas = true;
        _RayCast3D.CollideWithBodies = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        _WeaponMesh.Position = _WeaponMesh.Position.Lerp(_WeaponPosition, (float)delta * 10.0f);  
	}

	public void Shoot(double delta)
    {
        
        CurrentAmmo--;
        if(_RayCast3D.GetCollider() is ICanTakeDamage target)
        {
            GD.PrintT("Weapon Fired!", _RayCast3D.GetCollider());
            
            target.ApplyDamage(GetAttack());
        }
        _WeaponMesh.Position = _WeaponMesh.Position with { Z = _WeaponMesh.Position.Z + _Recoil};
        
    }
    public void Reload()
    {
        CurrentAmmo = AmmoCapacity;
    }

    public Attack GetAttack()
    {
        return new Attack() { Damage = _Damage};
    }

    public int GetCurrentAmmo()
    {
        return CurrentAmmo;
    }
    public int GetAmmoCapacity()
    {
        return AmmoCapacity;
    }
    
}

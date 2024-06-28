using Godot;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

public partial class hitscan_weapon : Node3D, ICanAttack
{
	[Export]
    private float _FireRate = 14.0f;
    [Export]
    private float _Recoil = 0.05f;
    [Export]
    private Node3D _WeaponMesh;

    private Timer _CooldownTimer;
    private Vector3 _WeaponPosition;
    private RayCast3D _RayCast3D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 _CooldownTimer = GetNode<Timer>("CooldownTimer");
        _WeaponPosition = _WeaponMesh.Position;
        _RayCast3D = GetNode<RayCast3D>("RayCast3D");

        _RayCast3D.CollideWithAreas = true;
        _RayCast3D.CollideWithBodies = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		 if(Input.IsActionPressed("fire"))
        {
            if(_CooldownTimer.IsStopped())
            {
                Shoot();
            }
        }
       
        _WeaponMesh.Position = _WeaponMesh.Position.Lerp(_WeaponPosition, (float)delta * 10.0f);
	}
	 private void Shoot()
    {
        
        _CooldownTimer.Start(1.0f / _FireRate);
        if(_RayCast3D.GetCollider() is ICanTakeDamage target)
        {
            GD.PrintT("Weapon Fired!", _RayCast3D.GetCollider());
            
            target.ApplyDamage(GetAttack());
        }
        _WeaponMesh.Position = _WeaponMesh.Position with { Z = _WeaponMesh.Position.Z + _Recoil};
    }

    public Attack GetAttack()
    {
        return new Attack() { Damage = 100};
    }

}

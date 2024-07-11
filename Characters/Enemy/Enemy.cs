using FPS.Weapons;
using Godot;
using System;

namespace FPS.Characters.Enemy;



public partial class Enemy : CharacterBody3D
{
    [Export] WeaponHandler weaponHandler;
    [Export] private Timer _CooldownTimer;
    public override void  _PhysicsProcess(double delta)
    {
        if(_CooldownTimer.IsStopped())
        {
            GD.Print("enemy shot!");
            weaponHandler.Shoot(delta);
            _CooldownTimer.Start();
        }
    }
    public void OnHealthComponentHealthDepleted()
    {   
        QueueFree();
    }
}

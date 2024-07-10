using Godot;
using System;

namespace FPS.Characters.Enemy;

public partial class Enemy : CharacterBody3D
{
    public void OnHealthComponentHealthDepleted()
    {   
        QueueFree();
    }
}

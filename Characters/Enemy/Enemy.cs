using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
    public void OnHealthComponentHealthDepleted()
    {   
        QueueFree();
    }
}

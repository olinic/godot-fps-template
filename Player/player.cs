using Godot;
using System;

public partial class player : CharacterBody3D
{
    public override void _Ready()
    {

    }

    public override void _Input(InputEvent @event)
    {

    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
        Vector3 direction = new Vector3(inputDir.X, 0, inputDir.Y).Normalized();
        float speed = 300;
        
        if (direction != Vector3.Zero)
        {
            Velocity = new Vector3(
                    direction.X * speed * (float)delta,
                    0,
                    direction.Z * speed * (float)delta);

        } 
        else 
        {
            Velocity = Vector3.Zero;
        }
        this.MoveAndSlide();
    }
}

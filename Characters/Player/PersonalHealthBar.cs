using FPS.Characters.Health;
using Godot;
using System;

public partial class PersonalHealthBar : ProgressBar
{

    public void OnHealthComponentHealthChanged(Health health)
    {
        MaxValue = health.Max;
        Value = health.Value;

        Visible = MaxValue != Value;
    }
}

using Godot;

public partial class HealthBar : TextureProgressBar
{

    public void OnHealthComponentHealthChanged(Health health)
    {
        GD.Print("Update health bar");
        MaxValue = health.Max;
        Value = health.Value;
    }
}

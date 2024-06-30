using Godot;

public partial class HealthBar : ProgressBar
{

    public void OnHealthComponentHealthChanged(Health health)
    {
        GD.Print("Update health bar");
        MaxValue = health.Max;
        Value = health.Value;
    }
}

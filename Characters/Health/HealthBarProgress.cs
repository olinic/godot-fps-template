using Godot;

public partial class HealthBarProgress : ProgressBar
{
    public void OnHealthComponentHealthChanged(Health health)
    {
        MaxValue = health.Max;
        Value = health.Value;
    }
}

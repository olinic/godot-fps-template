using Godot;

namespace FPS.Characters.Health;
public partial class HealthBar : Node3D
{
    [Export] public HealthComponent HealthComponent;

    [Export] public HealthBarProgress progress;

    public override void _Ready()
    {
        HealthComponent.health_changed += progress.OnHealthComponentHealthChanged;
    }
}

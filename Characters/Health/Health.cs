using Godot;

namespace FPS.Characters.Health;
public partial class Health: GodotObject
{
    private readonly float _max;
    private readonly float _value;

    public float Max { get => _max; init => _max = value; }
    public float Value { get => _value; init => _value = value; }

}
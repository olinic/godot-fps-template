using Godot;

namespace FPS.Characters.Health;
public partial class Health: GodotObject
{
    private readonly int _max;
    private readonly int _value;

    public int Max { get => _max; init => _max = value; }
    public int Value { get => _value; init => _value = value; }

}
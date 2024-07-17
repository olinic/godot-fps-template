using Godot;

namespace FPS.Characters.Health;
public partial class Health: GodotObject
{
    private int _max;
    private int _value;

    public int Max { get => _max; }
    public int Value { get => _value; }

    public Health(int max, int value)
    {
        _max = max;
        _value = value;
    }

    public static Health WithMaxAndValue(int max, int value)
    {
        return new Health(max, value);
    }

}
using Godot;
public class Fall : Jump
{

    public Fall(IMoveStateProvider provider): base(provider)
    {}

    public override Vector3 GetInitialVelocityChange()
    {
        return new Vector3(0, 0, 0);
    }
}
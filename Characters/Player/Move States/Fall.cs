using Godot;
public class Fall : Jump
{

    public Fall(IMoveStateProvider provider): base(provider)
    {}

    public override Vector3 GetInitialVelocity(Vector3 currVelocity)
    {
        AerialVelocity = currVelocity;
        return AerialVelocity;
    }
}
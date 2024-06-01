using Godot;
public class Fall : Jump
{

    public Fall(IMoveStateProvider provider): base(provider)
    {}

    public override Vector3 GetInitialVelocity(Vector3 currVelocity)
    {
        return currVelocity with {};
    }

    public void SetCurrentVelocity(Vector3 aerialDir)
    {
        this.AerialVelocity = aerialDir with {};
    }
}
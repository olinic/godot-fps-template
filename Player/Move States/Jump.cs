using Godot;

public class Jump : IMoveState
{
    public Vector3 GetInitialVelocityChange()
    {
        return new Vector3(1, 1, 1);
    }

    public Optional<IMoveState> GetNextState(Vector2 inputDir, bool isPlayerOnFloor)
    {
        return Optional<IMoveState>.Empty();
    }

    public Vector3 GetVelocity(float delta, Vector2 inputDir, Basis playerBasis)
    {
        return Vector3.Zero;
    }
}
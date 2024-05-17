
using System.Numerics;

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

    public Vector3 GetVelocity(float delta, Vector2 inputDir)
    {
        return Vector3.Zero;
    }
}
using Godot;

public interface IMoveState
{
    Vector3 GetInitialVelocityChange()
    {
        return Vector3.Zero;
    }
    Vector3 GetVelocity(float delta, Vector2 inputDir, Basis playerBasis);
    Optional<IMoveState> GetNextState(Vector2 inputDir, bool isPlayerOnFloor);
}
using Godot;
public class Fall : IMoveState
{

    public Optional<IMoveState> GetNextState(Vector2 inputDir, bool isPlayerOnFloor)
    {
        return Optional<IMoveState>.Empty();
    }

    public Vector3 GetVelocity(float delta, Vector2 inputDir, Basis playerBasis)
    {
        return Vector3.Zero;
    }  
}
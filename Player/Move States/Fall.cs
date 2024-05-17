using System.Numerics;
public class Fall : IMoveState
{

    public Optional<IMoveState> GetNextState(Vector2 inputDir, bool isPlayerOnFloor)
    {
        return Optional<IMoveState>.Empty();
    }

    public Vector3 GetVelocity(float delta, Vector2 inputDir)
    {
        return Vector3.Zero;
    }  
}
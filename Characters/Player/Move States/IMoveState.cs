using Godot;

namespace FPS.Characters.Player.Movestates;
public interface IMoveState
{
    Vector3 GetInitialVelocity(Vector3 currentVelocity)
    {
        return currentVelocity with {};
    }
    Vector3 GetVelocity(float delta, Vector2 inputDir, Basis playerBasis);
    Optional<IMoveState> GetNextState(Vector2 inputDir, bool isPlayerOnFloor);
}
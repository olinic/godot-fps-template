
using System.Numerics;

public interface IMoveStateProvider
{
    Walk GetWalk();
    Sprint GetSprint();
    Jump GetJumpWith(Vector3 aerialDir, float speed, IMoveState nextState);
    Fall GetFall();
}
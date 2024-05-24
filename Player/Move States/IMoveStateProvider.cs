using Godot;


public interface IMoveStateProvider
{
    Walk GetWalk();
    Sprint GetSprint();
    Jump GetJumpWith(Vector3 aerialDir, IMoveState targetState);
    Fall GetFall();
}
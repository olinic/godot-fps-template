using Godot;


public interface IMoveStateProvider
{
    Walk GetWalk();
    Sprint GetSprint();
    Jump GetJumpWith(Vector3 currVelocity, IMoveState targetState);
    DoubleJump GetDoubleJumpWith(Vector3 currVelocity, IMoveState targetState);
    Fall GetFallWith(Vector3 currVelocity, IMoveState targetState);
}
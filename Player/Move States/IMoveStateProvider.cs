using Godot;


public interface IMoveStateProvider
{
    Walk GetWalk();
    Sprint GetSprint();
    Jump GetJumpWith(IMoveState targetState);
    DoubleJump GetDoubleJumpWith(IMoveState targetState);
    Fall GetFallWith(IMoveState targetState);
}
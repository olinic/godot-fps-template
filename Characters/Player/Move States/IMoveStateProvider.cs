using Godot;

namespace FPS.Characters.Player.Movestates;
public interface IMoveStateProvider
{
    Walk GetWalk();
    Sprint GetSprint();
    Jump GetJumpWith(IMoveState targetState);
    Fall GetFallWith(IMoveState targetState);
}
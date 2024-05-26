using Godot;

public class Sprint : Walk
{

    private const float SPRINT_LIMIT_ANGLE_MULTIPLIER = 0.22f; // 0.25 = 45 degrees
    private const float SPRINT_LIMIT_ANGLE_LEFT = -Mathf.Pi * SPRINT_LIMIT_ANGLE_MULTIPLIER;
    private const float SPRINT_LIMIT_ANGLE_RIGHT = -Mathf.Pi * (1 - SPRINT_LIMIT_ANGLE_MULTIPLIER);

    public static bool IsMovingForward(Vector2 inputDir)
    {
        var angle = inputDir.Angle();
        return (angle <= SPRINT_LIMIT_ANGLE_LEFT)
                && (angle >= SPRINT_LIMIT_ANGLE_RIGHT);
    }

    public Sprint(IMoveStateProvider provider): base(provider)
    {
        this._speed = 500;
    }

    public override Optional<IMoveState> GetNextState(Vector2 inputDir, bool isPlayerOnFloor)
    {
        if (!isPlayerOnFloor)
        {
            return Optional<IMoveState>.Of(_provider.GetFallWith(_velocity, this));
        }
        if (Input.IsActionJustPressed("jump"))
        {
            return Optional<IMoveState>.Of(_provider.GetJumpWith(_velocity, this));
        }
        else if (Input.IsActionJustReleased("keyboard_sprint")
			|| !Sprint.IsMovingForward(inputDir))
        {
    		return Optional<IMoveState>.Of(_provider.GetWalk());
        }
        else
        {
            return _empty;
        }
    }

}
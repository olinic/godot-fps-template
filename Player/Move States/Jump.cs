using System;
using Godot;


public class Jump : IMoveState
{
    protected IMoveStateProvider _provider;
    protected readonly Optional<IMoveState> _empty = Optional<IMoveState>.Empty();
    protected Vector3 AerialVelocity = Vector3.Zero;
    protected float AdjustedVelocity;

    
    private readonly float Gravity = (float) ProjectSettings.GetSetting("physics/3d/default_gravity");
    private IMoveState TargetState;
    private const float JumpHeight = 1.5f;
    private const float FallMultiplier = 1.8f;

    public Jump(IMoveStateProvider provider)
    {
        this._provider = provider;
        AdjustedVelocity = (float) Math.Sqrt(JumpHeight/Gravity * 2.0f) *  Gravity;
    }

    public virtual Vector3 GetInitialVelocity(Vector3 currentVelocity)
    {
        AerialVelocity = currentVelocity with 
        { 
            X = currentVelocity.X, 
            Y = AdjustedVelocity, 
            Z = currentVelocity.Z 
        };
        return AerialVelocity;
    }

    public Vector3 GetVelocity(float delta, Vector2 inputDir, Basis playerBasis)
    {

        if(AerialVelocity.Y >= 0)
        {
            AerialVelocity = AerialVelocity with { Y = AerialVelocity.Y - (Gravity * delta)};
        }
        else
        {
            AerialVelocity = AerialVelocity with { Y = AerialVelocity.Y - (Gravity * delta * FallMultiplier)};
        }
        return AerialVelocity;
    }

    public virtual Optional<IMoveState> GetNextState(Vector2 inputDir, bool isPlayerOnFloor)
    {
        if (isPlayerOnFloor)
        {
		    return Optional<IMoveState>.Of(TargetState);
        }
        else if(Input.IsActionJustPressed("jump"))
        {
            return Optional<IMoveState>.Of(_provider.GetDoubleJumpWith(TargetState));
        }
        else
        {
    	    return _empty;
        }
    }

    public void SetTargetState(IMoveState targetState)
    {
        this.TargetState = targetState;
    }

    public IMoveState GetTargetState()
    {
        return this.TargetState;
    }

    public float GetGravity()
    {
        return Gravity;
    }

    public float GetJumpHeight()
    {
        return JumpHeight;
    }
}
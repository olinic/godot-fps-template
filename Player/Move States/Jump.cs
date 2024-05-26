using System;
using System.Runtime.CompilerServices;
using Godot;


public class Jump : IMoveState
{
    private float Gravity = (float) ProjectSettings.GetSetting("physics/3d/default_gravity");
    private Vector3 AerialVelocity = Vector3.Zero;
    private Optional<IMoveState> NextState = Optional<IMoveState>.Empty();
    private IMoveState TargetState;

    protected IMoveStateProvider _provider;
    protected readonly Optional<IMoveState> _empty = Optional<IMoveState>.Empty();
    protected Vector3 _velocity;

    
    [Export]
    public float JumpHeight = 1.5f;
    [Export]
    public float FallMultiplier = 1.8f;

    public Jump(IMoveStateProvider provider)
    {
        this._provider = provider;
    }

    public Vector3 GetInitialVelocityChange()
    {
        return new Vector3(0,(float) Math.Sqrt(JumpHeight/Gravity * 2.0f) *  Gravity, 0);
    }

    public virtual Optional<IMoveState> GetNextState(Vector2 inputDir, bool isPlayerOnFloor)
    {
        if (isPlayerOnFloor)
        {
		    return Optional<IMoveState>.Of(TargetState);
        }
        else
        {
    	    return _empty;
        }
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
    
    public void SetAerialDir(Vector3 aerialDir)
    {
        this.AerialVelocity = aerialDir + GetInitialVelocityChange();
    }

    public void SetTargetState(IMoveState targetState)
    {
        this.TargetState = targetState;
    }

}
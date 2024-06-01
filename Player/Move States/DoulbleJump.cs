using System;
using System.Runtime.CompilerServices;
using Godot;

public class DoubleJump : Jump
{

    public DoubleJump(IMoveStateProvider provider): base(provider)
    {}

    public override Optional<IMoveState> GetNextState(Vector2 inputDir, bool isPlayerOnFloor)
    {
        if (isPlayerOnFloor)
        {
		    return Optional<IMoveState>.Of(GetTargetState());
        }
        else
        {
    	    return _empty;
        }
    }

    public override void SetCurrentVelocity(Vector3 aerialDir)
    {
        // Ignore current Y velocity for a nice double jump feel
        this.AerialVelocity = aerialDir with { Y = AdjustedVelocity }; 
    }
}
using System.Security.Cryptography.X509Certificates;
using Godot;

namespace FPS.Characters.Player.Movestates;
public class Walk : IMoveState
{
    protected float _speed = 300.0f;
    protected IMoveStateProvider _provider;
    protected readonly Optional<IMoveState> _empty = Optional<IMoveState>.Empty();

    public Walk(IMoveStateProvider provider)
    {
        this._provider = provider;
    }

    public Vector3 GetVelocity(float delta, Vector2 inputDir, Basis playerBasis)
    {
        Vector3 direction = new Vector3(inputDir.X, 0, inputDir.Y).Normalized();
	    Vector3 velocity = Vector3.Zero;
        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * _speed * delta;
            velocity.Z = direction.Z * _speed * delta;
        }
        velocity = playerBasis * velocity;
        return velocity;
        
    }   
     
    public virtual Optional<IMoveState> GetNextState(Vector2 inputDir, bool isPlayerOnFloor)
    {
        if (!isPlayerOnFloor)
        {
            return Optional<IMoveState>.Of(_provider.GetFallWith(this));
        }
        if (Input.IsActionJustPressed("sprint") && Sprint.IsMovingForward(inputDir))
        {
		    return Optional<IMoveState>.Of(_provider.GetSprint());
        }
        else if(Input.IsActionJustPressed("jump"))
        {
            return Optional<IMoveState>.Of(_provider.GetJumpWith(this));
        }
        else
        {
            return _empty;
        }
    }

    public float GetSpeed()
    {
        return _speed;
    }
}
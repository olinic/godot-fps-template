using System.Security.Cryptography.X509Certificates;
using Godot;
public class Walk : IMoveState
{
    protected float _speed = 300.0f;
    protected IMoveStateProvider _provider;
    protected readonly Optional<IMoveState> _empty = Optional<IMoveState>.Empty();
    protected Godot.Vector3 _velocity;

    public Walk(IMoveStateProvider provider)
    {
        this._provider = provider;
    }

    public Vector3 GetVelocity(float delta, Vector2 inputDir, Basis playerBasis)
    {
        Vector3 direction = new Vector3(inputDir.X, 0, inputDir.Y).Normalized();
	    
        if (direction != Vector3.Zero)
        {
            _velocity.X = direction.X * _speed * delta;
            _velocity.Z = direction.Z * _speed * delta;
        }
        else
        {
            _velocity = Vector3.Zero;
        }
        _velocity = playerBasis * _velocity;
        return _velocity;
        
    }   
     
    public virtual Optional<IMoveState> GetNextState(Vector2 inputDir, bool isPlayerOnFloor)
    {
        if (!isPlayerOnFloor)
        {
            return Optional<IMoveState>.Of(_provider.GetFallWith(_velocity, this));
        }
        if (Input.IsActionJustPressed("sprint") && Sprint.IsMovingForward(inputDir))
        {
		    return Optional<IMoveState>.Of(_provider.GetSprint());
        }
        else if(Input.IsActionJustPressed("jump"))
        {
            return Optional<IMoveState>.Of(_provider.GetJumpWith(_velocity, this));
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
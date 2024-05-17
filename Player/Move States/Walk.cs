using System.Security.Cryptography.X509Certificates;
using Godot;
public class Walk : IMoveState
{
    private float _speed = 300.0f;
    private IMoveStateProvider _provider;
    private Optional<IMoveState> _nextState = Optional<IMoveState>.Empty();
    private Vector3 _velocity;

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
     
    public Optional<IMoveState> GetNextState(Vector2 inputDir, bool isPlayerOnFloor)
    {
        return Optional<IMoveState>.Empty();
    }

}
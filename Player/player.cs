using Godot;
using System;
using System.ComponentModel;

public partial class player : CharacterBody3D, IMoveStateProvider
{
	private const float ControllerLookMulitplier = 7; 
	private const float VerticalLookLowerLimit = -90; 
	private const float VerticalLookUpperLimit = 90; 
	[Export]
	private int HorizontalLookSetting = 6;
	[Export]
	private int VerticalLookSetting = 6;

	private float HorizontalLookSensitivity;
	private float VerticalLookSensitivity;

	private Node3D CameraController;

	private IMoveState _moveState;
	private Walk _walk;
	private Sprint _sprint;
	private Jump _jump;

	private Vector2 MouseMotion = Vector2.Zero;

	public player()
	{
		_walk = new Walk(this);
		_sprint = new Sprint(this);
		_jump = new Jump(this);
		ChangeState(_walk);
	}
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		HorizontalLookSensitivity = SettingToSensitivity(HorizontalLookSetting);
		VerticalLookSensitivity = SettingToSensitivity(VerticalLookSetting);
		CameraController = GetNode<Node3D>("CameraController");
	}
	public float SettingToSensitivity(int setting)
	{
		//6 is regular speed. Adjust by 10% higher or lower based on setting.
		return 0.4f + (0.1f * setting);
	}

	public override void _Input(InputEvent @event )
	{
		if(@event is InputEventMouseMotion eventMouseMotion)
		{
			MouseMotion = -eventMouseMotion.Relative * 0.06f;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		LookAround((float)delta);
		Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
		Velocity = _moveState.GetVelocity((float)delta, inputDir, Transform.Basis);
		_moveState.GetNextState(inputDir, IsOnFloor()).IfPresent(ChangeState);
		MoveAndSlide();
	}

	public void LookAround(float delta)
	{
		if (MouseMotion != Vector2.Zero)
		{
			MouseLook(delta);
		}
		else
		{
			ControllerLook(delta);
		}
	}
	public void MouseLook(float delta){
		AdjustCameraLook(delta, MouseMotion);
		MouseMotion = Vector2.Zero;
	}
	public void ControllerLook(float delta)
	{
		Vector2 AimDir = Input.GetVector("aim_left", "aim_right", "aim_up", "aim_down");
		AdjustCameraLook(delta, -AimDir * ControllerLookMulitplier);
	}
	public void AdjustCameraLook(float delta, Vector2 lookRotation)
	{
		RotateY(lookRotation.X * delta * HorizontalLookSensitivity );
		CameraController.RotateX(lookRotation.Y * delta * VerticalLookSensitivity);
		CameraController.RotationDegrees = new Vector3(
			Mathf.Clamp(
				CameraController.RotationDegrees.X, 
				VerticalLookLowerLimit,
				VerticalLookUpperLimit
			),
			CameraController.RotationDegrees.Y,
			CameraController.RotationDegrees.Z
		);
		
	}

	private void ChangeState(IMoveState newState)
	{
		_moveState = newState;
		GD.Print("Changed state to " + _moveState.GetType().Name);
		Velocity = Velocity with {Y = Velocity.Y + _moveState.GetInitialVelocityChange().Y};
	}

    public Walk GetWalk()
    {
        return _walk;
    }

    public Sprint GetSprint()
    {
        return _sprint;
    }

    public Jump GetJumpWith(Vector3 aerialDir, IMoveState targetState)
    {
		_jump.SetAerialDir(aerialDir);
		_jump.SetTargetState(targetState);
        return _jump;
    }

    public Fall GetFall()
    {
        throw new NotImplementedException();
    }

}

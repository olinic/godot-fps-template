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

	private Vector2 MouseMotion = Vector2.Zero;

	public player()
	{
		_moveState = new Walk(this);
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
		
		MoveAndSlide();
	}
	public void LookAround(float delta)
	{
		if (MouseMotion != Vector2.Zero)
		{
			MouseLook(delta);
			Console.WriteLine("Sensing Mouse Motion");
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

    public Walk GetWalk()
    {
        throw new NotImplementedException();
    }

    public Sprint GetSprint()
    {
        throw new NotImplementedException();
    }

    public Jump GetJumpWith(System.Numerics.Vector3 aerialDir, float speed, IMoveState nextState)
    {
        throw new NotImplementedException();
    }

    public Fall GetFall()
    {
        throw new NotImplementedException();
    }

}

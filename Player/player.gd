class_name Player

extends CharacterBody3D

const CONTROLLER_LOOK_MULTIPLIER: float = 7
const VERTICAL_LOOK_LOWER_LIMIT: float = -90
const VERTICAL_LOOK_UPPER_LIMIT: float = 90

@export var horizontal_look_setting: int = 6
@export var vertical_look_setting: int = 6

var _move_state = Walk.new(self, self.is_on_floor)
var _horizontal_look_sensitivity: float
var _vertical_look_sensitivity: float

var _mouse_motion: Vector2 = Vector2.ZERO

@onready var camera_controller = $CameraController

func change_move_state(state):
	self._move_state = state
	print("Changed state to ", _move_state.get_name())
	velocity += self._move_state.get_initial_velocity_change()
	
func get_move_state():
	return self._move_state

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	_horizontal_look_sensitivity = _setting_to_senstivity(horizontal_look_setting)
	_vertical_look_sensitivity = _setting_to_senstivity(vertical_look_setting)

func _setting_to_senstivity(setting: int) -> float:
	# 6 is regular speed. Adjust by 10% higher or lower based on setting.
	return 0.4 + (0.1 * setting)

func _input(event):
	if event is InputEventMouseMotion:
		_mouse_motion = -event.relative * 0.06

func _physics_process(delta):
	_look_around(delta)
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
	var move_velocity = _move_state.get_velocity(delta, input_dir)
	set_velocity(move_velocity)
	_move_state.get_next_state(input_dir).if_present(change_move_state)
	move_and_slide()


func _look_around(delta):
	if _mouse_motion != Vector2.ZERO:
		_mouse_look(delta)
	else:
		_controller_look(delta)
	
func _mouse_look(delta):
	_adjust_camera_look(delta, _mouse_motion)
	_mouse_motion = Vector2.ZERO

func _controller_look(delta):
	var aim_dir = Input.get_vector("aim_left", "aim_right", "aim_up", "aim_down")
	_adjust_camera_look(delta, -aim_dir * CONTROLLER_LOOK_MULTIPLIER)

func _adjust_camera_look(delta, look_rotation: Vector2):
	rotate_y(look_rotation.x * delta * _horizontal_look_sensitivity)
	camera_controller.rotate_x(look_rotation.y * delta * _vertical_look_sensitivity)
	camera_controller.rotation_degrees.x = clampf(
		camera_controller.rotation_degrees.x, 
		VERTICAL_LOOK_LOWER_LIMIT, 
		VERTICAL_LOOK_UPPER_LIMIT
	)
	
func move(direction: Vector3, speed: float, delta: float):
	speed *= delta
	if direction:
		velocity.x = direction.x * speed 
		velocity.z = direction.z * speed 
	else:
		velocity.x = move_toward(velocity.x, 0, speed)
		velocity.z = move_toward(velocity.z, 0, speed)
	move_and_slide()

class_name Player

extends CharacterBody3D

const SPRINT_SPEED: float = 600.0
const SPEED: float = 300.0
const JUMP_VELOCITY: float = 4.5
const CONTROLLER_LOOK_MULTIPLIER: float = 7
const VERTICAL_LOOK_LOWER_LIMIT: float = -90
const VERTICAL_LOOK_UPPER_LIMIT: float = 90
const SPRINT_LIMIT_ANGLE_MULTIPLIER: float = 0.22 # 0.25 = 45 degrees
const SPRINT_LIMIT_ANGLE_LEFT: float = -PI * SPRINT_LIMIT_ANGLE_MULTIPLIER
const SPRINT_LIMIT_ANGLE_RIGHT: float = -PI * (1 - SPRINT_LIMIT_ANGLE_MULTIPLIER)

@export var horizontal_look_setting: int = 6
@export var vertical_look_setting: int = 6

var state = Walk.new(self)
var speed = SPEED * .01666
var horizontal_look_sensitivity: float
var vertical_look_sensitivity: float

var is_sprinting: bool = false
var mouse_motion: Vector2 = Vector2.ZERO
var aerial_dir: Vector3 = Vector3.ZERO

var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")
@onready var camera_controller = $CameraController

func change_state(state):
	self.state = state
	

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	horizontal_look_sensitivity = setting_to_senstivity(horizontal_look_setting)
	vertical_look_sensitivity = setting_to_senstivity(vertical_look_setting)
	

func setting_to_senstivity(setting: int) -> float:
	# 6 is regular speed. Adjust by 10% higher or lower based on setting.
	return 0.4 + (0.1 * setting)

func _input(event):
	if event is InputEventMouseMotion:
		mouse_motion = -event.relative * 0.06

func _physics_process(delta):
	look_around(delta)
	apply_gravity(delta)
	state.process(delta)
	
	

func apply_gravity(delta):
	if not is_on_floor():
		velocity.y -= gravity * delta

func look_around(delta):
	if mouse_motion != Vector2.ZERO:
		mouse_look(delta)
	else:
		controller_look(delta)
	
func mouse_look(delta):
	adjust_camera_look(delta, mouse_motion)
	mouse_motion = Vector2.ZERO

func controller_look(delta):
	var aim_dir = Input.get_vector("aim_left", "aim_right", "aim_up", "aim_down")
	adjust_camera_look(delta, -aim_dir * CONTROLLER_LOOK_MULTIPLIER)

func adjust_camera_look(delta, look_rotation: Vector2):
	rotate_y(look_rotation.x * delta * horizontal_look_sensitivity)
	camera_controller.rotate_x(look_rotation.y * delta * vertical_look_sensitivity)
	camera_controller.rotation_degrees.x = clampf(
		camera_controller.rotation_degrees.x, 
		VERTICAL_LOOK_LOWER_LIMIT, 
		VERTICAL_LOOK_UPPER_LIMIT
	)
func is_moving_forward(input_direction: Vector2):
	var angle = input_direction.angle()
	return ((angle <= SPRINT_LIMIT_ANGLE_LEFT)
			and (angle >= SPRINT_LIMIT_ANGLE_RIGHT))
	
func move(direction: Vector3, speed: float, delta: float):
	speed *= delta
	if direction:
		velocity.x = direction.x * speed 
		velocity.z = direction.z * speed 
	else:
		velocity.x = move_toward(velocity.x, 0, speed)
		velocity.z = move_toward(velocity.z, 0, speed)
	
	move_and_slide()

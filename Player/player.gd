extends CharacterBody3D

const SPRINT_SPEED: float = 600.0
const SPEED: float = 300.0
const JUMP_VELOCITY: float = 4.5
const CONTROLLER_LOOK_MULTIPLIER: float = 0.08
const VERTICAL_LOOK_LOWER_LIMIT: float = -90
const VERTICAL_LOOK_UPPER_LIMIT: float = 90
const SPRINT_LIMIT_ANGLE_MULTIPLIER: float = 0.22 # 0.25 = 45 degrees
const SPRINT_LIMIT_ANGLE_LEFT: float = -PI * SPRINT_LIMIT_ANGLE_MULTIPLIER
const SPRINT_LIMIT_ANGLE_RIGHT: float = -PI * (1 - SPRINT_LIMIT_ANGLE_MULTIPLIER)
var is_sprinting: bool = false

var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")
@onready var camera_controller = $CameraController


var mouse_motion: Vector2 = Vector2.ZERO

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)

func _input(event):
	if event is InputEventMouseMotion:
		mouse_motion = -event.relative * 0.001

func _physics_process(delta):
	aim()
	apply_gravity(delta)
	move(delta)
	handle_jump()

func apply_gravity(delta):
	if not is_on_floor():
		velocity.y -= gravity * delta

func move(delta):
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
	handle_sprint(input_dir)
	var direction = (transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	var speed = get_speed(delta)
	if direction:
		velocity.x = direction.x * speed
		velocity.z = direction.z * speed
	else:
		velocity.x = move_toward(velocity.x, 0, speed)
		velocity.z = move_toward(velocity.z, 0, speed)
	move_and_slide()

func handle_sprint(input_direction: Vector2):
	if ((Input.is_action_just_pressed("keyboard_sprint") or Input.is_action_just_pressed("controller_sprint")) 
			and is_on_floor() 
			and is_moving_forward(input_direction)):
		is_sprinting = true
	if Input.is_action_just_released("keyboard_sprint"):
		is_sprinting = false
	if !is_moving_forward(input_direction):
		is_sprinting = false

func is_moving_forward(input_direction: Vector2):
	var angle = input_direction.angle()
	return ((angle <= SPRINT_LIMIT_ANGLE_LEFT)
			and (angle >= SPRINT_LIMIT_ANGLE_RIGHT))

func get_speed(delta):
	return (SPRINT_SPEED if is_sprinting else SPEED) * delta

func aim():
	if mouse_motion != Vector2.ZERO:
		mouse_look()
	else:
		controller_look()
	
func mouse_look():
	adjust_camera_look(mouse_motion)
	mouse_motion = Vector2.ZERO

func controller_look():
	var aim_dir = Input.get_vector("aim_left", "aim_right", "aim_up", "aim_down")
	adjust_camera_look(-aim_dir * CONTROLLER_LOOK_MULTIPLIER)

func adjust_camera_look(look_rotation: Vector2):
	rotate_y(look_rotation.x)
	camera_controller.rotate_x(look_rotation.y)
	camera_controller.rotation_degrees.x = clampf(
		camera_controller.rotation_degrees.x, 
		VERTICAL_LOOK_LOWER_LIMIT, 
		VERTICAL_LOOK_UPPER_LIMIT
	)

func handle_jump():
	if Input.is_action_just_pressed("jump") and is_on_floor():
		velocity.y = JUMP_VELOCITY

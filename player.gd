extends CharacterBody3D

const SPEED: float = 5.0
const JUMP_VELOCITY: float = 4.5
@export var HORIZONTAL_LOOK_SENSITIVITY: float
@export var VERTICAL_LOOK_SENSITIVITY: float
const CONTROLLER_LOOK_MULTIPLIER: int = 50
const VERTICAL_LOOK_LOWER_LIMIT: float = -89
const VERTICAL_LOOK_UPPER_LIMIT: float = 89

var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")
var camera: Camera3D
var mouseDelta: Vector2 = Vector2()

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	camera = get_node("CameraController/Camera3D")

func _input(event):
	if event is InputEventMouseMotion:
		mouseDelta = event.relative

func _physics_process(delta):
	apply_gravity(delta)
	move(delta)
	aim(delta)
	handle_jump()

func apply_gravity(delta):
	if not is_on_floor():
		velocity.y -= gravity * delta

func getSpeed():
	return SPEED

func move(delta):
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
	var direction = (transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	if direction:
		velocity.x = direction.x * getSpeed()
		velocity.z = direction.z * getSpeed()
	else:
		velocity.x = move_toward(velocity.x, 0, getSpeed())
		velocity.z = move_toward(velocity.z, 0, getSpeed())
	move_and_slide()

func aim(delta):
	if mouseDelta != Vector2.ZERO:
		mouse_look(delta)
	else:
		controller_look(delta)
	
func mouse_look(delta):
	adjust_camera_look(delta, mouseDelta)
	mouseDelta = Vector2()

func controller_look(delta):
	var aim_dir = Input.get_vector("aim_left", "aim_right", "aim_up", "aim_down")
	adjust_camera_look(delta, aim_dir * CONTROLLER_LOOK_MULTIPLIER)

func adjust_camera_look(delta, look_rotation: Vector2):
	look_rotation *= delta
	camera.rotation_degrees.x -= look_rotation.y * VERTICAL_LOOK_SENSITIVITY
	camera.rotation_degrees.x = clamp(camera.rotation_degrees.x, VERTICAL_LOOK_LOWER_LIMIT, VERTICAL_LOOK_UPPER_LIMIT)
	rotation_degrees.y -= look_rotation.x * HORIZONTAL_LOOK_SENSITIVITY

func handle_jump():
	if Input.is_action_just_pressed("jump") and is_on_floor():
		velocity.y = JUMP_VELOCITY

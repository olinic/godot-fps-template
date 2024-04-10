extends CharacterBody3D

const SPEED = 5.0
const JUMP_VELOCITY = 4.5
@export var LOOK_SENSITIVITY: float
const TILT_LOWER_LIMIT: float = -89
const TILT_UPPER_LIMIT: float = 89

# Get the gravity from the project settings to be synced with RigidBody nodes.
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
	adjust_camera_look(delta)
	handle_jump()

func apply_gravity(delta):
	if not is_on_floor():
		velocity.y -= gravity * delta
	
func move(delta):
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
	var direction = (transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	if direction:
		velocity.x = direction.x * SPEED
		velocity.z = direction.z * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
		velocity.z = move_toward(velocity.z, 0, SPEED)
	move_and_slide()

func adjust_camera_look(delta):
	var look_rotation = Vector3(mouseDelta.y, mouseDelta.x, 0) * LOOK_SENSITIVITY * delta
	camera.rotation_degrees.x -= look_rotation.x
	camera.rotation_degrees.x = clamp(camera.rotation_degrees.x, TILT_LOWER_LIMIT, TILT_UPPER_LIMIT)
	rotation_degrees.y -= look_rotation.y
	mouseDelta = Vector2()

func handle_jump():
	if Input.is_action_just_pressed("jump") and is_on_floor():
		velocity.y = JUMP_VELOCITY

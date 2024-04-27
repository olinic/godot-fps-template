class_name Walk

const SPEED: float = 300.0
const JUMP_VELOCITY: float = 4.5
const SPRINT_LIMIT_ANGLE_MULTIPLIER: float = 0.22 # 0.25 = 45 degrees
const SPRINT_LIMIT_ANGLE_LEFT: float = -PI * SPRINT_LIMIT_ANGLE_MULTIPLIER
const SPRINT_LIMIT_ANGLE_RIGHT: float = -PI * (1 - SPRINT_LIMIT_ANGLE_MULTIPLIER)

var player

var is_sprinting: bool = false
var aerial_dir: Vector3 = Vector3.ZERO

func _init(player: Player) -> void:
	self.player = player
	print("Entered Walk State.")

func process(delta):
	move(delta)
	
func move(delta):
	player.set_speed(SPEED, delta)
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
	handle_sprint(input_dir)
	var direction: Vector3
	direction = (player.transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	var speed = player.get_speed()
	
	if direction:
		player.velocity.x = direction.x * speed
		player.velocity.z = direction.z * speed
	else:
		player.velocity.x = move_toward(player.velocity.x, 0, speed)
		player.velocity.z = move_toward(player.velocity.z, 0, speed)
	
	player.move_and_slide()
	handle_jump(direction)

func handle_sprint(input_direction: Vector2):
	if ((Input.is_action_just_pressed("keyboard_sprint") or Input.is_action_just_pressed("controller_sprint")) 
			and player.is_on_floor() 
			and player.is_moving_forward(input_direction)):
		player.change_state(Sprint.new(player))


func get_speed(delta):
	return SPEED * delta

func handle_jump(direction: Vector3):
	if Input.is_action_just_pressed("jump") and player.is_on_floor():
		player.change_state(Jump.new(player, direction))

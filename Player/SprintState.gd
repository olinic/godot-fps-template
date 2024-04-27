class_name Sprint

const SPRINT_LIMIT_ANGLE_MULTIPLIER: float = 0.22 # 0.25 = 45 degrees
const SPRINT_LIMIT_ANGLE_LEFT: float = -PI * SPRINT_LIMIT_ANGLE_MULTIPLIER
const SPRINT_LIMIT_ANGLE_RIGHT: float = -PI * (1 - SPRINT_LIMIT_ANGLE_MULTIPLIER)
const SPRINT_SPEED: float = 600.0
const JUMP_VELOCITY: float = 4.5

var player
var aerial_dir: Vector3 = Vector3.ZERO

func _init(player: Player) -> void:
	self.player = player
	print("Entered Sprint State.")

func process(delta):
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
	if (Input.is_action_just_released("keyboard_sprint") 
			or !player.is_moving_forward(input_dir)):
		player.change_state(Walk.new(player))
		
	var direction: Vector3
	if player.is_on_floor():
		direction = (player.transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	else:
		direction = aerial_dir
	var speed = get_speed(delta)
	if direction:
		
		player.velocity.x = direction.x * speed
		player.velocity.z = direction.z * speed
	else:
		player.velocity.x = move_toward(player.velocity.x, 0, speed)
		player.velocity.z = move_toward(player.velocity.z, 0, speed)
	player.move_and_slide()
	handle_jump(direction)
	
func get_speed(delta):
	return SPRINT_SPEED * delta

func handle_jump(direction):
	if Input.is_action_just_pressed("jump") and player.is_on_floor():
		player.velocity.y = JUMP_VELOCITY
		aerial_dir = direction
	


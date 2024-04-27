class_name Jump

const SPEED: float = 300.0
const JUMP_VELOCITY: float = 4.5

var player

var aerial_dir: Vector3 = Vector3.ZERO

func _init(player: Player) -> void:
	self.player = player
	print("Entered Jump State.")

func process(delta):
	handle_jump()
	
func handle_jump():
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
		#handle_sprint(input_dir)
	var direction: Vector3
	if player.is_on_floor():
		direction = (player.transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	else:
		direction = aerial_dir
	var speed = player.get_speed()
	if direction:
		
		player.velocity.x = direction.x * speed
		player.velocity.z = direction.z * speed
	else:
		player.velocity.x = move_toward(player.velocity.x, 0, speed)
		player.velocity.z = move_toward(player.velocity.z, 0, speed)
	player.move_and_slide()
	player.velocity.y = JUMP_VELOCITY
	aerial_dir = direction
	player.change_state(Walk.new(player))
	

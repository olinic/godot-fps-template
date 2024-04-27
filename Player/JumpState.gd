class_name Jump

const SPEED: float = 300.0
const JUMP_VELOCITY: float = 4.5

var player

var aerial_dir: Vector3 = Vector3.ZERO

func _init(player: Player, aerial_dir: Vector3) -> void:
	self.player = player
	player.velocity.y = JUMP_VELOCITY
	self.aerial_dir = aerial_dir
	print("Entered Jump State.")

func process(delta):
	handle_jump()
	
func handle_jump():
	var direction = aerial_dir
	var speed = player.get_speed()
	
	player.velocity.x = direction.x * speed
	player.velocity.z = direction.z * speed
	player.move_and_slide()
	
	if player.is_on_floor():
		player.change_state(Walk.new(player))
	

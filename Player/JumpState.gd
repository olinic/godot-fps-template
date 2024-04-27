class_name Jump

const SPEED: float = 300.0
const JUMP_VELOCITY: float = 4.5

var player: Player
var aerial_dir: Vector3 = Vector3.ZERO
var speed: float
var prev_state

func _init(player: Player, aerial_dir: Vector3, speed: float, prev_state) -> void:
	self.player = player
	self.aerial_dir = aerial_dir
	self.speed = speed
	self.prev_state = prev_state
	
	player.velocity.y = JUMP_VELOCITY
	print("Entered Jump State.")

func process(delta):
	player.move(aerial_dir, speed, delta)
	
	if player.is_on_floor():
		player.change_state(prev_state)

class_name Jump

const SPEED: float = 300.0
const JUMP_VELOCITY: float = 4.5

var player

var aerial_dir: Vector3 = Vector3.ZERO
var speed: float

func _init(player: Player, aerial_dir: Vector3, speed: float) -> void:
	self.player = player
	self.aerial_dir = aerial_dir
	self.speed = speed
	
	player.velocity.y = JUMP_VELOCITY
	print("Entered Jump State.")

func process(delta):
	var direction = aerial_dir
	
	player.move(direction, speed, delta)
	
	if player.is_on_floor():
		player.change_state(Walk.new(player))

	
	

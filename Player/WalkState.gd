class_name Walk

const SPEED: float = 300.0

var player: Player

func _init(player: Player) -> void:
	self.player = player
	print("Entered Walk State.")

func process(delta):
	move(delta)
	
func move(delta):
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
	var direction = (player.transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	handle_sprint(input_dir)
	handle_jump(direction)
	player.move(direction, SPEED, delta)

func handle_sprint(input_direction: Vector2):
	if ((Input.is_action_just_pressed("keyboard_sprint") or Input.is_action_just_pressed("controller_sprint")) 
			and player.is_on_floor() 
			and player.is_moving_forward(input_direction)):
		player.change_state(Sprint.new(player))

func handle_jump(direction: Vector3):
	if Input.is_action_just_pressed("jump") and player.is_on_floor():
		player.change_state(Jump.new(player, direction, SPEED))

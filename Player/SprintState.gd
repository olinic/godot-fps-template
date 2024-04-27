class_name Sprint

const SPRINT_SPEED: float = 600.0

var player: Player

func _init(player: Player) -> void:
	self.player = player
	print("Entered Sprint State.")

func process(delta):
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
	
	if (Input.is_action_just_released("keyboard_sprint") 
			or !player.is_moving_forward(input_dir)):
		player.change_state(Walk.new(player))
		
	var direction = (player.transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	player.move(direction, SPRINT_SPEED, delta)
	handle_jump(direction)
	
func handle_jump(direction):
	if Input.is_action_just_pressed("jump") and player.is_on_floor():
		player.change_state(Jump.new(player, direction, SPRINT_SPEED))
	


class_name Sprint
extends RefCounted

const SPRINT_SPEED: float = 600.0

var _player: Player

func _init(player: Player) -> void:
	self._player = player
	print("Entered Sprint State.")

func process(delta):
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
	
	if (Input.is_action_just_released("keyboard_sprint") 
			or !_player.is_moving_forward(input_dir)):
		_player.change_move_state(Walk.new(_player))
		
	var direction = (_player.transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	handle_jump(direction)
	_player.move(direction, SPRINT_SPEED, delta)
	
func handle_jump(direction):
	if Input.is_action_just_pressed("jump") and _player.is_on_floor():
		_player.change_move_state(Jump.new(_player, direction, SPRINT_SPEED, Sprint.new(_player)))
	


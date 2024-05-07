class_name Walk
extends RefCounted

const SPEED: float = 300.0

var _player: Player

func _init(player: Player) -> void:
	self._player = player
	print("Entered Walk State.")

func process(delta):
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
	var direction = (_player.transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	handle_sprint(input_dir)
	handle_jump(direction)
	_player.move(direction, SPEED, delta)
	
func handle_sprint(input_direction: Vector2):
	if ((Input.is_action_just_pressed("keyboard_sprint") or Input.is_action_just_pressed("controller_sprint")) 
			and _player.is_on_floor() 
			and _player.is_moving_forward(input_direction)):
		_player.change_move_state(Sprint.new(_player, _player.is_on_floor))

func handle_jump(direction: Vector3):
	if Input.is_action_just_pressed("jump") and _player.is_on_floor():
		_player.change_move_state(Jump.new(_player, direction, SPEED, Walk.new(_player)))
		

class_name Walk

const SPEED: float = 300.0

var _player: Player
var _next_state := Optional.empty()
var _player_is_on_floor: Callable
var _direction: Vector3


func _init(player: Player, player_is_on_floor: Callable) -> void:
	self._player = player
	self._player_is_on_floor = player_is_on_floor
	print("Entered Walk State.")

func process(delta):
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
	var direction = (_player.transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	handle_sprint(input_dir)
	handle_jump(direction)
	_player.move(direction, SPEED, delta)

func get_initial_velocity_change() -> Vector3:
	return Vector3.ZERO

func get_velocity(delta: float, input_dir: Vector2) -> Vector3:
	_direction = Vector3(input_dir.x, 0, input_dir.y).normalized()
	var velocity = Vector3.ZERO
	if _direction:
		velocity.x = _direction.x * SPEED * delta
		velocity.z = _direction.z * SPEED * delta
	return velocity
	
func get_next_state(input_dir: Vector2) -> Optional:
	if Input.is_action_just_pressed("jump"):
		_next_state = Optional.of(
				Jump.new(_player, _player_is_on_floor, _direction, SPEED, Walk.new(_player, _player_is_on_floor)))
	elif !_player_is_on_floor.call():
		_next_state = Optional.of(
				Fall.new(_player, _player_is_on_floor, _direction, SPEED, Walk.new(_player, _player_is_on_floor)))
	# TODO address the following issue: player stops keyboard sprinting mid-air, but continues sprinting when landing 
	elif (Input.is_action_just_pressed("keyboard_sprint") or Input.is_action_just_pressed("controller_sprint") 
			and _player_is_on_floor.call()
			and Sprint.is_moving_forward(input_dir)):
		_next_state = Optional.of(Sprint.new(_player, _player_is_on_floor))
	return _next_state
		
func handle_sprint(input_direction: Vector2):
	if ((Input.is_action_just_pressed("keyboard_sprint") or Input.is_action_just_pressed("controller_sprint")) 
			and _player.is_on_floor() 
			and Sprint.is_moving_forward(input_direction)):
		_player.change_move_state(Sprint.new(_player, _player.is_on_floor))

func handle_jump(direction: Vector3):
	if Input.is_action_just_pressed("jump") and _player.is_on_floor():
		_player.change_move_state(Jump.new(_player, _player.is_on_floor, direction, SPEED, Walk.new(_player, _player_is_on_floor)))
		

class_name Walk

const SPEED: float = 300.0

var _player: Player
var _next_state := Optional.empty()
var _player_is_on_floor: Callable
var _velocity: Vector3

func _init(player: Player, player_is_on_floor: Callable) -> void:
	self._player = player
	self._player_is_on_floor = player_is_on_floor

func get_name() -> String:
	return "Walk"

func get_initial_velocity_change() -> Vector3:
	return Vector3.ZERO

func get_velocity(delta: float, input_dir: Vector2) -> Vector3:
	var direction = Vector3(input_dir.x, 0, input_dir.y).normalized()
	_velocity = Vector3.ZERO
	if direction:
		_velocity.x = direction.x * SPEED * delta
		_velocity.z = direction.z * SPEED * delta
	_velocity = _player.transform.basis * _velocity
	return _velocity
	
func get_next_state(input_dir: Vector2) -> Optional:
	if Input.is_action_just_pressed("jump"):
		_next_state = Optional.of(
				Jump.new(_player, _player_is_on_floor, _velocity, SPEED, Walk.new(_player, _player_is_on_floor)))
	elif !_player_is_on_floor.call():
		_next_state = Optional.of(
				Fall.new(_player, _player_is_on_floor, _velocity, SPEED, Walk.new(_player, _player_is_on_floor)))
	# TODO address the following issue: player stops keyboard sprinting mid-air, but continues sprinting when landing 
	elif ((Input.is_action_just_pressed("keyboard_sprint") or Input.is_action_just_pressed("controller_sprint"))
			and Sprint.is_moving_forward(input_dir)):
		_next_state = Optional.of(Sprint.new(_player, _player_is_on_floor))
	return _next_state

class_name Jump

const SPEED: float = 300.0
const JUMP_VELOCITY: float = 4.5

var _player: Player
var _player_is_on_floor: Callable
var _aerial_dir: Vector3 = Vector3.ZERO
var _speed: float
var _target_state: Variant
var _next_state := Optional.empty()

func _init(player: Player, player_is_on_floor: Callable, aerial_dir: Vector3, speed: float, target_state) -> void:
	self._player = player
	self._player_is_on_floor = player_is_on_floor
	self._aerial_dir = aerial_dir
	self._speed = speed
	self._target_state = target_state
	
	player.velocity.y = JUMP_VELOCITY
	print("Entered Jump State.")

func get_initial_velocity_change() -> Vector3:
	return Vector3(0, JUMP_VELOCITY, 0)

func get_velocity(_delta: float, _input_dir: Vector2) -> Vector3:
	return _aerial_dir

func get_next_state() -> Optional:
	if _player_is_on_floor.call():
		_next_state = Optional.of(_target_state)
	return _next_state

func process(delta):
	_player.move(_aerial_dir, _speed, delta)
	
	if _player.is_on_floor():
		_player.change_move_state(_target_state)

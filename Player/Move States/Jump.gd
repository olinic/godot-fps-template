class_name Jump

const SPEED: float = 300.0

var _player: Player
var _player_is_on_floor: Callable
var _aerial_dir: Vector3 = Vector3.ZERO
var _speed: float
var _target_state: Variant
var _next_state := Optional.empty()
var _gravity = ProjectSettings.get_setting("physics/3d/default_gravity")
var _state_name = "Jump"

@export var jump_height: float = 1.0
@export var fall_multiplier: float = 2.0

func _init(player: Player, player_is_on_floor: Callable, aerial_dir: Vector3, speed: float, target_state) -> void:
	self._player = player
	self._player_is_on_floor = player_is_on_floor
	self._aerial_dir = aerial_dir + get_initial_velocity_change()
	self._speed = speed
	self._target_state = target_state

func get_name() -> String:
	return _state_name

func get_initial_velocity_change() -> Vector3:
	return Vector3(0, sqrt(jump_height * 2.0 * _gravity), 0)

func get_velocity(delta: float, _input_dir: Vector2) -> Vector3:
	if _aerial_dir.y >= 0:
		_aerial_dir.y -= _gravity * delta
	else:
		_aerial_dir.y -= _gravity * delta * fall_multiplier
	return _aerial_dir

func get_next_state(_input_dir: Vector2) -> Optional:
	if _player_is_on_floor.call():
		_next_state = Optional.of(_target_state)
	return _next_state

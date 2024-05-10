class_name Sprint

const SPRINT_LIMIT_ANGLE_MULTIPLIER: float = 0.22 # 0.25 = 45 degrees
const SPRINT_LIMIT_ANGLE_LEFT: float = -PI * SPRINT_LIMIT_ANGLE_MULTIPLIER
const SPRINT_LIMIT_ANGLE_RIGHT: float = -PI * (1 - SPRINT_LIMIT_ANGLE_MULTIPLIER)
const SPRINT_SPEED: float = 600.0

var _player: Player
var _player_is_on_floor: Callable
var _next_state := Optional.empty()
var _velocity: Vector3

static func is_moving_forward(input_direction: Vector2):
	var angle = input_direction.angle()
	return ((angle <= SPRINT_LIMIT_ANGLE_LEFT)
			and (angle >= SPRINT_LIMIT_ANGLE_RIGHT))

func _init(player: Player, player_is_on_floor: Callable) -> void:
	self._player = player
	self._player_is_on_floor = player_is_on_floor

func get_name() -> String:
	return "Sprint"

func get_initial_velocity_change() -> Vector3:
	return Vector3.ZERO

func get_velocity(delta: float, input_dir: Vector2) -> Vector3:
	var direction = Vector3(input_dir.x, 0, input_dir.y).normalized()
	_velocity = Vector3.ZERO
	if direction:
		_velocity.x = direction.x * SPRINT_SPEED * delta
		_velocity.z = direction.z * SPRINT_SPEED * delta
	_velocity = _player.transform.basis * _velocity
	return _velocity

func get_next_state(input_dir: Vector2) -> Optional:
	if Input.is_action_just_pressed("jump"):
		_next_state = Optional.of(
				Jump.new(_player, _player_is_on_floor, _velocity, SPRINT_SPEED, Sprint.new(_player, _player_is_on_floor)))
	elif !_player_is_on_floor.call():
		_next_state = Optional.of(
				Fall.new(_player, _player_is_on_floor, _velocity, SPRINT_SPEED, Sprint.new(_player, _player_is_on_floor)))
	# TODO address the following issue: player stops keyboard sprinting mid-air, but continues sprinting when landing 
	elif (Input.is_action_just_released("keyboard_sprint")
			or !Sprint.is_moving_forward(input_dir)):
		_next_state = Optional.of(Walk.new(_player, _player_is_on_floor))
	return _next_state

func process(delta):
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
	
	if (Input.is_action_just_released("keyboard_sprint") 
			or !Sprint.is_moving_forward(input_dir)):
		_player.change_move_state(Walk.new(_player, _player_is_on_floor))
		
	var direction = (_player.transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	handle_jump(direction)
	_player.move(direction, SPRINT_SPEED, delta)
	
func handle_jump(direction):
	if Input.is_action_just_pressed("jump"):
		_player.change_move_state(
				Jump.new(_player, _player_is_on_floor, direction, SPRINT_SPEED, Sprint.new(_player, func(): return)))
	elif !_player.is_on_floor():
		_player.change_move_state(
				Fall.new(_player, _player_is_on_floor, direction, SPRINT_SPEED, Sprint.new(_player, func(): return)))


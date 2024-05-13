class_name Sprint
extends Walk

const SPRINT_LIMIT_ANGLE_MULTIPLIER: float = 0.22 # 0.25 = 45 degrees
const SPRINT_LIMIT_ANGLE_LEFT: float = -PI * SPRINT_LIMIT_ANGLE_MULTIPLIER
const SPRINT_LIMIT_ANGLE_RIGHT: float = -PI * (1 - SPRINT_LIMIT_ANGLE_MULTIPLIER)

static func is_moving_forward(input_direction: Vector2):
	var angle = input_direction.angle()
	return ((angle <= SPRINT_LIMIT_ANGLE_LEFT)
			and (angle >= SPRINT_LIMIT_ANGLE_RIGHT))

func _init(player: Player, player_is_on_floor: Callable) -> void:
	super(player, player_is_on_floor)
	self._state_name = "Sprint"
	self._speed = 600

func get_next_state(input_dir: Vector2) -> Optional:
	if Input.is_action_just_pressed("jump"):
		_next_state = Optional.of(
				Jump.new(_player_is_on_floor, _velocity, self._speed, Sprint.new(_player, _player_is_on_floor)))
	elif !_player_is_on_floor.call():
		_next_state = Optional.of(
				Fall.new(_player_is_on_floor, _velocity, self._speed, Sprint.new(_player, _player_is_on_floor)))
	# TODO address the following issue: player stops keyboard sprinting mid-air, but continues sprinting when landing 
	elif (Input.is_action_just_released("keyboard_sprint")
			or !Sprint.is_moving_forward(input_dir)):
		_next_state = Optional.of(Walk.new(_player, _player_is_on_floor))
	return _next_state

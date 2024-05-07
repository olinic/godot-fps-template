class_name Sprint

const SPRINT_LIMIT_ANGLE_MULTIPLIER: float = 0.22 # 0.25 = 45 degrees
const SPRINT_LIMIT_ANGLE_LEFT: float = -PI * SPRINT_LIMIT_ANGLE_MULTIPLIER
const SPRINT_LIMIT_ANGLE_RIGHT: float = -PI * (1 - SPRINT_LIMIT_ANGLE_MULTIPLIER)
const SPRINT_SPEED: float = 600.0

var _player: Player
var _player_is_on_floor: Callable
var _next_state := Optional.empty()
var _direction: Vector3
var _input_dir: Vector2

static func is_moving_forward(input_direction: Vector2):
	var angle = input_direction.angle()
	return ((angle <= SPRINT_LIMIT_ANGLE_LEFT)
			and (angle >= SPRINT_LIMIT_ANGLE_RIGHT))

func _init(player: Player, player_is_on_floor: Callable) -> void:
	self._player = player
	self._player_is_on_floor = player_is_on_floor
	print("Entered Sprint State.")

func get_initial_velocity_change() -> Vector3:
	return Vector3.ZERO

func get_velocity(delta: float, input_dir: Vector2) -> Vector3:
	_input_dir = input_dir
	_direction = Vector3(input_dir.x, 0, input_dir.y).normalized()
	var velocity = Vector3.ZERO
	if _direction:
		velocity.x = _direction.x * SPRINT_SPEED * delta
		velocity.z = _direction.z * SPRINT_SPEED * delta
	return velocity

func get_next_state() -> Optional:
	if Input.is_action_just_pressed("jump") or !_player_is_on_floor.call(): # TODO avoid "jumping" when falling off edge 
		_next_state = Optional.of(
				Jump.new(_player, _direction, SPRINT_SPEED, Sprint.new(_player, _player.is_on_floor)))
	elif (Input.is_action_just_released("keyboard_sprint")
			or !Sprint.is_moving_forward(_input_dir)):
		_next_state = Optional.of(Walk.new(_player))
	return _next_state

func process(delta):
	var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
	
	if (Input.is_action_just_released("keyboard_sprint") 
			or !Sprint.is_moving_forward(input_dir)):
		_player.change_move_state(Walk.new(_player))
		
	var direction = (_player.transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	handle_jump(direction)
	_player.move(direction, SPRINT_SPEED, delta)
	
func handle_jump(direction):
	if Input.is_action_just_pressed("jump") or !_player.is_on_floor():
		_player.change_move_state(
				Jump.new(_player, direction, SPRINT_SPEED, Sprint.new(_player, func(): return)))
	


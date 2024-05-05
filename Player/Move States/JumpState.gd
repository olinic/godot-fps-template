class_name Jump
extends RefCounted

const SPEED: float = 300.0
const JUMP_VELOCITY: float = 4.5

var _player: Player
var _aerial_dir: Vector3 = Vector3.ZERO
var _speed: float
var _prev_state

func _init(player: Player, aerial_dir: Vector3, speed: float, prev_state) -> void:
	self._player = player
	self._aerial_dir = aerial_dir
	self._speed = speed
	self._prev_state = prev_state
	
	player.velocity.y = JUMP_VELOCITY
	print("Entered Jump State.")

func process(delta):
	_player.move(_aerial_dir, _speed, delta)
	
	if _player.is_on_floor():
		_player.change_move_state(_prev_state)

class_name Fall
extends Jump

func _init(player: Player, player_is_on_floor: Callable, aerial_dir: Vector3, speed: float, target_state) -> void:
	self._state_name = "Fall"
	super(player, player_is_on_floor, aerial_dir, speed, target_state)
	player.velocity.y = 0
	
func get_initial_velocity_change() -> Vector3:
	return Vector3(0, 0, 0)

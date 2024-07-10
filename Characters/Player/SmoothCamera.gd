extends Camera3D


@export var speed := 44.0

func _physics_process(delta: float) -> void:
	smooth_aiming(delta)
	keep_parent_position()

func smooth_aiming(delta:float) -> void:
	var weight = clampf(delta * speed, 0.0, 1.0)
	global_transform = global_transform.interpolate_with(
		get_parent().global_transform, weight
	)
	
func keep_parent_position() -> void:
	global_position = get_parent().global_position

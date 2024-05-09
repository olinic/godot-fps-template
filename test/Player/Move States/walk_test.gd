class_name WalkTest
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

var player
var walk_state: Walk

func before():
	player = auto_free(Player.new())

func before_test():
	walk_state = auto_free(Walk.new(player, func(): return true))

func test_sideways_is_moving_forward_false(sideways: Vector2, 
		test_parameters := [[Vector2.LEFT], [Vector2.RIGHT]]) -> void:
	assert_bool(Sprint.is_moving_forward(sideways)).is_false()

func test_forward_is_moving_forward_true(forward: Vector2, 
		test_parameters := [
			[Vector2.UP], 
			[Vector2.from_angle(-PI / 4)], 
			[Vector2.from_angle(-PI * 3 / 4)]]) -> void:
	assert_bool(Sprint.is_moving_forward(forward)).is_true()

func test_backward_is_moving_forward_false(backward: Vector2, 
		test_parameters := [
			[Vector2.DOWN], 
			[Vector2.from_angle(PI / 4)], 
			[Vector2.from_angle(PI * 3 / 4)]]) -> void:
	assert_bool(Sprint.is_moving_forward(backward)).is_false()

func test_get_initial_velocity_change():
	assert_vector(walk_state.get_initial_velocity_change()).is_equal(Vector3.ZERO)

func test_get_velocity():
	assert_vector(walk_state.get_velocity(1.0, Vector2(1, 0))).is_equal(Vector3(Walk.SPEED, 0, 0))
	assert_vector(walk_state.get_velocity(1.0, Vector2(0, 1))).is_equal(Vector3(0, 0, Walk.SPEED))

func test_normalized_get_velocity():
	assert_vector(walk_state.get_velocity(1.0, Vector2(1, 1))).is_less(Vector3(Walk.SPEED, 0, Walk.SPEED))

func test_get_next_state_keep_walking():
	assert_bool(walk_state.get_next_state(Vector2.UP).is_present()).is_false()

func test_get_next_state_to_jump_on_action():
	Input.action_press("jump")
	assert_object(walk_state.get_next_state(Vector2.UP).get_value()).is_instanceof(Jump)

func test_get_next_state_to_fall_on_walk_off_edge():
	var not_on_floor = func(): return false
	walk_state = auto_free(Walk.new(player, not_on_floor))
	assert_object(walk_state.get_next_state(Vector2.UP).get_value()).is_instanceof(Fall)

func test_controller_get_next_state_to_sprint_on_is_moving_forward():
	Input.action_press("controller_sprint")
	assert_object(walk_state.get_next_state(Vector2.UP).get_value()).is_instanceof(Sprint)

func test_keyboard_get_next_state_to_sprint_on_is_moving_forward():
	Input.action_press("keyboard_sprint")
	assert_object(walk_state.get_next_state(Vector2.UP).get_value()).is_instanceof(Sprint)

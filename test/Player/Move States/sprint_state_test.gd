class_name SprintTest
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

var player
var sprint_state: Sprint

func before():
	player = auto_free(Player.new())

func before_test():
	sprint_state = auto_free(Sprint.new(player, func(): return true))

func test_get_velocity():
	assert_vector(sprint_state.get_velocity(1.0, Vector2(1, 0))).is_equal(Vector3(600, 0, 0))
	assert_vector(sprint_state.get_velocity(1.0, Vector2(0, 1))).is_equal(Vector3(0, 0, 600))

func test_normalized_get_velocity():
	assert_vector(sprint_state.get_velocity(1.0, Vector2(1, 1))).is_less(Vector3(600, 0, 600))

func test_get_next_state_keep_sprinting():
	sprint_state.get_velocity(1.0, Vector2.UP)
	assert_bool(sprint_state.get_next_state().is_present()).is_false()

func test_get_next_state_to_jump_on_action():
	Input.action_press("jump")
	assert_bool(sprint_state.get_next_state().is_present()).is_true()
	assert_object(sprint_state.get_next_state().get_value()).is_instanceof(Jump)

func test_get_next_state_to_jump_on_run_off_edge():
	var not_on_floor = func(): return false
	sprint_state = auto_free(Sprint.new(player, not_on_floor))
	assert_bool(sprint_state.get_next_state().is_present()).is_true()
	assert_object(sprint_state.get_next_state().get_value()).is_instanceof(Jump)

func test_get_next_state_to_walk_on_keyboard_release():
	Input.action_release("keyboard_sprint")
	assert_bool(sprint_state.get_next_state().is_present()).is_true()
	assert_object(sprint_state.get_next_state().get_value()).is_instanceof(Walk)

func test_get_next_state_to_walk_on_not_moving_forward(input_dir: Vector2, 
		test_parameters := [
			[Vector2.DOWN],
			[Vector2.ZERO]
		]):
	sprint_state.get_velocity(1.0, input_dir)
	assert_bool(sprint_state.get_next_state().is_present()).is_true()
	assert_object(sprint_state.get_next_state().get_value()).is_instanceof(Walk)
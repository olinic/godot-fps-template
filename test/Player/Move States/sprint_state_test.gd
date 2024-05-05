class_name SprintTest
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

var player
var sprint_state: Sprint

func before():
	player = auto_free(Player.new())

func before_test():
	sprint_state = auto_free(Sprint.new(player))

func test_get_velocity():
	assert_vector(sprint_state.get_velocity(1.0, Vector2(1, 0))).is_equal(Vector3(600, 0, 0))
	assert_vector(sprint_state.get_velocity(1.0, Vector2(0, 1))).is_equal(Vector3(0, 0, 600))

func test_normalized_get_velocity():
	assert_vector(sprint_state.get_velocity(1.0, Vector2(1, 1))).is_less(Vector3(600, 0, 600))
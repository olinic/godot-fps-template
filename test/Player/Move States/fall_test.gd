class_name FallTest
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

var player
var fall_state: Fall
var curr_velocity := Vector3(1, 1, 1)

func before():
	player = auto_free(Player.new())

func before_test():
	fall_state = auto_free(Fall.new(func(): return false, curr_velocity, 1.0, Walk.new(player, player.is_on_floor)))

func test_get_initial_velocity_change():
	assert_vector(fall_state.get_initial_velocity_change()).is_equal(Vector3(0, 0, 0))
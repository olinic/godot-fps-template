class_name JumpTest
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

var player
var jump_state: Jump

func before():
	player = auto_free(Player.new())

func before_test():
	jump_state = auto_free(Jump.new(player, Vector3.ZERO, 1.0, Walk.new(player)))

func test_get_initial_velocity_change():
	assert_vector(jump_state.get_initial_velocity_change()).is_equal(Vector3(0, Jump.JUMP_VELOCITY, 0))
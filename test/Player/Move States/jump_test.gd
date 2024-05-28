class_name JumpTest
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

var player
var jump_state: Jump
var curr_velocity := Vector3(1, 1, 1)

func before():
	player = auto_free(Player.new())

func before_test():
	jump_state = auto_free(Jump.new(func(): return false, curr_velocity, 1.0, Walk.new(player, player.is_on_floor)))

func test_get_initial_velocity_change():
	assert_vector(jump_state.get_initial_velocity_change())\
			.is_equal(Vector3(0, sqrt(jump_state.jump_height * 2.0 * jump_state._gravity), 0))

func test_get_velocity():
	var velocity = jump_state.get_velocity(1.0, Vector2(0.5, 0.5))
	assert_float(velocity.x).is_equal(1)
	assert_float(velocity.y).is_less(0) # falling
	assert_float(velocity.z).is_equal(1)

func test_get_next_state_stay_in_jump():
	assert_bool(jump_state.get_next_state(Vector2.UP).is_present()).is_false()

func test_get_next_state_transition_on_landing():
	var landed := func(): return true
	jump_state = auto_free(Jump.new(player, landed, curr_velocity, 1.0, Walk.new(player, player.is_on_floor)))
	assert_bool(jump_state.get_next_state(Vector2.UP).is_present()).is_true()
	assert_object(jump_state.get_next_state(Vector2.UP).get_value()).is_instanceof(Walk)
# GdUnit generated TestSuite
class_name PlayerTest
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

var player
var camera_controller

func before():
	camera_controller = mock(Node3D)

func before_test():
	player = auto_free(Player.new())
	player.camera_controller = camera_controller
	
func test_init_get_move_state():
	assert_object(player.get_move_state()).is_not_null().is_instanceof(Walk)
		
func test_change_state():
	player.change_move_state(Sprint.new(player, player.is_on_floor))
	assert_object(player.get_move_state()).is_not_null().is_instanceof(Sprint)
	
	
func test_process():
	var player_spy = spy(Sprint.new(player, player.is_on_floor))
	player.change_move_state(player_spy)
	player._physics_process(1.0)
	verify(player_spy, 1).process(1.0)

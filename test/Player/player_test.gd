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
	
	
func test_sideways_is_moving_forward_false(sideways: Vector2, test_parameters := [[Vector2.LEFT], [Vector2.RIGHT]]) -> void:
	assert_bool(player.is_moving_forward(sideways)).is_false()

func test_forward_is_moving_forward_true(forward: Vector2, 
		test_parameters := [
			[Vector2.UP], 
			[Vector2.from_angle(-PI / 4)], 
			[Vector2.from_angle(-PI * 3 / 4)]]) -> void:
	assert_bool(player.is_moving_forward(forward)).is_true()

func test_backward_is_moving_forward_false(backward: Vector2, 
		test_parameters := [
			[Vector2.DOWN], 
			[Vector2.from_angle(PI / 4)], 
			[Vector2.from_angle(PI * 3 / 4)]]) -> void:
	assert_bool(player.is_moving_forward(backward)).is_false()
	
func test_init_get_move_state():
	assert_object(player.get_move_state()).is_not_null().is_instanceof(Walk)
		
		
func test_change_state():
	player.change_move_state(Sprint.new(player))
	assert_object(player.get_move_state()).is_not_null().is_instanceof(Sprint)
	
	
func test_process():
	var spy = spy(Sprint.new(player))
	player.change_move_state(spy)
	player._physics_process(1.0)
	verify(spy, 1).process(1.0)

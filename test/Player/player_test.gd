# GdUnit generated TestSuite
class_name PlayerTest
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

var player
func before_test():
	player = auto_free(Player.new())

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

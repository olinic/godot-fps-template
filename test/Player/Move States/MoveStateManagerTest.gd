# GdUnit generated TestSuite
class_name MoveStateManagerTest
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

# TestSuite generated from
const __source = 'res://Player/Move States/MoveStateManager.gd'

var move_state_manager: MoveStateManager

var player: Player

func before_test():
	player = auto_free(Player.new())
	move_state_manager = auto_free(MoveStateManager.new(player))
	
func test_init_get_state():
	assert_object(move_state_manager.get_state()).is_not_null().is_instanceof(Walk)
		
func test_set_state():
	move_state_manager.set_state(Sprint.new(player))
	assert_object(move_state_manager.get_state()).is_not_null().is_instanceof(Sprint)
	
func test_process():
	var spy = spy(Sprint.new(player))
	move_state_manager.set_state(spy)
	move_state_manager.process(1.0)
	verify(spy, 1).process(1.0)
	

class_name MoveStateManager

extends RefCounted

var player: Player
var state 

func _init(player: Player) -> void:
	self.player = player
	self.state = Walk.new(player)

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


func process(delta: float) -> void:
	state.process(delta)

func set_state(state):
	self.state = state

func get_state():
	return self.state
	

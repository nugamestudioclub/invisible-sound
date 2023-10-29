tool
extends State

var monster : Monster

var target_point : Vector2
var direction_to_point : Vector2
var dist_to_point : float

var distance_moved : float

#
# FUNCTIONS TO INHERIT IN YOUR STATES
#

# This function is called when the state enters
# XSM enters the root first, the the children
func _on_enter(_args) -> void:
	monster = target as Monster
	target_point = monster.target.position
	direction_to_point = monster.position.direction_to(target_point)
	dist_to_point = monster.position.distance_to(target_point)
	distance_moved = 0


# This function is called just after the state enters
# XSM after_enters the children first, then the parent
func _after_enter(_args) -> void:
	pass


# This function is called each frame if the state is ACTIVE
# XSM updates the root first, then the children
func _on_update(_delta: float) -> void:
	var velocity = monster.speed
	
	var cur_dist = monster.position.distance_to(target_point)
	
	var old_pos = monster.position
	
	monster.move_and_slide(min(velocity, cur_dist / _delta) 
		* direction_to_point)
	
	distance_moved += monster.position.distance_to(old_pos)
	
	if distance_moved >= dist_to_point:
		change_state("ChargeDecay", { "direction" : direction_to_point})
	
	print(distance_moved)


# This function is called each frame after all the update calls
# XSM after_updates the children first, then the root
func _after_update(_delta: float) -> void:
	pass


# This function is called before the State exits
# XSM before_exits the root first, then the children
func _before_exit(_args) -> void:
	pass


# This function is called when the State exits
# XSM before_exits the children first, then the root
func _on_exit(_args) -> void:
	pass


# when StateAutomaticTimer timeout()
func _state_timeout() -> void:
	pass


# Called when any other Timer times out
func _on_timeout(_name) -> void:
	pass

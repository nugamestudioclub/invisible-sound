tool
extends State

var monster : Monster

var path : Path2D 
var path_follow : PathFollow2D

var path_point

#
# FUNCTIONS TO INHERIT IN YOUR STATES
#

# This function is called when the state enters
# XSM enters the root first, the the children
func _on_enter(_args) -> void:
	monster = target as Monster
	path_follow = monster.path_follow_node
	path = path_follow.get_parent()
	path_point = path.curve.get_closest_point(monster.position)
	pass

# This function is called just after the state enters
# XSM after_enters the children first, then the parent
func _after_enter(_args) -> void:
	pass


# This function is called each frame if the state is ACTIVE
# XSM updates the root first, then the children
func _on_update(_delta: float) -> void:
	var velocity = monster.speed
	
	var dist_to_point = monster.position.distance_to(path_point)
	
	monster.move_and_slide(min(velocity, dist_to_point / _delta) * monster.position.direction_to(path_point))
	if monster.position.is_equal_approx(path_point):
		change_state("MoveOnPath")



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
	path_follow.offset = path.curve.get_closest_offset(path_point)


# when StateAutomaticTimer timeout()
func _state_timeout() -> void:
	pass


# Called when any other Timer times out
func _on_timeout(_name) -> void:
	pass

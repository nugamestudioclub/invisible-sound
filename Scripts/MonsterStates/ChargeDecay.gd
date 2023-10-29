tool
extends State

var monster : Monster

var charge_direction : Vector2
var dist_traveled : float

#
# FUNCTIONS TO INHERIT IN YOUR STATES
#

# This function is called when the state enters
# XSM enters the root first, the the children
func _on_enter(_args : Dictionary) -> void:
	monster = target as Monster
	dist_traveled = 0
	if _args.has("direction"):
		charge_direction = _args["direction"]


# This function is called just after the state enters
# XSM after_enters the children first, then the parent
func _after_enter(_args) -> void:
	pass


# This function is called each frame if the state is ACTIVE
# XSM updates the root first, then the children
func _on_update(_delta: float) -> void:
	print(charge_direction)
	if dist_traveled >= monster.overcharge_dist:
		change_state("ChargeCooldown")
	else:
		var velocity = monster.speed
		
		monster.move_and_slide(velocity * charge_direction)
		
		dist_traveled += velocity * _delta
	
	pass


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

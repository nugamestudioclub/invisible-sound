extends Node

signal change_monster_state(state_name, args)

onready var timer : Timer = $AlarmTimer

var pos_node : Position2D

func _on_activate_alarm(pos : Vector2):
	pos_node = Position2D.new()
	pos_node.position = pos
	emit_signal("change_monster_state", "FollowNode", { "Node" : pos_node })
	timer.start()


func _on_AlarmTimer_timeout():
	emit_signal("change_monster_state", "MoveOnPath", { })

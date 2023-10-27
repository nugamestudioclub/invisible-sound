extends Area2D

signal activate_alarm(pos)

func _ready():
	add_to_group("AlarmTile")
	connect("body_entered", self, "_on_body_entered")
	

func _on_body_entered(body : Node):
	if body.is_in_group("Player") and body is Node2D:
		emit_signal("activate_alarm", body.position)

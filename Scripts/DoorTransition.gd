extends Area2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

export var teleport_to : NodePath

onready var teleport_position : Node2D = get_node(teleport_to)


# Called when the node enters the scene tree for the first time.
func _ready():
	connect("body_entered", self, "_on_body_entered")

func _on_body_entered(body : Node):
	if body.is_in_group("Player") and body is Node2D and teleport_position:
		body.global_position = teleport_position.global_position

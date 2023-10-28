extends Area2D

signal teleport_player(pos)
signal teleport_monster(pos)

export var teleport_player_to : NodePath
export var teleport_monster_to : NodePath

onready var player_pos : Node2D = get_node_or_null(teleport_player_to)
onready var monster_pos : Node2D = get_node_or_null(teleport_monster_to)


# Called when the node enters the scene tree for the first time.
func _ready():
	add_to_group("Teleporter")
	connect("body_entered", self, "_on_body_entered")

func _on_body_entered(body : Node):
	if body.is_in_group("Player") and body is Node2D:
		if player_pos:
			emit_signal("teleport_player", player_pos.position)
		if monster_pos:
			emit_signal("teleport_monster", monster_pos.position)

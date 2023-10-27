extends Node

var stream_player : PackedScene = preload("res://Scenes/VisualAudioStreamPlayer.tscn")

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

export var size : int = 5

func _enter_tree():
	for i in size:
		add_child(stream_player.instance())

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

func _emit(pos : Vector2):
	var next_stream = _first_free()
	if next_stream:
		next_stream.global_position = pos
		next_stream._emit(pos)

func _first_free() -> Node2D:
	for node in get_children():
		if node.free:
			return node
	return null

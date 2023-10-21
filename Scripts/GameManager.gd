extends Node

onready var player : Node = $Player
onready var audio_visualizer : Node = $AudioVisualizerManager

# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	for avstream in get_tree().get_nodes_in_group("VisualAudioStreamPlayer"):
		audio_visualizer.connect("enabled", avstream, "_on_visualizer_enabled")
		audio_visualizer.connect("disabled", avstream, "_on_visualizer_disabled")
	player.connect("activate_visualizer", audio_visualizer, "activate")


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

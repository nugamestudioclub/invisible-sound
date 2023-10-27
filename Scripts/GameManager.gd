extends Node

onready var player : Node = $Player
onready var audio_visualizer : Node = $AudioVisualizerManager
onready var alarm_manager : Node = $AlarmManager

# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	for avstream in get_tree().get_nodes_in_group("VisualAudioStreamPlayer"):
		audio_visualizer.connect("enabled", avstream, "_on_visualizer_enabled")
		audio_visualizer.connect("disabled", avstream, "_on_visualizer_disabled")
	
	for interactable in get_tree().get_nodes_in_group("Interactable"):
		interactable.connect("in_range_of", player, "_on_interactable_in_range_of")
		interactable.connect("out_of_range_of", player, "_on_interactable_out_of_range_of")
		player.connect("interacting_with", interactable, "_on_player_interact")
	
	for alarm_tile in get_tree().get_nodes_in_group("AlarmTile"):
		alarm_tile.connect("activate_alarm", alarm_manager, "_on_activate_alarm")
	
	player.connect("activate_visualizer", audio_visualizer, "activate")
	
	connect_monster($Monster)
	
	$Monster.target = $Player


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func connect_monster(monster : Node):
	alarm_manager.connect("change_monster_state", monster, "change_state")

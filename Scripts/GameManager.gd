extends Node

onready var player : Node = get_tree().get_nodes_in_group("Player")[0]
onready var monster : Node = get_tree().get_nodes_in_group("Monster")[0]
onready var audio_visualizer : Node = get_tree().get_nodes_in_group("AudioVisualizerManager")[0]
onready var alarm_manager : Node = get_tree().get_nodes_in_group("AlarmManager")[0]

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
	
	for teleporter in get_tree().get_nodes_in_group("Teleporter"):
		teleporter.connect("teleport_player", player, "set_position")
		teleporter.connect("teleport_monster", monster, "set_position")

	player.connect("activate_visualizer", audio_visualizer, "activate")
	
	connect_monster(monster)
	
	monster.target = player


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func connect_monster(monster : Node):
	alarm_manager.connect("change_monster_state", monster, "change_monster_state")

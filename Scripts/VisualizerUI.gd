tool
extends MarginContainer

var battery_rect : PackedScene = preload("res://Scenes/UI/BatteryRect.tscn")

onready var resource_container : Control = $HBoxContainer/VisualizerResourceContainer
onready var battery_container : Control = $HBoxContainer/VisualizerResourceContainer/BatteryContainer
onready var consumption_progress : ProgressBar = $HBoxContainer/VisualizerResourceContainer/ConsumptionProgress

export(int, 1, 20) var max_battery : int
export var current_battery : int

var cur_timer_length : float
var max_timer_length : float

export var current_timer_segments : int = 0

func _ready():
	_update_size()
	_update_data()

func _process(delta):
	if Engine.is_editor_hint():
		resource_container = $HBoxContainer/VisualizerResourceContainer
		battery_container = $HBoxContainer/VisualizerResourceContainer/BatteryContainer
		consumption_progress = $HBoxContainer/VisualizerResourceContainer/ConsumptionProgress
		
		_update_size()
		_update_data()


func _update_data():
	
	
	battery_container.margin_right = current_battery * segment_length()
	consumption_progress.margin_left = current_battery * segment_length()
	consumption_progress.visible = current_timer_segments > 0
	consumption_progress.margin_right = (current_battery + current_timer_segments) * segment_length()
	
	if battery_container.get_child_count() != current_battery:
		for c in battery_container.get_children():
			c.queue_free()
		
		for i in current_battery:
			var battery_node = battery_rect.instance()
			battery_node.margin_bottom = resource_container.margin_bottom
			battery_node.margin_right = current_battery * segment_length()
			battery_container.add_child(battery_node)
	
	consumption_progress.max_value = max_timer_length
	consumption_progress.value = cur_timer_length

func segment_length() -> float:
	return (resource_container.margin_right - resource_container.margin_left) / max_battery

func _update_size():
	consumption_progress.margin_bottom = resource_container.margin_bottom

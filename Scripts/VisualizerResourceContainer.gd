tool
extends HBoxContainer

onready var battery_rect = preload("res://Scenes/UI/BatteryRect.tscn")

onready var battery_container : Control = $BatteryContainer
onready var consumption_progress : Control = $ConsumptionProgress

export var max_battery : int
export var current_battery : int

var current_timer_segments : int = 0

func _ready():
	_update_size()

func _process(delta):
	if Engine.is_editor_hint():
		_update_size()
		_update_data()

func _update_data():
	battery_container.margin_right = self.margin_right * current_battery / max_battery
	consumption_progress.margin_left = self.margin_right * current_battery / max_battery
	consumption_progress.visible = current_timer_segments > 0
	consumption_progress.margin_right = self.margin_right * current_timer_segments / max_battery

func _update_size():
	consumption_progress.margin_bottom = self.margin_bottom

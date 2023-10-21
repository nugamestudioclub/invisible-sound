extends Node

class_name AudioVisualizerManager

onready var timer = $Timer

var enabled : bool = false setget _set_enabled

signal enabled()
signal disabled()

export var duration : float

func _set_enabled(new_val : bool):
	enabled = new_val
	if enabled:
		emit_signal("enabled")
	else:
		emit_signal("disabled")

func activate():
	_set_enabled(true)
	timer.start(timer.time_left + duration)

func _deactivate():
	_set_enabled(false)


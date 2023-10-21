extends Node


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
enum FlagName {
	INFINITE_VISUALIZER
}

export var batteries : int = 5
export var flags := {
	FlagName.INFINITE_VISUALIZER: true
}



func add_battery(_to_add := 1):
	batteries += _to_add

func use_battery(_to_use := 1) -> bool:
	if (_to_use > batteries):
		return false
	batteries -= _to_use
	return true

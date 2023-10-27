extends AudioStreamPlayer2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
onready var particles := $CPUParticles2D

export var force : float = 1.0

var free : bool = true

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.



# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _on_visualizer_enabled():
	print("enabled")

func _on_visualizer_disabled():
	print("disabled")

func emit():
	pass

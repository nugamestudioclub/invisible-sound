extends KinematicBody2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

signal activate_visualizer()



var speed : float = 100

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	
	if Input.is_action_just_pressed("ui_accept"):
		if (ResourceManager.flags[ResourceManager.FlagName.INFINITE_VISUALIZER] 
		or ResourceManager.use_battery()):
			emit_signal("activate_visualizer") 
	
	
	var move_input = Vector2(Input.get_axis("ui_left", "ui_right"), Input.get_axis("ui_up", "ui_down"))
	var velocity = move_input.normalized() * speed
	move_and_slide(velocity)

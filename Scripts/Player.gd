extends KinematicBody2D

class_name Player

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

signal activate_visualizer()

signal interacting_with(player, interacting)

export var speed : float = 100

var interact_field := [ ]



# Called when the node enters the scene tree for the first time.
func _ready():
	pass


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	
	if Input.is_action_just_pressed("ui_accept"):
		if (ResourceManager.flags[ResourceManager.FlagName.INFINITE_VISUALIZER] 
		or ResourceManager.use_battery()):
			emit_signal("activate_visualizer") 
	
	if Input.is_action_just_pressed("ui_accept"):
		if interact_field.size() > 0:
			emit_signal("interacting_with", self, interact_field[0])
	
	var move_input = Vector2(Input.get_axis("ui_left", "ui_right"), Input.get_axis("ui_up", "ui_down"))
	var velocity = move_input.normalized() * speed
	move_and_slide(velocity)
	
	

func _on_interactable_in_range_of(interacting : Node):
	interact_field.append(interacting)
	interact_field.sort_custom(self, "_sort_interactables")

func _on_interactable_out_of_range_of(interacting : Node):
	interact_field.erase(interacting)

func _sort_interactables(a, b):
	return a.interact_priority > b.interact_priority

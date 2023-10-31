extends KinematicBody2D

class_name Player

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

signal activate_visualizer()

signal interacting_with(player, interacting)

signal area_collision(player, area)

signal footstep()

onready var anim_player : AnimationPlayer = $AnimationPlayer

export var speed : float = 100

var direction : Vector2 = Vector2(0, -1)

var interact_field := [ ]



# Called when the node enters the scene tree for the first time.
func _ready():
	anim_player.play("idle down")
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


func _physics_process(delta):
	var move_input = Vector2(Input.get_axis("ui_left", "ui_right"), Input.get_axis("ui_up", "ui_down"))
	var velocity = move_input.normalized() * speed
	
	move_and_slide(velocity)
	
	if move_input.x:
		direction = Vector2(move_input.x, 0)
	elif move_input.y:
		direction = Vector2(0, move_input.y)
	
	if move_input == Vector2(0, 0):
		match direction:
			Vector2(0, -1):
				anim_player.play("idle up")
			Vector2(0, 1):
				anim_player.play("idle down")
			Vector2(-1, 0):
				anim_player.play("idle left")
			Vector2(1, 0):
				anim_player.play("idle right")
	else:
		match direction:
			Vector2(0, -1):
				anim_player.play("walk up")
			Vector2(0, 1):
				anim_player.play("walk down")
			Vector2(-1, 0):
				anim_player.play("walk left")
			Vector2(1, 0):
				anim_player.play("walk right")
		# emit_signal("footstep")
	

func _on_interactable_in_range_of(interacting : Interactable):
	interact_field.append(interacting)
	interact_field.sort_custom(self, "_sort_interactables")

func _on_interactable_out_of_range_of(interacting : Interactable):
	interact_field.erase(interacting)

func _sort_interactables(a, b):
	return a.interact_priority > b.interact_priority

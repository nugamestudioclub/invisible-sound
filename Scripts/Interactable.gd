extends Area2D

class_name Interactable

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

signal in_range_of(interacting)
signal out_of_range_of(interacting)

export var interact_priority : int

# Called when the node enters the scene tree for the first time.
func _ready():
	add_to_group("Interactable")
	self.connect("body_entered", self, "_on_body_entered")
	self.connect("body_exited", self, "_on_body_exited")

func _interact(_player : Player):
	pass

func _on_body_entered(body : Node):
	if body.is_in_group("Player"):
		emit_signal("in_range_of", self)

func _on_body_exited(body : Node):
	if body.is_in_group("Player"):
		emit_signal("out_of_range_of", self)

func _on_player_interact(player : Player, interacting : Interactable):
	if self == interacting:
		_interact(player)

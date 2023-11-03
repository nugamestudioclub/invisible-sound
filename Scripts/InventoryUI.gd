tool
extends Control

onready var gas_container = $HBoxContainer/GasContainer
onready var card_container = $HBoxContainer/CardContainer

export var has_gas : bool = true
export var has_card : bool = false

func _process(delta):
	if Engine.is_editor_hint():
		gas_container.visible = has_gas
		card_container.visible = has_card

tool
extends TextureProgress

onready var square_texture = $TextureRect

func _process(delta):
	if Engine.is_editor_hint():
		square_texture = $TextureRect
	
	if value == min_value:
		square_texture.visible = false
	else:
		square_texture.visible = true
	print(square_texture.visible)

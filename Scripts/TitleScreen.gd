extends Control


func _process(delta):
	if Input.is_action_just_pressed("ui_accept"):
		get_tree().change_scene_to(load("res://Scenes/TestScenes/JoshGeneralTesting.tscn"))

extends Area2D

onready var audio_player : AudioStreamPlayer2D = $AudioStreamPlayer2D

export(float, -80, 24) var base_volume_dB := 0.0
export var volume_curve : Curve
export var max_range : int

var player : Node2D

func _process(delta):
	if player:
		var dist = position.distance_to(player.position)
		if dist > max_range:
			audio_player.stop()
		else:
			audio_player.play()
			audio_player.volume_db = volume_curve.interpolate_baked(dist/max_range)

func _on_body_entered(body : Node):
	if body.is_in_group("Player"):
		player = body

func _on_body_exited(body : Node):
	if body.is_in_group("Player"):
		player = null

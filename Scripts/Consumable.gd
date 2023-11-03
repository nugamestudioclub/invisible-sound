extends Interactable


func _interact(_player : PhysicsBody2D):
	print("consuming...")
	emit_signal("activate")
	_consume()
	queue_free()

func _consume():
	pass


extends Interactable


func _interact(_player : PhysicsBody2D):
	_consume()
	queue_free()

func _consume():
	pass


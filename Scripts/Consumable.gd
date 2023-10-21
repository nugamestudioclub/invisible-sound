extends Interactable


func _interact(_player : Player):
	_consume()
	queue_free()

func _consume():
	pass


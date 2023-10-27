extends KinematicBody2D

class_name Monster

enum MonsterState {
	PATH, FOLLOW
}

export (MonsterState) var default_state

onready var state_machine = $StateMachine

export var path_follow : NodePath
onready var path_follow_node : PathFollow2D = get_node(path_follow) as PathFollow2D

export var speed : int

var target : Node2D

# Called when the node enters the scene tree for the first time.
func _ready():
	# state_machine.change_state(_get_state(default_state))
	pass

func change_monster_state(state_name : String, _args : Dictionary):
	match(state_name):
		"MoveOnPath":
			pass
		"FollowNode":
			pass

func _get_state(state : int) -> String:
	var state_name : String
	match(state):
		MonsterState.PATH:
			state_name = "MoveOnPath"
		MonsterState.FOLLOW:
			state_name = "FollowNode"
	return state_name

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

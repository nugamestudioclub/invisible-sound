using Godot;

public class MonsterEntity : SceneServiceNode {
	private KinematicBody2D _kinematicBody;

	public override System.Numerics.Vector3 ScenePosition =>
		new System.Numerics.Vector3(
			_kinematicBody.GlobalPosition.x,
			_kinematicBody.GlobalPosition.y,
			0
		);

	public override System.Numerics.Vector3 Anchor =>
		new System.Numerics.Vector3(
			_kinematicBody.Position.x,
			_kinematicBody.Position.y,
			0
		);

	public override void _Ready() {
		_kinematicBody = GetNode<KinematicBody2D>("Monster");
	}


	public override void Start() {
		base.Start();
		Entity.Services.AudioService.PlayLooping("res://Audio/Monster/Creature.wav", (int)AudioTrack.Danger1, this);
		Entity.Services.AudioService.PlayLooping("res://Audio/Monster/Creature2.wav", (int)AudioTrack.Danger2, this);
	}
}

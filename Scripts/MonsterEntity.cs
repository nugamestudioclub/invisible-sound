using Godot;

public class MonsterEntity : SceneServiceNode {
	private KinematicBody2D _kinematicBody;
	private CanvasItem visualizerParticle;

	private AudioPlayerNode _track1;
	private AudioPlayerNode _track2;

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

	public bool Vizualized
	{
		get => visualizerParticle.Visible;
		set => visualizerParticle.Visible = value;

    }

	public override void _Ready() {
		_kinematicBody = GetNode<KinematicBody2D>("Monster");
        visualizerParticle= GetNode<CanvasItem>("Monster/VisualizerParticles");
		Vizualized = false;
    }


	public override void Start() {
		base.Start();
		_track1 = (AudioPlayerNode)Entity.Services.AudioService.PlayLooping("res://Audio/Monster/Creature.wav", (int)AudioTrack.Danger1, this);
		_track2 = (AudioPlayerNode)Entity.Services.AudioService.PlayLooping("res://Audio/Monster/Creature2.wav", (int)AudioTrack.Danger2, this);
	}

	public override void _PhysicsProcess(float delta) {
		base._PhysicsProcess(delta);
		if( _track1 != null  )
			_track1.GlobalPosition = _kinematicBody.GlobalPosition;
		if( _track2 != null )
			_track2.GlobalPosition = _kinematicBody.GlobalPosition;
	}
}

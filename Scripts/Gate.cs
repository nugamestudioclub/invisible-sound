using Godot;

public class Gate : Node2D {
	[Export]
	private PlayerEntity _player;

	[Export]
	private float _radius = 2.5f;

	public override void _PhysicsProcess(float delta) {
		bool hasKey =_player.Entity.Game.GlobalData.GetValueOrDefault("hasKey", false);
		float distance = GlobalPosition.DistanceTo(_player.GlobalPosition);
		if( hasKey && distance <=_radius ) {
			var scene = GD.Load<PackedScene>("res://Scenes/Menus/Credits.tscn");
			GetTree().ChangeSceneTo(scene);
		}
	}
}

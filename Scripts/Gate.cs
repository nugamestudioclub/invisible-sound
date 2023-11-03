using Godot;

public class Gate : Node2D {
	[Export]
	private float _radius = 2.5f;

	private PlayerEntity _player;

	private Game _game;

	public override void _PhysicsProcess(float delta) {
		if( _player == null ) {
			var entities = GetTree().GetNodesInGroup("Player");
			foreach( var entity in entities )
				if( entity is PlayerEntity playerEntity )
					_player = playerEntity;
		}
		else {
			bool hasKey =_player.Entity.Game.GlobalData.GetValueOrDefault("hasKey", false);
			float distance = GlobalPosition.DistanceTo(_player.GlobalPosition);
			if( hasKey && distance <=_radius ) {
				var scene = GD.Load<PackedScene>("res://Scenes/Menus/Credits.tscn");
				GetTree().ChangeSceneTo(scene);
			}
		}
	}
}

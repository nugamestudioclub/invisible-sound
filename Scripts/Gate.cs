using Godot;

public class Gate : Node2D {
	[Export]
	private float _radius = 65f;

	private PlayerEntity _player;

	public override void _PhysicsProcess(float delta) {
		if( _player == null ) {
			var entities = GetTree().GetNodesInGroup("entity");
			foreach( var entity in entities ) {
				if( entity is PlayerEntity player )
					_player = player;
			}
		}
		else if( _player.Entity != null ){
			bool hasKey =_player.Entity.Game.GlobalData.GetValueOrDefault("hasKey", false);
			var playerPosition = new Vector2(
				_player.ScenePosition.X,
				_player.ScenePosition.Y
			);
			float distance = GlobalPosition.DistanceTo(playerPosition);
			// GD.Print($"hasKey? {hasKey}");
			// GD.Print($"distance: {distance}");
			if( hasKey && distance <=_radius ) {
				var scene = GD.Load<PackedScene>("res://Scenes/Menus/Credits.tscn");
				GetTree().ChangeSceneTo(scene);
			}
		}
	}
}

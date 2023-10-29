using Godot;

public class AlarmEntity : SceneServiceNode {
	private Node _alarmManager;

	public override void _Ready() {
		base._Ready();
		_alarmManager = GetNode<Node>("AlarmManager");
	}

	public override void Alert(System.Numerics.Vector2 position) {
		_alarmManager.Call("_on_activate_alarm", new Vector2(position.X, position.Y));
	}
}
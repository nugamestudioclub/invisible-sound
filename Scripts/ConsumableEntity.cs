using Collections;
using Godot;

public class ConsumableEntity : SceneServiceNode {
	[Export]
	private ConsumableType _consumableType;

	public ConsumableType ConsumableType => _consumableType;
	public void _Consume() {
		GD.Print("consimung from entity");
		//consume the type
		IBlackboard blackboard = new Blackboard();
		blackboard.SetValue("messageType", "consume");
		blackboard.SetValue("consumableType", ConsumableType);
		OnMessage(new MessageEventArgs(blackboard));
		var sfx = GetPickupSound();
		Entity.Services.AudioService.PlayOneShot(sfx, (int)AudioTrack.Interactable, this);
	}

	private string GetPickupSound() {
		switch( _consumableType ) {
		case ConsumableType.Gas: return "res://Audio/SFX/Item Acquired Sound.wav";
		case ConsumableType.Battery: return "res://Audio/SFX/Low Bat.wav";
		case ConsumableType.Key: return "res://Audio/SFX/Item Acquired Sound.wav";
		case ConsumableType.Visualizer: return "res://Audio/SFX/Item Acquired Sound.wav";
		default: return null;
		}
	}
}
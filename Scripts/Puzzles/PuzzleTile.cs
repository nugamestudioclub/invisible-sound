using Collections;
using Godot;
using System;

public class PuzzleTile : Area2D {
	public PuzzleTileState State { get; private set; }

	[Export]
	private Texture goodTexture;
	[Export]
	private Texture badTexture;

	private Sprite currentSprite;

	public event EventHandler<AreaEventArgs> Entered;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		currentSprite = GetNode<Sprite>("Sprite");
	}

	public void ChangeState(PuzzleTileState newState) {
		State = newState;
		if( newState == PuzzleTileState.Danger ) {
			currentSprite.Texture = badTexture;
		}
		else {
			currentSprite.Texture = goodTexture;
		}
	}

	public void _BodyEntered(Node body) {
		GD.Print($"{nameof(PuzzleTile)}.{nameof(_BodyEntered)}");
		IBlackboard args = new Blackboard();
		// add x,y or other data to this
		args.SetValue("(x,y)", Name);
		AreaEventArgs areaEvent = new AreaEventArgs(this, body, args);
		if( body is KinematicBody2D kb ) {
			GD.Print($"\tcollided with {nameof(KinematicBody2D)}");
			kb.EmitSignal("area_collision", kb, this);
		}
		OnEntered(areaEvent);
	}

	protected virtual void OnEntered(AreaEventArgs e) {
		Entered?.Invoke(this, e);
	}
}

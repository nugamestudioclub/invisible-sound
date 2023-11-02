using Collections;
using Godot;

public class PlayerEntity : SceneServiceNode {
	[Export]
	private float _footstepSeconds = 0.5f;

	private float _footstepElapsed = float.MinValue;

	public PlayerEntity() {
		Type = EntityType.Character;
	}

	private bool HasFootstepStarted => _footstepElapsed >= 0f;

	private bool HasFootstepFinished => _footstepElapsed >= _footstepSeconds;

	public void _Player_area_collision(KinematicBody2D player, Area2D area) {
		// GD.Print($"{nameof(PlayerEntity)}.{nameof(_Player_area_collision)}");
		OnCollision(new CollisionEventArgs(this, player, area, new Blackboard()));
	}

	public void _Player_footstep() {
		if( !HasFootstepStarted )
			StartFootstep();
	}

	public override void _PhysicsProcess(float delta) {
		base._PhysicsProcess(delta);
		if( HasFootstepStarted )
			UpdateFootstep(delta);

	}

	private void StartFootstep() {
		// TODO: Detect ground material
		Entity.Services.AudioService.PlayFootstep("Dirt");
		_footstepElapsed = 0f;
	}

	private void UpdateFootstep(float delta) {
		if( HasFootstepFinished )
			_footstepElapsed =  float.MinValue;
		else if( HasFootstepStarted )
			_footstepElapsed += delta;
	}
}
using Collections;
using Godot;
using System;

public class PlayerEntity : SceneServiceNode
{
	bool firstFootstep = true;
    public void _Player_area_collision(KinematicBody2D player, Area2D area) {
		// GD.Print($"{nameof(PlayerEntity)}.{nameof(_Player_area_collision)}");
		OnCollision(new CollisionEventArgs(this, player, area, new Blackboard()));
    }

	public void _Player_footstep() {
		//GD.Print($"{nameof(PlayerEntity)}.{nameof(_Player_footstep)}");
		if (!firstFootstep)
		{
			return;
		}
		// Entity.Services.AudioService.PlayOneShot("res://Audio/Footsteps/Dirt/Dirt 1.wav", 0, this);

		//firstFootstep = false;
	}
}

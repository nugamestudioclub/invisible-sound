using Collections;
using Godot;
using System;

public class PlayerEntity : SceneServiceNode
{
    public void _Player_area_collision(KinematicBody2D player, Area2D area) {
		// GD.Print($"{nameof(PlayerEntity)}.{nameof(_Player_area_collision)}");
		OnCollision(new CollisionEventArgs(this, player, area, new Blackboard()));
    }
}

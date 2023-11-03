using Collections;
using Godot;
using System;

public class PlayerEntity : SceneServiceNode {

	public bool VizualizerActive { get; private set; }

    [Export]
	private float _footstepSeconds = 0.5f;

	private float _footstepElapsed = float.MinValue;

	private KinematicBody2D _kinematicBody;

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

	public PlayerEntity() {
		Type = EntityType.Character;
	}

	public override void _Ready() {
		_kinematicBody = GetNode<KinematicBody2D>("Player");
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
	private float lastTick;
	[Export]
	private float tickInterval = .5f;
	private float currentTime = 0;

	public override void _PhysicsProcess(float delta) {
		currentTime += delta;
		base._PhysicsProcess(delta);
		if( HasFootstepStarted )
			UpdateFootstep(delta);
		if (VizualizerActive && currentTime - lastTick > tickInterval)
		{
			lastTick = currentTime;
            if (Entity.Game.GlobalData.TryGetValue("currentBatteryLife", out float batteryLife))
            {
				if (batteryLife < 0)
				{
                    UpdateMonVisualizer(false);
                }
				else
				{
					
					Entity.Game.GlobalData.SetValue("currentBatteryLife", batteryLife - 1);
					var UI = Entity.Game.GetEntityByName("UIEntity");
					if (UI.Services.SceneService is UIEntity uiScene)
					{
						float percentLife = (batteryLife - 1) / Entity.Game.GlobalData.GetValue<float>("maxBatteryLife");
						uiScene.CurrentVisualizerProgress = percentLife * 100;
					}
				}
            }
        }

	}

	private void UpdateMonVisualizer(bool active)
	{
        VizualizerActive = active;
        if (Entity.Game.GetEntityByName("MonsterEntity").Services.SceneService is MonsterEntity monster)
        {
            monster.Vizualized = VizualizerActive;

        };
    }

    public override void _Input(InputEvent e)
    {
        base._Input(e);
        if (e is InputEventKey k && k.IsPressed())
        {
			if (k.Scancode == (int)KeyList.Q && VizualizerActive)
			{
				UpdateMonVisualizer(false);
            }
			else if (k.Scancode == (int) KeyList.Q)
			{
				if (Entity.Game.GlobalData.TryGetValue("currentBatteryLife", out float batteryLife )) {
					if (batteryLife > 0) {
                        UpdateMonVisualizer(true);


                    }
				}

            }

        }
    }

    private void StartFootstep() {
		var material = Entity.Services.SceneService.GetMaterialAt(ScenePosition);
		var materialName = SceneServiceProvider.GetName(material);
		// GD.Print(materialName);
		if( materialName == "None" )
			materialName = "Grass";
		Entity.Services.AudioService.PlayFootstep(materialName);
		_footstepElapsed = 0f;
	}

	private void UpdateFootstep(float delta) {
		if( HasFootstepFinished )
			_footstepElapsed =  float.MinValue;
		else if( HasFootstepStarted )
			_footstepElapsed += delta;
	}
}
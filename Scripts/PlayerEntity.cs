using Collections;
using Godot;

public class PlayerEntity : SceneServiceNode {

	public bool HasVisualizer { get; private set; }
    public bool HasGas { get; private set; }

    public bool HasKey { get; private set; }

    [Export]
	private float _maxBatteryLife;
	public float MaxBatteryLife => _maxBatteryLife;

    [Export]
    private float _batteryStep;
    public float BatteryStep => _batteryStep;

    public float CurrentBatteryLife { get; private set; }

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

	public void Collect(ConsumableEntity consumable)
	{
		//for UI updating, just get node and set it to visible/not visible
		switch(consumable.ConsumableType)
		{
			case ConsumableType.None:
				break;
			case ConsumableType.Key:
				HasKey = true;
				break;
            case ConsumableType.Gas:
				HasGas = true;
                break;
            case ConsumableType.Visualizer:
				HasVisualizer = true;
                break;
            case ConsumableType.Battery:
				CurrentBatteryLife = System.Math.Min(BatteryStep + CurrentBatteryLife, MaxBatteryLife);
				//update battery level in UI


                break;
        }
	}

    public void Use(ConsumableEntity consumable)
	{
        switch (consumable.ConsumableType)
        {
            case ConsumableType.None:
                break;
            case ConsumableType.Key:
				//make key ui invisible
                break;
            case ConsumableType.Gas:
				//make gas ui invisible
				//Entity.Game.GetEntityByName();
                break;
        }
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
		var material = Entity.Services.SceneService.GetMaterialAt(ScenePosition);
		var materialName = SceneServiceProvider.GetName(material);
		GD.Print(materialName);
		if( materialName == "None" )
			materialName = "Dirt";
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
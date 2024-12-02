using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene;

	private Timer _mobTimer;
	private Player _player;

	private UI ui;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNode<Player>("Player");
		ui = GetNode<UI>("Ui");
		_mobTimer = GetNode<Timer>("MobTimer");
		_mobTimer.Timeout += CreateMob;
		//mobTimer.Start();
		GameInit();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void CreateMob()
	{
		Mob mob = MobScene.Instantiate<Mob>();
		var follow = GetNode<PathFollow2D>("Path2D/PathFollow2D");
		follow.ProgressRatio = GD.Randf();

		mob.Position = follow.Position;
		mob.Rotation = follow.Rotation + Mathf.Pi/2;
		mob.Rotation += (float)GD.RandRange(-Mathf.Pi/4, Mathf.Pi/4);
		
		var moveDirection = Vector2.Right.Rotated(mob.Rotation);
		mob.LinearVelocity = moveDirection * GD.RandRange(100, 150);
		
		this.AddChild(mob);
	}

	public void GameInit()
	{
		// Player 
		_player.Hide();
		// Mob
		_mobTimer.Stop();
		// UI
		ui.UIInit();
	}

	public void GameBegin()
	{
		// Player
		_player.Show();
		// Mob
		_mobTimer.Start();
		// UI
		ui.UIBegin();
	}

	public void GameOver()
	{
		GetNode<AudioStreamPlayer2D>("GameOver").Play();
		// Player
		_player.Hide();
		// Mob
		_mobTimer.Stop();
		GetTree().CallGroup("mobs", MethodName.QueueFree);
		// UI
		ui.UIOver();
	}
}

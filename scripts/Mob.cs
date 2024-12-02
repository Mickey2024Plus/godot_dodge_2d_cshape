using Godot;
using System;

public partial class Mob : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// GD.Print("Mob ready");
		var mobAnimatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		string[] mobTypes = mobAnimatedSprite.SpriteFrames.GetAnimationNames();
		mobAnimatedSprite.Play(mobTypes[GD.Randi() % mobTypes.Length]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void OnViOnVisibleOnScreenEnabler2DScreenExited()
	{
		// GD.Print("OnViOnVisibleOnScreenEnabler2DScreenExited called");
		QueueFree();
	}
}

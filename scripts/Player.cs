using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int Speed;

	private Vector2 _screenSize;

	private AnimatedSprite2D _animatedSprite2D;
	
	[Signal]
	public delegate void HitEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//GD.Print("Player Ready");
		_screenSize = GetViewportRect().Size;
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		//GD.Print(_animatedSprite2D.Name);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var direction = Input.GetVector("my_left", "my_right", "my_up", "my_down");
		//GD.Print(direction);
		Position += direction.Normalized() * Speed * (float)delta;
		Position = new Vector2(Mathf.Clamp(Position.X, 0, _screenSize.X), Mathf.Clamp(Position.Y, 0, _screenSize.Y));

		if (direction.Length() > 0)
		{
			if (direction.X != 0)
			{
				_animatedSprite2D.Animation = "walk_right";
				_animatedSprite2D.FlipH = direction.X < 0;
				_animatedSprite2D.FlipV = false;
			} else if (direction.Y != 0)
			{
				_animatedSprite2D.Animation = "walk_up";
				_animatedSprite2D.FlipV = direction.Y > 0;
			}
			_animatedSprite2D.Play();
		}
		else
		{
			_animatedSprite2D.Stop();
		}
	}

	public void OnBodyEntered(Node2D node)
	{
		//GD.Print("OnBodyEntered called");
		this.EmitSignal(SignalName.Hit);
	}
}

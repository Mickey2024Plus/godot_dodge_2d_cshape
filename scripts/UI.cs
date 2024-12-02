using Godot;
using System;
using System.Threading.Tasks;

public partial class UI : CanvasLayer
{
	private Label _titleLabel;

	private Label _scoreLabel;

	private Button _beginBtn;

	private Timer _scoreTimer;

	private int _scoreValue;

	[Signal]
	public delegate void BeginGameEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_titleLabel = GetNode<Label>("Title");
		_scoreLabel = GetNode<Label>("Score");
		_beginBtn = GetNode<Button>("BeginBtn");
		_scoreTimer = GetNode<Timer>("ScoreTimer");

		_scoreTimer.Timeout += () =>
		{
			_scoreValue++;
			_scoreLabel.Text = _scoreValue.ToString();
		};
		
		_scoreTimer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UIInit()
	{
		_scoreTimer.Stop();
		_scoreLabel.Text = "0";
		_scoreLabel.Hide();
		_titleLabel.Show();
		_beginBtn.Show();
	}

	public void UIBegin()
	{
		_scoreTimer.Start();
		_scoreLabel.Show();
		_titleLabel.Hide();
		_beginBtn.Hide();
	}

	public void UIOver()
	{
		_scoreTimer.Stop();
		_scoreValue = 0;
		_scoreLabel.Text = "0";
		_scoreLabel.Hide();
		_titleLabel.Show();
		_beginBtn.Show();
	}

	public void OnBeginBtnPressed()
	{
		this.EmitSignal("BeginGame");
	}
}

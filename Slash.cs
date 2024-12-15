namespace GamblingWizard;
using Godot;
using System;


public partial class Slash : Node2D
{
	private AnimationPlayer _animationPlayer;

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationPlayer.AnimationFinished += OnAnimationFinished;
	}

	
	private void OnAnimationFinished(StringName animName)
	{
		QueueFree();
	}
	
}

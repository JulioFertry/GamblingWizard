using Godot;
using System;

public partial class Battle : Node2D
{
	private Slime? _enemy;

	
	public override void _Ready()
	{
		_enemy = GetNodeOrNull<Slime>("Slime");
		if (_enemy == null)
		{
			GD.PrintErr("Slime node not found!");
		}
	}
	
	
	private void _onAttackButtonPressed()
	{
		if (_enemy != null)
		{
			_enemy.ReceiveDamage(4);
			_enemy.UpdateHealthLabel();
		}
		else
		{
			GD.PrintErr("No enemy to attack!");
		}

	}
	
}

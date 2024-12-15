namespace GamblingWizard;
using Godot;
using System;


public partial class Battle : Node2D
{
	private Player _player;
	private Slime _enemy;

	
	public override void _Ready()
	{
		_player = GetNodeOrNull<Player>("Player");
		if (_player == null)
		{
			GD.PrintErr("Player node not found!");
		}
		
		_enemy = GetNodeOrNull<Slime>("Slime");
		if (_enemy == null)
		{
			GD.PrintErr("Slime node not found!");
		}
		
		if (_player != null && _enemy != null)
		{
			_player.SetTarget(_enemy);
		}
		
	}
	
	
	private void _onEnemyAttack()
	{
		if (_enemy != null && _player != null)
		{
			_enemy.Attack(_player);
		}
	}
	
}
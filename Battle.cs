using System.Threading.Tasks;

namespace GamblingWizard;
using Godot;
using System;


public partial class Battle : Node2D
{
	private Player _player;
	private Slime _enemy;

	private bool _isPlayerTurn = true;
	
	
	public override void _Ready()
	{
		_player = GetNodeOrNull<Player>("Player");
		if (_player != null)
		{
			_player.PlayerAttacked += _onPlayerAttack;
		}
		else
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
			_enemy.SetTarget(_player);
		}
		
	}
	
	
	private async void _onPlayerAttack()
	{
		if (_isPlayerTurn)
		{
			_isPlayerTurn = false;
			await StartEnemyTurn();
		}
		else
		{
			GD.PrintErr("It's not your turn!");
		}
	}
	
	
	private async void _onEnemyAttack()
	{
		if (_enemy != null && _player != null)
		{
			await _enemy.Attack();
		}
	}
	
	
	private void EndEnemyTurn()
	{
		_isPlayerTurn = true;
		_player.SetAttackButtonState(true);
	}
	
	
	private async Task StartEnemyTurn()
	{
		
		if (_enemy != null && _player != null)
		{
			await ToSignal(GetTree().CreateTimer(1.0f), "timeout");
			await _enemy.Attack();
		}
		
		EndEnemyTurn();
	}
	
}
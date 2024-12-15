using GamblingWizard.scenes;

namespace GamblingWizard;
using System.Threading.Tasks;
using Godot;
using System;


public partial class Battle : Node2D
{
	private int _roundCounter;
	private Player _player;
	private IEnemy _enemy;
	
	private PackedScene _slimeScene;
	private PackedScene _spiderScene;
	private PackedScene _crabScene;

	private bool _isPlayerTurn = true;
	
	
	public override void _Ready()
	{
		_roundCounter = 0;
		_player = GetNodeOrNull<Player>("Player");
		if (_player != null)
		{
			_player.PlayerAttacked += _onPlayerAttack;
		}
		else
		{
			GD.PrintErr("Player node not found!");
		}
		
		_slimeScene = GD.Load<PackedScene>("res://scenes/slime.tscn");
		_spiderScene = GD.Load<PackedScene>("res://scenes/spider.tscn");
		_crabScene = GD.Load<PackedScene>("res://scenes/crab.tscn");
		
		SpawnEnemy();
	}
	
	
	private void SpawnEnemy()
	{
		if (_enemy is Node enemyNode && IsInstanceValid(enemyNode))
		{
			enemyNode.QueueFree();
		}
		
		Random random = new Random();
		int choice = random.Next(3);
		PackedScene chosenScene;
		if (choice == 0)
			chosenScene = _slimeScene;
		else if (choice == 1)
			chosenScene = _spiderScene;
		else
			chosenScene = _crabScene;


		Node2D enemyNode2D = chosenScene.Instantiate<Node2D>();
		AddChild(enemyNode2D);
		
		Rect2 viewportRect = GetViewportRect();
		Vector2 centerPosition = viewportRect.Size / 2;
		enemyNode2D.GlobalPosition = centerPosition;
		
		_enemy = enemyNode2D as IEnemy;
		if (_enemy == null)
		{
			GD.PrintErr("This enemy is not an IEnemy");
			return;
		}
		
		Node enemyInstance = _enemy as Node;
		if (enemyInstance != null)
		{
			enemyInstance.Connect("EnemyDied", new Callable(this, nameof(OnEnemyDied)));
		}
		
		if (_player != null)
		{
			_player.SetTarget(_enemy);
			_enemy.SetTarget(_player);
		}
	}
	
	
	private void OnEnemyDied()
	{
		SpawnEnemy();
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

using Godot;
using System;

public partial class Battle : Node2D
{
	private int _enemyHealth = 30;
	private Label? _healthLabel;
	
	
	public override void _Ready()
	{
		_healthLabel = GetNode<Label>("Snake/HealthLabel");
		UpdateHealthLabel();
	}


	public override void _Process(double delta)
	{
		
	}


	private void _onAttackButtonPressed()
	{
		GD.Print("Attack!");
		DecreaseEnemyHealth(10);
	}


	private void DecreaseEnemyHealth(int damage)
	{
		_enemyHealth = Math.Max(0, _enemyHealth - damage);
		UpdateHealthLabel();

		if (_enemyHealth <= 0)
		{
			GD.Print("Enemy defeated!");
		}
	}


	private void UpdateHealthLabel()
	{
		if (_healthLabel != null)
		{
			_healthLabel.Text = $"{_enemyHealth} HP";
		}
		else
		{
			GD.PrintErr("HealthLabel node not found!");
		}
	}
	
}

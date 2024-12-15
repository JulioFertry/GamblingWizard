namespace GamblingWizard;
using Godot;
using System;


public partial class Player : Node
{
	public string PlayerName { get; set; } = "Kiko";
	private int _maxHealth = 100;
	private int _currentHealth;

	public int Health
	{
		get => _currentHealth;
		set
		{
			_currentHealth = Math.Max(0, value);
			UpdateStatsLabels();
			if (_currentHealth <= 0)
			{
				Die();
			}
		}
	}

	private int _damage = 4;

	public int Power
	{
		get => _damage;
		set
		{
			_damage = value;
			UpdateStatsLabels();
		}
	}
	
	private Label PlayerHealthLabel { get; set; }
	private Label PlayerNameLabel { get; set; }
	private Label PlayerDamageLabel { get; set; }
	private IEnemy _target;


	public override void _Ready()
	{
		_currentHealth = _maxHealth;
		
		PlayerHealthLabel = GetNodeOrNull<Label>("PlayerUI/PlayerStats/StatsContainer/PlayerHpLabel");
		PlayerNameLabel = GetNodeOrNull<Label>("PlayerUI/PlayerStats/StatsContainer/PlayerNameLabel");
		PlayerDamageLabel = GetNodeOrNull<Label>("PlayerUI/PlayerStats/StatsContainer/PlayerDmgLabel");
		
		if (PlayerNameLabel != null) PlayerNameLabel.Text = PlayerName;
		UpdateStatsLabels();

		GD.Print($"Bienvenido a la grieta de la ludopatía, {PlayerName}!");
	}

	
	private void UpdateStatsLabels()
	{
		if (PlayerHealthLabel != null)
		{
			PlayerHealthLabel.Text = $"{_currentHealth} HP";
		}

		if (PlayerDamageLabel != null)
		{
			PlayerDamageLabel.Text = $"{_damage} DMG";
		}
	}
	
	
	public void SetTarget(IEnemy target)
	{
		_target = target;
	}
	
	
	private void _onAttackButtonPressed()
	{
		if (_target != null)
		{
			GD.Print($"{PlayerName} attacks {_target.MonsterName}");
			_target.ReceiveDamage(4);
		}
		else
		{
			GD.PrintErr("No enemy to attack!");
		}
	}
	
	
	public void ReceiveDamage(int damage)
	{
		if (Health > 0)
		{
			Health -= damage;
		}
	}
	
	
	public void Die()
	{
		GD.Print($"Has muerto, {PlayerName}");
		GetTree().Quit();
	}
	
}
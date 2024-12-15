namespace GamblingWizard;
using Godot;
using System;


public partial class Slime : Node2D, IEnemy
{

	private int _health;
	private int _damage;
	public string MonsterName { get; set; } = "Slime";
	private Label HealthLabel { get; set; }
	private AnimationPlayer Animations { get; set; }

	public int Health
	{
		get => _health;
		set
		{
			_health = Math.Max(0, value);
			UpdateHealthLabel();
			if (_health <= 0)
			{
				GD.Print($"{MonsterName} ha muerto!");
				Die();
			}
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HealthLabel = GetNode<Label>("HealthLabel");
		Animations = GetNode<AnimationPlayer>("AnimationPlayer");
		Random random = new Random();
		this._health = random.Next(15, 46);
		this.Health = this._health;
		this._damage = random.Next(2, 8);
		GD.Print($"Un {this.MonsterName} con {this.Health} puntos de vida ha aparecido!");
		UpdateHealthLabel();
	}


	public void UpdateHealthLabel()
	{
		if (HealthLabel != null)
		{
			HealthLabel.Text = $"{this._health} hp";
		}
	}
	
	
	public void Attack(Player player)
	{
		if (_health > 0)
		{
			GD.Print($"{MonsterName} ataca a {player.PlayerName} causando {_damage} puntos de daño");
			Animations?.Play("attack");
			player.ReceiveDamage(_damage);
		}
	}
	
	
	public void ReceiveDamage(int damage)
	{
		if (_health > 0)
		{
			GD.Print($"{Name} recibe {damage} de daño");
			Animations.Play("shake");
			Health -= damage;
		}
	}


	public void Die()
	{
		QueueFree();
	}
	
}
using Godot;
using System;


public partial class Slime : Node2D
{

	private int _health;
	public string MonsterName { get; set; } = "Slime";
	private Label? HealthLabel { get; set; }

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
			}
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HealthLabel = GetNode<Label>("HealthLabel");
		Random random = new Random();
		this._health = random.Next(15, 46);
		this.Health = this._health;
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


	public void ReceiveDamage(int damage)
	{
		GD.Print($"{Name} recibe {damage} de daño");
		Health -= damage;
	}

	

}

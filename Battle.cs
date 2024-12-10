using Godot;
using System;

public partial class Battle : Node2D
{

	private int _enemyHealth = 30;
	private Label? _healthLabel;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_healthLabel = GetNode<Label>("HealthLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	
	//
	private void _onAttackButton()
	{
		GD.Print("Attack!");
	}
	
	//
	private void UpdateHealthLabel()
	{
		_healthLabel.Text = $"{_enemyHealth} hp";
	}
	
}

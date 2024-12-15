namespace GamblingWizard.scenes;
using System;


public partial class Slime : BaseEnemy, IEnemy
{
	public override string MonsterName { get; } = "Slime";

	
	public override void _Ready()
	{
		Random random = new Random();
		Health = random.Next(15, 31);
		Damage = random.Next(2, 8);
		base._Ready();
	}
	
}

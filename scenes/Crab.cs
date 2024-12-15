namespace GamblingWizard.scenes;
using System;


public partial class Crab : BaseEnemy, IEnemy
{
	public override string MonsterName { get; } = "Cangrejo";

	
	public override void _Ready()
	{
		Random random = new Random();
		Health = random.Next(12, 21);
		Damage = random.Next(14, 23);
		base._Ready();
	}
	
}
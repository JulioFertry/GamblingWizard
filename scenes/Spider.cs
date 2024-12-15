namespace GamblingWizard.scenes;
using System;


public partial class Spider : BaseEnemy, IEnemy
{
	public override string MonsterName { get; } = "Araña";

	
	public override void _Ready()
	{
		Random random = new Random();
		Health = random.Next(30, 51);
		Damage = random.Next(5, 11);
		base._Ready();
	}
	
}

namespace GamblingWizard;
using Godot;


public partial class StartButton : Button
{
	public override void _Ready()
	{
		Pressed += OnPressed;
	}

	
	private void OnPressed()
	{
		GetTree().ChangeSceneToFile("res://battle.tscn");
	}
	
}

namespace GamblingWizard;
using Godot;


public partial class ExitButton : Button
{
	public override void _Ready()
	{
		Pressed += OnPressed;
	}

	
	private void OnPressed()
	{
		GetTree().Quit();
	}
	
}

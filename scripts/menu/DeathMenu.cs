using Godot;
using System;

public partial class DeathMenu : Control
{
	public void OnPlayPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}

	public void OnMainPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/mainmenu.tscn");
	}
}

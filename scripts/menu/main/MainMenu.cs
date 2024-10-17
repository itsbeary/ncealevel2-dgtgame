using Godot;
using System;

public partial class MainMenu : Control
{
	public void _on_play_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}
	public void _on_quit_pressed()
	{
		GetTree().Quit();
	}
}

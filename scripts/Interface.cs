using Godot;
using System;

public partial class Interface : Control
{
	public Game Game => (Game)GetParent();

	public Label MoneyLabel => GetNode<Label>("Money/Label");

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MoneyLabel.Text = Game.Balance.ToString();
	}
}

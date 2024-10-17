using Godot;
using System;

public partial class Interface : Control
{
	public Game Game => (Game)GetParent();

	public Label MoneyLabel => GetNode<Label>("Money/Label");
	public ProgressBar HealthBar => GetNode<ProgressBar>("HealthBar");

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MoneyLabel.Text = Game.Balance.ToString();
		HealthBar.Value = Game.Player.Health;
	}
}

using Godot;
using System;

public partial class Shop : Control
{
	public ProgressBar BulletProgress => GetNode<ProgressBar>("BulletDamage");
	public ProgressBar SpeedProgress => GetNode<ProgressBar>("Speed");

	public Button BulletButton => GetNode<Button>("BulletDamage/Button");
	public Button SpeedButton => GetNode<Button>("Speed/Button");

	public int bulletCost = 100;
	public int speedCost = 100;

	public Game Game => (Game)GetParent();

	public void OnBulletButton()
	{
		if (BulletProgress.Value >= 100)
			return;
		if(Game.Balance >= bulletCost)
		{
			Game.Balance -= bulletCost;
			bulletCost *= 2;
			Game.bulletDamageBonus += 10;
			BulletProgress.Value = Game.bulletDamageBonus;
			BulletButton.Text = "Purchase +10% for " + bulletCost;
		}
	}
	public void OnSpeedButton()
	{
		if (SpeedProgress.Value >= 100)
			return;
		if (Game.Balance >= speedCost)
		{
			Game.Balance -= speedCost;
			speedCost *= 2;
			Game.speedBonus += 10;
			SpeedProgress.Value = Game.speedBonus;
			Game.Player.Speed = Game.Player.Speed + (Game.Player.Speed * (Game.speedBonus / 100));
			SpeedButton.Text = "Purchase +10% for " + speedCost;
		}
	}
	public void _on_quitbutton_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/mainmenu.tscn");

	}
}

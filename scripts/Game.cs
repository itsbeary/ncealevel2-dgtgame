using Godot;
using System;

public partial class Game : Node2D
{
	// Game 
	private int CurrentWave = 1;
	public bool IsGamePaused = false;

	// Game Stats
	public int EnemyKilled = 0;
	public int Balance = 0;

	// Enemy Spawning
	private float SpawnEnemyDelay = 0.5f;
	private double TimeSinceEnemySpawn = Time.GetUnixTimeFromSystem();
	private Vector2[] EnemySpawns => new Vector2[4]
	{
		new(ScreenSize.X, ScreenSize.Y),
		new(0, 0),
		new(0, ScreenSize.Y),
		new(ScreenSize.X, 0)
	};
	private PackedScene EnemyScene = (PackedScene)ResourceLoader.Load("res://scenes/enemy.tscn");

	// Other
	private Vector2 ScreenSize { get => GetViewportRect().Size; }
	private RandomNumberGenerator Random = new RandomNumberGenerator();
	public Player Player => GetNode<Player>("Player");
	public Shop Shop => GetNode<Shop>("Shop");
	public DeathMenu DeathMenu => GetNode<DeathMenu>("Death");

	// Shop Percent
	public int speedBonus = 0;
	public int bulletDamageBonus = 0;

	public override void _Ready()
	{
		Shop.Hide();
		DeathMenu.Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Player.Health <= 10f)
			return;
		if(!IsGamePaused && Input.IsActionJustPressed("openshop"))
		{
			Shop.Show();
			IsGamePaused = true;
		} else if(IsGamePaused && Input.IsActionJustPressed("openshop")) {
			Shop.Hide();
			IsGamePaused = false;
		}

		if (IsGamePaused)
			return;
		TrySpawnEnemy();
	}
	private void TrySpawnEnemy()
	{
		if (Time.GetUnixTimeFromSystem() - TimeSinceEnemySpawn < SpawnEnemyDelay)
			return;

		Enemy enemy = (Enemy)EnemyScene.Instantiate();
		enemy.Name = "Enemy - " + Random.Randi();
		AddChild(enemy);
		enemy.Position = EnemySpawns[Random.RandiRange(0, 3)];
		TimeSinceEnemySpawn = Time.GetUnixTimeFromSystem();
		if(Random.Randf() <= 0.05f)
		{
			enemy.Scale *= 2.5f;
			enemy.Speed *= 2.5f;
			enemy.IsLarge = true;
		}
	}
}

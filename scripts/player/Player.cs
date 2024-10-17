using Godot;

public partial class Player : CharacterBody2D
{
	// Movement
	[Export, ExportCategory("Movement")]
	public float Speed { get; set; } = 400f;
	private AnimatedSprite2D AnimatedSprite { get => GetNode<AnimatedSprite2D>("AnimatedSprite2D"); }

	// Weapon
	[Export, ExportCategory("Weapon")]
	public float CanShootDelay { get; set; } = 0.2f;
	private double TimeSinceShot = Time.GetUnixTimeFromSystem();
	private PackedScene BulletScene = (PackedScene) ResourceLoader.Load("res://scenes/bullet.tscn");
	private Marker2D WeaponMarker { get => GetNode<Marker2D>("Marker2D"); }

	private Game Game => (Game)GetParent();


	// Other
	private Vector2 ScreenSize { get => GetViewportRect().Size; }
	public float Health { get; private set; } = 100f;


	public override void _PhysicsProcess(double delta)
	{
		if (Game.IsGamePaused)
			return;
		// Input / Movement
		MovePlayer();
		RotatePlayer();
		ClampToBounds();
		// Animation
		PlayAnimation();

		// Gun
		Shoot();
	}

	private void MovePlayer()
	{
		var input = Input.GetVector("left", "right", "forward", "backward");
		Velocity = input * Speed;
		MoveAndSlide();
	}
	private void PlayAnimation()
	{
		if (Velocity.IsZeroApprox())
			AnimatedSprite.Play("idle");
		else AnimatedSprite.Play("fly");
	}

	private void Shoot()
	{
		if (!Input.IsActionJustPressed("attack1") || (Time.GetUnixTimeFromSystem() - TimeSinceShot) < CanShootDelay)
			return;
		TimeSinceShot = Time.GetUnixTimeFromSystem();

		Bullet bullet = (Bullet) BulletScene.Instantiate();
		GetParent().AddChild(bullet);
		bullet.Rotation = Rotation;
		bullet.Position = WeaponMarker.GlobalPosition;
		bullet.Velocity = -GlobalTransform.Y.Normalized();
	}

	public void DamagePlayer()
	{
		Health -= 10f;
		if (Health <= 10f)
		{
			Game.DeathMenu.Show();
			Game.IsGamePaused = true;
		}
			
	}

	private void RotatePlayer()
		=> LookAt(ToGlobal(ToLocal(GetGlobalMousePosition()).Rotated(Mathf.Pi/2)));

	private void ClampToBounds() 
		=> Position = new Vector2(Mathf.Clamp(Position.X, 0, ScreenSize.X), Mathf.Clamp(Position.Y, 0, ScreenSize.Y));
}

using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 400f;

	public Vector2 ScreenSize { get => GetViewportRect().Size; }
	public AnimatedSprite2D AnimatedSprite { get => GetNode<AnimatedSprite2D>("AnimatedSprite2D"); }


	public override void _PhysicsProcess(double delta)
	{
		// Input / Movement
		MovePlayer();
		RotatePlayer();
		ClampToBounds();
		// Animation
		PlayAnimation();
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

	private void RotatePlayer()
		=> LookAt(ToGlobal(ToLocal(GetGlobalMousePosition()).Rotated(Mathf.Pi/2)));

	private void ClampToBounds() 
		=> Position = new Vector2(Mathf.Clamp(Position.X, 0, ScreenSize.X), Mathf.Clamp(Position.Y, 0, ScreenSize.Y));
}

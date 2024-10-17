using Godot;

public partial class Bullet : Area2D
{
	[Export]
	public float Speed = 800f;
	public Vector2 Velocity = Vector2.Zero;

	private Vector2 ScreenSize { get => GetViewportRect().Size; }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Position += Velocity.Normalized() * Speed * (float) delta;
		if (Position.X >= ScreenSize.X - 1 || Position.Y >= ScreenSize.Y - 1)
			QueueFree();

	}
}

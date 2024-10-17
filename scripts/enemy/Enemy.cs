using Godot;
using System;

public partial class Enemy : Area2D
{
	public Node2D Player => GetNode<Node2D>("../Player");
	public AnimatedSprite2D AnimatedSprite => GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	public Vector2 Velocity = Vector2.Zero;

	private float Speed = 100f;
	private float Health = 100f;

	public override void _PhysicsProcess(double delta)
	{
		if(Position.DistanceTo(Player.Position) > 325) {
			AnimatedSprite.Play("idle");
			Velocity = (Player.GlobalPosition - Position).Normalized() * Speed;
		}
		else {
			AnimatedSprite.Play("lock");
			Velocity = (Player.GlobalPosition - Position).Normalized() * Speed * 1.5f;
		}
		Position += Velocity.Normalized() * Speed * (float)delta;
		LookAt(Player.GlobalPosition);
		Rotate(160);
	}
	public void _on_body_entered(Node2D body)
	{
		if (body.Equals(Player))
			GD.Print("player was attacked by " + Name);
		GD.Print($"Body entered: {body.Name}");
	}
}

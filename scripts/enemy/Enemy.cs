using Godot;
using System;

public partial class Enemy : Area2D
{
	public Player Player => GetNode<Player>("../Player");
	public AnimatedSprite2D AnimatedSprite => GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	public Vector2 Velocity = Vector2.Zero;

	public PackedScene DeathParticle = (PackedScene)ResourceLoader.Load("res://scenes/deathparticle.tscn");

	public float Speed = 100f;
	private float Health = 100f;
	public bool IsLarge = false;

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

	public void Kill()
	{
		Health -= !IsLarge ? 60f : 30f;
		if (Health > 0f)
			return;
		var particle = (DeathParticle)DeathParticle.Instantiate();
		GetParent().AddChild(particle);
		particle.Position = GlobalPosition;
		particle.Emitting = true;
		particle.hasPlayed = true;
		((Game)GetParent()).Balance += 10;
		QueueFree();
	}

	public void _on_body_entered(Node2D body)
	{
		if (body.Equals(Player)) { 
			Player.DamagePlayer();
			Kill();
		}
	}
}

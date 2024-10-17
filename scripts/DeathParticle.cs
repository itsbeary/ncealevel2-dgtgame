using Godot;
using System;

public partial class DeathParticle : CpuParticles2D
{

	public bool hasPlayed = false;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!Emitting && hasPlayed)
			QueueFree();
	}
}

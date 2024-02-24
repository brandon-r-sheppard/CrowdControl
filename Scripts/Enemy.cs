using Godot;
using System;

public partial class Enemy : Node3D
{
	private Random rnd;
	private SquadManager manager;
	private float RunSpeed = 2f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CallDeferred("Generate");
	}
	
	public override void _Process(double delta)
	{
		Position -= new Vector3(0, 0, (float) (RunSpeed * GetProcessDeltaTime()));
	}
	
	private void Generate()
	{
		rnd = new Random();
		manager = GetNode<SquadManager>("SquadSpawner");
		
		int cnt = rnd.Next(1,10);
		manager.EnterGate(cnt);
	}
	
}

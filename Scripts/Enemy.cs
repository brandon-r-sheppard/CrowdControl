using Godot;
using System;

public partial class Enemy : Node3D
{
	private Random rnd;
	private SquadManager manager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CallDeferred("Generate");
	}
	
	private void Generate()
	{
		rnd = new Random();
		manager = GetNode<SquadManager>("SquadSpawner");
		
		int cnt = rnd.Next(1,10);
		manager.EnterGate(cnt);
	}
}

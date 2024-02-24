using Godot;
using System;

public partial class EnemySpawner : Node3D
{
	private PackedScene enemies;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(Position.Z > 10)
		{
			enemies = GD.Load<PackedScene>("res://Scenes/EnemySquad.tscn");
			Node3D Enemy = enemies.Instantiate<Node3D>();
			Enemy.Position = Position + new Vector3(0,0,2);
			AddChild(Enemy);
		}
	}
}

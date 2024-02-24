using Godot;
using System;

public partial class BlueGuy : RigidBody3D
{
	private bool collided = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("body_entered", new Callable(this, nameof(_on_body_entered)));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _on_body_entered(Node body)
	{
		
		if (collided) {
			return;
		}
		
		if(body.IsInGroup("Enemy")) {
			EnemyStatus es = body as EnemyStatus;
			
			if(es == null){
				return;
			}
			if(!es.alive){
				return;
			}
			QueueFree();
			body.QueueFree();
			
			es.alive = false;
		}	
	}
}







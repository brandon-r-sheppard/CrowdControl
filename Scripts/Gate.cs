using Godot;
using System;

public partial class Gate : Area3D
{
	private Random rnd;
	private int reward;
	private bool collidedWithPlayer = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rnd = new Random();
		reward = rnd.Next(1,10);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_body_entered(Node body)
	{
		if (collidedWithPlayer) {
			return;
		}
		
		if( body.IsInGroup("Player")) {
			collidedWithPlayer = true;
			Node currentNode = body;
			SquadManager squadManager = null;

			while (currentNode != null)
			{
				squadManager = currentNode as SquadManager;
				if (squadManager != null)
				{
					// SquadManager found, break out of the loop
					break;
				}
				// Move up to the next parent node
				currentNode = currentNode.GetParent();
			}

			if (squadManager != null)
			{
				// Call the EnterGate method on the SquadManager instance
				squadManager.EnterGate(reward);
				// Schedule this node to be deleted in the next frame
	   			CallDeferred("queue_free");
			}
			
		}
	}
}




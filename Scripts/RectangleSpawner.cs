using Godot;
using System;
using System.Collections.Generic;

public partial class RectangleSpawner : Node3D
{
	private float spawnInterval = 2.0f;
	private PackedScene rectangleScene;
	private List<Node3D> spawnedRectangles = new List<Node3D>(); // List to track rectangles
	private int maxRectangles = 500; // Maximum number of rectangles to keep in the scene
	private Vector3 spawnOffset = new Vector3(0, 0, 20); // offset
	private Vector3 currentPosition = Vector3.Zero; // spawn position

	public override void _Ready()
	{
		rectangleScene = GD.Load<PackedScene>("res://Scenes/Rectangle.tscn");
		var timer = new Timer();
		AddChild(timer);
		timer.WaitTime = spawnInterval;
		timer.Autostart = true;
		timer.Connect("timeout", new Callable(this, nameof(OnTimerTimeout)));
		timer.Start();
		
		
		OnTimerTimeout();
		OnTimerTimeout();
		OnTimerTimeout();
		OnTimerTimeout();
		OnTimerTimeout();
	}

	private void OnTimerTimeout()
	{
		Node instance = rectangleScene.Instantiate<Node>();
		if (instance is Node3D newMeshInstance)
		{
			newMeshInstance.GlobalTransform = new Transform3D(Basis.Identity, currentPosition);
			AddChild(newMeshInstance);
			spawnedRectangles.Add(newMeshInstance); // Add the new instance to the list

			// Move the spawn position forward for the next rectangle
			currentPosition += spawnOffset;

			// Check if we need to remove the oldest rectangle
			if (spawnedRectangles.Count > maxRectangles)
			{
				Node3D oldestRectangle = spawnedRectangles[0];
				oldestRectangle.QueueFree(); // Remove from scene
				spawnedRectangles.RemoveAt(0); // Remove from list
			}
		}
		else
		{
			GD.Print("Instantiated scene's root is not a Node3D!");
			instance.QueueFree();
		}
	}
}

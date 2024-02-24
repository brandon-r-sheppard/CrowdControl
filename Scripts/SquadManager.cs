using Godot;
using System;
using System.Collections.Generic;

public partial class SquadManager : Node
{
	[Export]
	string res = "res://Scenes/BlueDude.tscn";
	private PackedScene character;
	
	// Adjust the radius increment to control spacing between the characters
	private float baseRadius = 0.1f; // Base radius for the circle
	private float radiusIncrement = 0.1f; // Increment per layer of the circle
	private int charactersPerLayer = 8; // Estimate characters per layer
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		character = GD.Load<PackedScene>(res);
		AddAlly();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ArrangeCharacters();
	}
	
	public void EnterGate(int Allies)
	{
		for(int i = 0; i < Allies; i++)
		{
			AddAlly();
		}
		ArrangeCharacters();
		
	}
	private void AddAlly()
	{
		Node3D instance = character.Instantiate<Node3D>();
		instance.GlobalTransform = new Transform3D(Basis.Identity, new Vector3(0,0,0));
		AddChild(instance);
	}
	
	private void ArrangeCharacters()
	{
		var characters = GetChildren(); // Assuming all children are the characters
		int totalCharacters = characters.Count;
		float currentRadius = baseRadius;
		int charactersPlaced = 0;
		int layer = 0;

		while (charactersPlaced < totalCharacters)
		{
			int layerCapacity = CalculateLayerCapacity(layer);
			float angleIncrement = 360.0f / layerCapacity;

			for (int i = 0; i < layerCapacity && charactersPlaced < totalCharacters; i++)
			{
				float angleDegrees = angleIncrement * i;
				Vector3 position = CalculatePositionOnCircle(currentRadius, angleDegrees);
				Node3D character = (Node3D)characters[charactersPlaced];
				character.Transform = new Transform3D(Basis.Identity, position);
				
				charactersPlaced++;
			}

			layer++;
			currentRadius += radiusIncrement;
		}
	}

	private int CalculateLayerCapacity(int layer)
	{
		// Adjust this calculation based on the desired density and spacing of characters
		return charactersPerLayer + layer * 6; // Example growth rate
	}

	private Vector3 CalculatePositionOnCircle(float radius, float angleDegrees)
	{
		// Convert degrees to radians for Godot's sin and cos functions
		float angleRadians = angleDegrees * Mathf.Pi / 180.0f;
		return new Vector3(Mathf.Cos(angleRadians) * radius, 0, Mathf.Sin(angleRadians) * radius);
	}
}

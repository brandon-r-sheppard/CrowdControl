using Godot;
using System;

public partial class RunAnimation : AnimationPlayer
{

	public override void _Ready()
	{
		Play("Running"); // Replace "MyAnimation" with the name of your animation
	}
}

using Godot;
using System;

public partial class PlayerController : Node3D
{
	// Speed at which the camera moves
	public float MoveSpeed = 100f;
	public float RunSpeed = 4f;
	public float Limit = 2f;

	public override void _Ready()
	{
		// Make sure the camera processes input events
		SetProcessInput(true);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector3(0, 0, (float) (RunSpeed * GetProcessDeltaTime()));
	}

	public override void _Input(InputEvent @event)
	{
		Vector3 pos;
		if (@event is InputEventMouseButton eventMouseButton)
		{
			// Check the position of the click relative to the viewport size
			Vector2 viewportSize = GetViewport().GetVisibleRect().Size;
			if (eventMouseButton.Position.X > viewportSize.X / 2)
			{
				pos = (Position - new Vector3((float) (MoveSpeed * GetProcessDeltaTime()), 0, 0));
				// Move camera to the right
				if (pos.X < -Limit)
				{
					Position = new Vector3(-Limit,0,0);
				}else{
					Position -= new Vector3((float) (MoveSpeed * GetProcessDeltaTime()), 0, 0);
				}
				
				
			}
			else
			{
				pos = (Position + new Vector3((float) (MoveSpeed * GetProcessDeltaTime()), 0, 0));
				// Move camera to the right
				if (pos.X > Limit)
				{
					Position = new Vector3(Limit,0,0);
				}else{
					Position += new Vector3((float) (MoveSpeed * GetProcessDeltaTime()), 0, 0);
				}
			}
		}
	}
}

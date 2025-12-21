using Godot;
using System;

public partial class Stage : Node2D
{
	public void DelegateInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventButton)
		{
			if (eventButton.Pressed)
			{
	   			var spaceState = GetWorld2D().DirectSpaceState;
				var query = new PhysicsPointQueryParameters2D();
				query.Position = GetGlobalMousePosition();
				query.CollideWithAreas = true;
				
				var result = spaceState.IntersectPoint(query, 1);
				
				if (result.Count > 0)
				{
					Area2D collider = (Area2D)result[0]["collider"];
						
					GD.Print(collider.IsInGroup("placeholder"));
				}
			}
		}
	}
}

using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Map
{
	public partial class MapWidget : Node2D
	{
		public bool Locked { get; set; } = false;
		public bool Dragging { get; set; } = false;
		
		private Map.Layers.GroundLayer groundLayer;
		private Camera camera;
		
		public void CenterAndAlign()
		{
			Rect2 usedRect = groundLayer.GetUsedRect();
			camera.SetLimits(usedRect, groundLayer.TileSize);
			camera.Position = usedRect.Position + usedRect.GetCenter() * 16;
		}
		
		private void Move(Vector2 amount)
		{
			Rect2 usedRect = groundLayer.GetUsedRect();
			Vector2 newPosition = (camera.Position - (amount / camera.Zoom));
			Vector2 clampedPosition = newPosition.Clamp(
				usedRect.Position * groundLayer.TileSize,
				(usedRect.Position + usedRect.Size) * groundLayer.TileSize
			);
			camera.Position = clampedPosition;
			GD.Print(clampedPosition);
			GD.Print(usedRect.Position * groundLayer.TileSize);
			GD.Print((usedRect.Position + usedRect.Size) * groundLayer.TileSize);
		}
		
		private void Zoom(Vector2 amount)
		{
			Vector2 zoom = camera.Zoom + amount;
			camera.Zoom = zoom.Clamp(new Vector2(.25f, .25f), new Vector2(2f, 2f));
		}
		
		public override void _Ready()
		{
			groundLayer = GetNode<TileMapLayer>("GroundLayer") as Map.Layers.GroundLayer;
			camera = GetNode<Camera2D>("Camera") as Camera;
			CenterAndAlign();
		}
		
		public override void _UnhandledInput(InputEvent @event)
		{
			if (!Locked)
			{
				if (@event is InputEventMouse mouseEvent)
				{
					if (mouseEvent is InputEventMouseButton mouseClick)
					{	
						if (mouseClick.Pressed)
						{
							switch (mouseClick.ButtonIndex)
							{
								case (MouseButton)4: Zoom(new Vector2(0.25f, 0.25f)); break;
								case (MouseButton)5: Zoom(new Vector2(-0.25f, -0.25f)); break;
							}
						}
					}
				
					if (mouseEvent is InputEventMouseMotion mouseMotion)
					{
						if (Dragging)
						{
							Move(mouseMotion.Relative);
						}
					}
				}
			}
		}
	}
}

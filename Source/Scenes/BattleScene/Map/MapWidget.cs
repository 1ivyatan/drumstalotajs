using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Map
{
	public partial class MapWidget : Node2D
	{
		public bool Locked { get; set; } = false;
		public bool Dragging { get; set; } = false;
		
		private Map.Layers.GroundLayer groundLayer;
		private Camera2D camera;
		
		public void Center()
		{
			Rect2 usedRect = groundLayer.GetUsedRect();
			GD.Print(usedRect.GetCenter() * 16);
			camera.Position = usedRect.Position + usedRect.GetCenter() * 16;
		}
		
		private void Move(Vector2 amount)
		{
			camera.Position -= amount;
		}
		
		private void Zoom(Vector2 amount)
		{
			Vector2 zoom = camera.Zoom + amount;
			camera.Zoom = zoom.Clamp(new Vector2(1f, 1f), new Vector2(2f, 2f));
		}
		
		public override void _Ready()
		{
			groundLayer = GetNode<TileMapLayer>("GroundLayer") as Map.Layers.GroundLayer;
			camera = GetNode<Camera2D>("Camera");
			Center();
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

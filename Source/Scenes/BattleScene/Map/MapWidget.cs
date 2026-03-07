using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Map
{
	public partial class MapWidget : Node2D
	{
		public bool Dragging = false;
		private Camera2D camera;
		private bool dragging = false;
		
		private void Zoom(Vector2 amount)
		{
			Vector2 zoom = camera.Zoom + amount;
			camera.Zoom = zoom.Clamp(new Vector2(1f, 1f), new Vector2(2f, 2f));
		}
		
		public override void _Ready()
		{
			camera = GetNode<Camera2D>("Camera");
		}
		
		public override void _Input(InputEvent @event)
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
						GD.Print(mouseMotion.Relative);
					}
				}
			}
		}
	}
}

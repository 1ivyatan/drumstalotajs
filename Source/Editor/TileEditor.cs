using Godot;
using System;

namespace Drumstalotajs.Editor
{
	public partial class TileEditor : Node2D
	{
		private TileMapLayer _groundLayer;
		private TileMapLayer _decorationLayer;
		
		public override void _Ready()
		{
			_groundLayer = GetNode<TileMapLayer>("GroundLayer");
			_decorationLayer = GetNode<TileMapLayer>("DecorationLayer");
		}
		
		public override void _Input(InputEvent @event)
		{
			if (@event is InputEventKey eventKey)
			{
				if (eventKey.Pressed && eventKey.Keycode == Key.Escape)
				{
					
				}
			}
		}
	}
}

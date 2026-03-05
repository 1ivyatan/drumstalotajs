using Godot;
using System;

namespace Drumstalotajs.Editor
{
	public partial class TileEditor : Node2D
	{
		private TileMapLayer _groundLayer;
		private TileMapLayer _decorationLayer;
		private readonly Shortcut _saveShortcut = new Shortcut();
		
		public override void _Ready()
		{
			_groundLayer = GetNode<TileMapLayer>("GroundLayer");
			_decorationLayer = GetNode<TileMapLayer>("DecorationLayer");
			
			InputEventKey keyEvent = new InputEventKey
			{
				Keycode = Key.S,
				CtrlPressed = true,
				CommandOrControlAutoremap = true
			};
			_saveShortcut.Events = [keyEvent];
		}
		
		public override void _Input(InputEvent @event)
		{
			if (@event is InputEventKey eventKey)
			{
				if (_saveShortcut.MatchesEvent(@event) &&
					eventKey.Pressed && !eventKey.Echo
				) {
					GD.Print("save");
					
					
				}
			}
		}
	}
}

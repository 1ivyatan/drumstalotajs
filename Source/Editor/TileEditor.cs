using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

namespace Drumstalotajs.Editor
{
	public partial class TileEditor : Node2D
	{
		private TileMapLayer _groundLayer;
		private TileMapLayer _decorationLayer;
		private readonly Shortcut _saveShortcut = new Shortcut();
		private bool _saving { get; set; }
		
		private void SavePatterns()
		{
			if (!_saving)
			{
				SavePattern(_groundLayer);
				SavePattern(_decorationLayer);
				GD.Print("should be saved, go to exports directory");
				
				_saving = true;
				SceneTreeTimer delayToSaveAgain = GetTree().CreateTimer(5f);
					delayToSaveAgain.Connect("timeout", Callable.From(() => {
					_saving = false;
				}));
			}
		}
		
		private void SavePattern(TileMapLayer layer)
		{
			Array<Vector2I> cells = layer.GetUsedCells();
			TileMapPattern pattern = layer.GetPattern(cells);
			ResourceSaver.Save(pattern, $"res://Exports/{layer.Name}.tres");
		}
		
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
					SavePatterns();
				}
			}
		}
	}
}

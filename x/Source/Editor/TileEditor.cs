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
		private TileMapLayer _entityLayer;
		private readonly Shortcut _saveShortcut = new Shortcut();
		private bool _saving { get; set; }
		
		private void SavePatterns()
		{
			if (!_saving)
			{
				SavePattern(_groundLayer);
				SavePattern(_decorationLayer);
				SavePattern(_entityLayer);
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
			Rect2I usedRect = layer.GetUsedRect();
			Array<Vector2I> allCells = new Array<Vector2I>();
			
			for (int y = usedRect.Position.Y; y < usedRect.Position.Y + usedRect.Size.Y; y++)
			{
				for (int x = usedRect.Position.X; x < usedRect.Position.X + usedRect.Size.X; x++)
				{
					allCells.Add(new Vector2I(x, y));
				}
			}
			
			TileMapPattern tiles = layer.GetPattern(allCells);
			Resources.Levels.Pattern pattern = new Resources.Levels.Pattern();
			pattern.Tiles = tiles;
			pattern.Offset = usedRect.Position;
			
			ResourceSaver.Save(pattern, $"res://Exports/{layer.Name}Pattern.tres");
		}
		
		public override void _Ready()
		{
			_groundLayer = GetNode<TileMapLayer>("GroundLayer");
			_decorationLayer = GetNode<TileMapLayer>("DecorationLayer");
			_entityLayer = GetNode<TileMapLayer>("EntityLayer");
			
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

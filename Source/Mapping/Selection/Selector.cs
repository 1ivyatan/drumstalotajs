using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	[Export] private Map _map;
	public SelectorMode Mode { get; set; } = SelectorMode.Invisible;
	public SelectorFilter Filter { get; set; } = SelectorFilter.Empty;
	private Vector2I _currentPos;
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion)
		{
			Vector2I newCellPos = _map.ViewportToMap();
			if (_currentPos != newCellPos)
			{
				_currentPos = newCellPos;
				switch (Mode)
				{
					case SelectorMode.Interactable:
						if (GetTiles(newCellPos).Count > 0)
						{
							HighlightAt(newCellPos);
						}
						break;
					case SelectorMode.Editing:
						HighlightAt(newCellPos);
						break;
					default:
						Visible = false;
						break;
				}
			}
		}
	}
	
	public FilteredTiles GetTiles(Vector2I position)
	{
		FilteredTiles tiles = new FilteredTiles();
		
		if (_map.GroundLayer.GetCellAtlasCoords(position) != Constants.Vector2I.Negative)
		{
			tiles = Filter.GetTiles(position);
		}
		
		return tiles;
	}
	
	private void HighlightAt(Vector2I cellPosition)
	{
		var tileSize = _map.GroundLayer.TileSize;
		SetPosition((cellPosition * tileSize) + new Vector2I(tileSize / 2, tileSize / 2));
		Visible = true;
	}
}

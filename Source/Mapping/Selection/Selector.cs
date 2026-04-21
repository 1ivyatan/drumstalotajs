using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	[Export] private Map _map;
	public SelectorMode Mode { get; set; } = SelectorMode.Invisible;
	public SelectorFilter Filter { get; set; } = SelectorFilter.Empty;
	private Vector2I _currentPos;
	
	public override void _UnhandledInput(InputEvent @event)
	{/*
		if (@event is InputEventMouseMotion mouseMotion)
		{
			switch (Mode)
			{
				case SelectorMode.Visible:
					Vector2I newCellPos = _map.GetCellPosFromMouse();
					if (_currentPos != newCellPos)
					{
						if (GetTiles(newCellPos).Count > 0)
						{
							HighlightAt(newCellPos);
						}
						_currentPos = newCellPos;
					}
					break;
				default: 
					Visible = false;
					break;
			}
		}*/
	}
	
	private void HighlightAt(Vector2I cellPosition)
	{
		var tileSize = _map.GroundLayer.TileSize;
		SetPosition((cellPosition * tileSize) + new Vector2I(tileSize / 2, tileSize / 2));
		Visible = true;
	}
	/*
	public FilteredTiles GetTiles(Vector2I position)
	{
		FilteredTiles tiles = new FilteredTiles();
		
		if (_map.GroundLayer.GetCellAtlasCoords(position) != Types.Vector2I.Negative)
		{
			tiles = Filter.GetTiles(position);
		}
		
		return tiles;
	}*/
}

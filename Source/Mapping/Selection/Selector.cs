using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	[Export] private Map _map;
	private Vector2I _currentPos;
	public SelectorFilter Filter { get; set; } = SelectorFilter.Empty;
	public SelectorMode Mode { get;
		set {
			field = value;
			switch (field)
			{
				case SelectorMode.Locked:
					State = SelectorState.Locked; 
					RemoveHighlight();
					break;
				default: State = SelectorState.Intedeterminate; break;
			}
		}
	} = SelectorMode.Locked;
	public SelectorState State { get; private set; } = SelectorState.Locked;
	
	public override void _Input(InputEvent @event)
	{
		if (Mode == SelectorMode.Locked) return;

		if (@event is InputEventMouseMotion)
		{
			var touchingControl = GetViewport().GuiGetHoveredControl();
			if (touchingControl != null)
			{
				State = SelectorState.Outside;
				RemoveHighlight();
			} else
			{
				State = SelectorState.Inside;
			}
		}
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (Mode == SelectorMode.Locked || 
		State != SelectorState.Inside) return;

		if (@event is InputEventMouseMotion)
		{
			Vector2I newCellPos = _map.ViewportMouseToMap();
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
						RemoveHighlight();
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
	
	private void RemoveHighlight()
	{
		Visible = false;
	}
}

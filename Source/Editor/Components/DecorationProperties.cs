using Godot;
using System;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Editor.Components;

public partial class DecorationProperties : TileProperties
{
	[Export] private Map _map;
	private DecorationTile _decorationTile = null;
	
	public override void Load(Tile tile)
	{
		if (tile is DecorationTile decorationTile)
		{
			_decorationTile = decorationTile;
			Visible = true;
		} else
		{
			Close();
		}
	}
	
	public override void Close()
	{
		Visible = false;
		_decorationTile = null;
	}
}

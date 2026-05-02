using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Tiles;

public partial class LevelMarker : OverlayTile
{
	private Sprite2D _sprite;

	public override Dictionary Data { get; 
		set {
			field = value;
			GD.Print(field);
		}
	} = new();
}

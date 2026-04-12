using Godot;
using System;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Mapping.Layers;

public partial class GroundLayer : AtlasLayer
{
	public override Godot.Collections.Array<AtlasTile> Flash(Vector2I position)
	{
		if (GetCellAtlasCoords(position) == Types.Vector2I.Negative) return [];
		
		return [ new GroundTile(this, position) ];
	}
}

using Godot;
using System;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Mapping.Layers;

public partial class DecorationLayer : AtlasLayer
{
	public override Godot.Collections.Array<AtlasTile> Flash(Vector2I position)
	{
		if (GetCellAtlasCoords(position) == Types.Vector2I.Negative) return [];
		return [ new DecorationTile(this, position) ];
	}
	
	public override DecorationLayerData Export()
	{
		return new DecorationLayerData(this);
	}
	
	public override void Load(AtlasLayerData layerData)
	{
		if (layerData is DecorationLayerData decorationLayerData)
		{
			Clear();
			SetPattern(decorationLayerData.Offset, decorationLayerData.Tiles);
		}
	}
}

using Godot;
using System;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Mapping.Layers;

public partial class DecorationTile : AtlasTile
{
	public DecorationTile(DecorationLayer layer, Vector2I position) : base(layer, position)
	{
	}
}

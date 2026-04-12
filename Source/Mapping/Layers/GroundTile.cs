using Godot;
using System;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Mapping.Layers;

public partial class GroundTile : AtlasTile
{	
	public GroundTile(GroundLayer layer, Vector2I position) : base(layer, position)
	{
		
	}
}

using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	private bool AllowedTileFilter(Vector2I cellPos)
	{
		if (Readonly)
		{
			if (map.GroundLayer.GetCellAtlasCoords(cellPos) == Utilities.Topography.NegativeVector)
				return false;
			return true;
		} else
		{
			return true;
		}
	}
	
	private Entities.Entity[] AllowedEntityFilter(Vector2 localPos)
	{
		Entities.Entity[] flashedNodes = map.EntityLayer.Flash(localPos, 9);
		return flashedNodes;
	}
}

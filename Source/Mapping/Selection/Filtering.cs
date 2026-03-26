using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	public Filtering.Filter Filter;
	
	private bool AllowedGround(Vector2I cellPos)
	{
		bool retval = false;
		switch (Mode)
		{
			case SelectorMode.VIEW:
				if (Filter.Layer == Filtering.SelectionLayer.GROUND || Filter.Layer == Filtering.SelectionLayer.ALL) retval = true;
				break;
			case SelectorMode.EDIT:
				retval = true;
				break;
		}
		
		return retval;
	}
	
	private Entities.Entity[] AllowedEntities(Vector2 localPos)
	{
		Entities.Entity[] flashedEntites = map.EntityLayer.FlashEntities(localPos, 9);
		return Filter.FilterEntities(flashedEntites);
	}
}

using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	private bool AllowedGround(Vector2I cellPos)
	{
		bool retval = false;
		switch (Mode)
		{
			case SelectorMode.VIEW:
				retval = true;
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
		return flashedEntites;
	}
	
	private Entities.Entity[] AllowedEntityFilter(Vector2 localPos)
	{
		Entities.Entity[] flashedNodes = map.EntityLayer.Flash(localPos, 9);
		return flashedNodes;
	}
}

using Godot;
using System;
using System.Linq;

namespace drumstalotajs.Mapping.Selection.Filtering;

public struct Filter
{
	public SelectionLayer Layer { get; set; } = SelectionLayer.ALL;
	public int[] EntityIds { get; set; } = [];
	
	public Filter() {}
	
	public Filter(SelectionLayer layerFilter, int[] entityIdFilter)
	{
		Layer = layerFilter;
		EntityIds = entityIdFilter;
	}
	
	public bool HasEntityIds()
	{
		return EntityIds != null && EntityIds.Length > 0;
	}
	
	public Entities.Entity[] FilterEntities(drumstalotajs.Entities.Entity[] entities)
	{
		if (HasEntityIds())
		{
			/* CS1673 */
			int[] entityIds = this.EntityIds;
			return entities.Where( e => entityIds.Contains(e.EntityResource.Id) ).ToArray();	
		} else
		{
			return entities;
		} 
	}
}

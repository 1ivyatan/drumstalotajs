using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;

namespace Drumstalotajs.Mapping.Layers;

public partial class SceneLayer : Layer<string, SceneTile, SceneLayerData>
{
	public int InstanceCount(int id)
	{
		return Instances.Where(i => i.TileId == id).Count();
	}
	
	public void RemoveAllInstancesByName(string name)
	{
		var id = GetAtlasId(name);
		if (id != -1)
		{
			foreach (var instance in Instances)
			{
				if (instance.TileId == id)
				{
					RemoveTile(LocalToMap(instance.Position));
				}
			}
		}
	}
	
	public int GetAtlasId(string name)
	{
		var atlas = GetAtlasData(name);
		return atlas != null ? atlas.Id : -1;
	}
}

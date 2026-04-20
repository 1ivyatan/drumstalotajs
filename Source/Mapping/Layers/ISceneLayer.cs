using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Mapping.Layers;

public interface ISceneLayer<
	[MustBeVariant] TSceneTile
> where TSceneTile : SceneTile
{
	/*
	public Godot.Collections.Array<TSceneTile> Flash(Vector2I position)
	{
		Array<TSceneTile> tiles = new Array<TSceneTile>();
		var spaceState = GetWorld2D().DirectSpaceState;
		PhysicsPointQueryParameters2D query = new PhysicsPointQueryParameters2D();
		query.Position = GlobalPosition + position;
		query.CollideWithAreas = true;
		var intersectedNodes = spaceState.IntersectPoint(query, 9);
		if (intersectedNodes.Count > 0)
		{
			foreach (var node in intersectedNodes)
			{
				Node2D collider = (Node2D)node["collider"];
				if (collider is TSceneTile tile)
				{
					tiles.Add(tile);
				}
			}
			
			if (tiles.Count > 0)
			{
				tiles.OrderBy(tile => {
					return ((Vector2)position).DistanceTo(tile.Position);
				});
			}
		}
		return tiles;
	}
	
	public override SceneLayerData Export()
	{
		return null;
	}
	
	public override void Load(SceneLayerData layerData)
	{
		
	}
	
	*/
}

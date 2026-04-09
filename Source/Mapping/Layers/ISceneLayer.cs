using Godot;
using System;
using System.Linq;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Mapping.Layers;

public interface ISceneLayer<[MustBeVariant] T> where T : SceneTile
{
	public Godot.Collections.Array<T> Flash(Vector2 localPosition, int limit)
	{
		Layer layer = this as Layer;
		Godot.Collections.Array<T> tiles = new Godot.Collections.Array<T>(); 
		var spaceState = layer.GetWorld2D().DirectSpaceState;
		PhysicsPointQueryParameters2D query = new PhysicsPointQueryParameters2D();
		query.Position = layer.GlobalPosition + localPosition;
		query.CollideWithAreas = true;
		var intersectedNodes = spaceState.IntersectPoint(query, limit);
		
		if (intersectedNodes.Count > 0)
		{
			foreach (var node in intersectedNodes)
			{
				Node2D collider = (Node2D)node["collider"];
				if (collider is T tile)
				{
					tiles.Add(tile);
				}
			}
			
			if (tiles.Count > 0)
			{
				tiles.OrderBy(tile => {
					return localPosition.DistanceTo(tile.Position);
				});
			}
		}
		
		return tiles;
	}
}

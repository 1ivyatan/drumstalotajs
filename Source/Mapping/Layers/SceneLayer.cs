using Godot;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Mapping.Layers;

public partial class SceneLayer : Layer
{
	[Signal] public delegate void SpawnedTileEventHandler(SceneTile tile);
	
	public List<SceneTile> Instances { get; private set; }
	private Resources.Layers.SceneLayer _sceneLayerSet;
	
	public override void _Ready()
	{
		_sceneLayerSet = TileSet as Resources.Layers.SceneLayer;
		_sceneLayerSet.PrepareAtlas();
		Instances = new List<SceneTile>();
		ChildEnteredTree += (Node node) => {
			if (node is SceneTile tile)
			{
				Instances.Add(tile);
				EmitSignal(SignalName.SpawnedTile, tile);
			}
		};
		ChildExitingTree += (Node node) => {
			if (node is SceneTile tile)
			{
				Instances.Remove(tile);
			}
		};
	}

	public async Task<SceneTile> AddTile(string name, Vector2I position)
	{
		int id = _sceneLayerSet.SceneTiles.FirstOrDefault(s => s.Name == name).Id;
		SetCell(position, 0, new Vector2I(0, 0), id);
		var data = await ToSignal(this, SignalName.SpawnedTile);
		return (SceneTile)data[0];
	}
	
	public Resources.Mapping.SceneTile[] GetSceneTileAtlas()
	{
		return _sceneLayerSet.SceneTiles;
	}
	
	public SceneTile GetTile(Vector2 position)
	{
		return Instances.FirstOrDefault(i => position == LocalToMap(position));
	}
	
	public Godot.Collections.Array<SceneTile> Flash(Vector2 localPosition, int limit)
	{
		Godot.Collections.Array<SceneTile> tiles = new Godot.Collections.Array<SceneTile>(); 
		var spaceState = GetWorld2D().DirectSpaceState;
		PhysicsPointQueryParameters2D query = new PhysicsPointQueryParameters2D();
		query.Position = GlobalPosition + localPosition;
		query.CollideWithAreas = true;
		var intersectedNodes = spaceState.IntersectPoint(query, limit);
		if (intersectedNodes.Count > 0)
		{
			foreach (var node in intersectedNodes)
			{
				Node2D collider = (Node2D)node["collider"];
				if (collider is SceneTile tile)
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

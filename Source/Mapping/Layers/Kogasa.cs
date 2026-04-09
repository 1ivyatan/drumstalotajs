using Godot;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Mapping.Layers;

public partial class Kogasa<[MustBeVariant]TSceneTile, [MustBeVariant]TSceneTileProps>
	: Layer where TSceneTile : SceneTile,
	  ILayer<TSceneTileProps> where TSceneTileProps : Resources.Mapping.SceneTileProps
{
	[Signal] public delegate void SpawnedTileEventHandler(SceneTile tile);
	[Signal] public delegate void DestroyedTileEventHandler(SceneTile tile);
	
	public List<TSceneTile> Instances { get; private set; }
	private Resources.Mapping.SceneLayer _sceneLayerSet;
	
	public override void _Ready()
	{
		_sceneLayerSet = TileSet as Resources.Mapping.SceneLayer;
		_sceneLayerSet.PrepareAtlas();
		Instances = new List<TSceneTile>();
		ChildEnteredTree += (Node node) => {
			if (node is TSceneTile tile)
			{
				Instances.Add(tile);
				EmitSignal(SignalName.SpawnedTile, tile);
			}
		};
		ChildExitingTree += (Node node) => {
			if (node is TSceneTile tile)
			{
				Instances.Remove(tile);
				EmitSignal(SignalName.DestroyedTile, tile);
			}
		};
	}

	public async Task<TSceneTile> AddTile(Vector2I position, string name)
	{
		int id = _sceneLayerSet.SceneTiles.FirstOrDefault(s => s.Name == name).Id;
		SetCell(position, 0, new Vector2I(0, 0), id);
		var data = await ToSignal(this, SignalName.SpawnedTile);
		return (TSceneTile)data[0];
	}
	
	new public TSceneTileProps[] GetAtlas()
	{
		return (TSceneTileProps[])_sceneLayerSet.SceneTiles;
	}
	
	public TSceneTile GetTile(Vector2 position)
	{
		return Instances.FirstOrDefault(i => position == LocalToMap(position));
	}
	
	public Godot.Collections.Array<TSceneTile> Flash(Vector2 localPosition, int limit)
	{
		Godot.Collections.Array<TSceneTile> tiles = new Godot.Collections.Array<TSceneTile>(); 
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
				if (collider is TSceneTile tile)
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

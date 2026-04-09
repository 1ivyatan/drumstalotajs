using Godot;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Mapping.Layers;

public partial class SceneLayer : Layer, ILayer<Resources.Mapping.SceneTileProps>
{
	[Signal] public delegate void SpawnedTileEventHandler(SceneTile tile);
	[Signal] public delegate void DestroyedTileEventHandler(SceneTile tile);
	
	public List<SceneTile> Instances { get; private set; }
	private Resources.Mapping.SceneLayer _sceneLayerSet;

/*public ISceneLayer<OverlayTile> AsISceneLayer => (ISceneLayer<OverlayTile>)this;*/

	public override void _Ready()
	{
		_sceneLayerSet = TileSet as Resources.Mapping.SceneLayer;
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
				EmitSignal(SignalName.DestroyedTile, tile);
			}
		};
	}

	public async Task<SceneTile> AddTile(Vector2I position, string name)
	{
		int id = _sceneLayerSet.SceneTiles.FirstOrDefault(s => s.Name == name).Id;
		SetCell(position, 0, new Vector2I(0, 0), id);
		var data = await ToSignal(this, SignalName.SpawnedTile);
		return (SceneTile)data[0];
	}
	
	new public Resources.Mapping.SceneTileProps[] GetAtlas()
	{
		return _sceneLayerSet.SceneTiles;
	}
	
	public Resources.Mapping.SceneTileProps[] GetSceneTileAtlas()
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

using Godot;
using System;
using System.Collections.Generic;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Mapping.Layers;

public partial class SceneLayer : Layer<SceneTileData, SceneTile, SceneLayerData>
{
	[Export] public SceneTileData[] Atlas { get; private set; }
	public List<SceneTile> Instances { get; private set; }
	
	public override void _Ready()
	{
		Instances = new List<SceneTile>();
		foreach (var tile in Atlas)
		{
			
		}
	}
	
	public override SceneTileData[] GetAtlas()
	{
		return Atlas;
	}
	
	public override void AddTile(Vector2I position, SceneTileData atlas)
	{
		
	}
	
	public void AddTile(Vector2I position, string atlasName)
	{
		
	}
	
	public override void RemoveTile(Vector2I position)
	{
		
	}
	
	public override Godot.Collections.Array<SceneTile> Flash(Vector2I position)
	{
		return null;
	}
	
	public override SceneLayerData Export()
	{
		return null;
	}
	
	public override void Load(SceneLayerData layerData)
	{
		
	}
}

using Godot;
using System;


namespace Drumstalotajs.Battle.Map
{	
	public partial class Widget : Node2D
	{
		public float TileScale { get; private set; }
	
		private TileMapLayer _groundTileLayer;
		private TileMapLayer _decorationLayer;
		private TileMapLayer _entityLayer;
		
		public void LoadIntoLayer(TileMapLayer layer, string path)
		{
			if (ResourceLoader.Exists(path))
			{
				Resources.Levels.Pattern pattern = GD.Load<Resources.Levels.Pattern>(path);
				
				if (pattern != null)
				{
					layer.SetPattern(pattern.Offset, pattern.Tiles);
				}
			}
		}
	
		public void LoadLayers(string groundLayerPath, string decorationLayerPath, string entityLayerPath)
		{
			LoadIntoLayer(_groundTileLayer, groundLayerPath);
			LoadIntoLayer(_decorationLayer, decorationLayerPath);
		//	LoadIntoLayer(_entityLayer, entityLayerPath);
			/*(ResourceLoader.Exists(levelResourcePath))
			{
				Level = ResourceLoader.Load<Resources.Levels.Level>(levelResourcePath);*/
		}
	
		public override void _Ready()
		{
			_groundTileLayer = GetNode<TileMapLayer>("GroundLayer2");
			_decorationLayer = GetNode<TileMapLayer>("DecorationLayer");
			_entityLayer = GetNode<TileMapLayer>("EntityLayer");
		}
	}
}

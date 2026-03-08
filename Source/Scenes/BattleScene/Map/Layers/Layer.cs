using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Map.Layers
{
	public partial class Layer : TileMapLayer
	{
		public int TileSize { get => TileSet.TileSize.X; }
		
		public void LoadLayer(string path)
		{
			if (ResourceLoader.Exists(path))
			{
				Resources.Levels.Pattern pattern = GD.Load<Resources.Levels.Pattern>(path);
				
				if (pattern != null)
				{
					SetPattern(pattern.Offset, pattern.Tiles);
				}
			}
		}
	}
}

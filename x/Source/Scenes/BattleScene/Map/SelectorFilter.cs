using Godot;
using System;


namespace Drumstalotajs.Scenes.BattleScene.Map
{
	public partial class Selector : Node2D
	{
		public struct SelectorFilter
		{	
			public int[] TileIds { get; }
			public Map.Layers.LayerType Layer;
			
			public bool Allowed(int tileId)
			{
				return true;
			}
			
			public SelectorFilter(int[] tileIds, Map.Layers.LayerType layer)
			{
				TileIds = tileIds;
			}
		}
	}
}

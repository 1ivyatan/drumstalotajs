using Godot;
using System;


namespace Drumstalotajs.Battle.Map
{	
	public partial class Widget : Node2D
	{
		private float TileScale { get; set; }
	
		private TileMapLayer _groundTileLayer;
	
		public void LoadLevel(TileMapPattern groundPattern, TileMapPattern entityPattern)
		{
		
		}
	
		public override void _Ready()
		{
			_groundTileLayer = GetNode<TileMapLayer>("GroundLayer");
		}

		public override void _Process(double delta)
		{
		}
	}
}

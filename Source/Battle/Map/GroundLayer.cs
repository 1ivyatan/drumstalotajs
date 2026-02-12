using Godot;
using System;

namespace Drumstalotajs.Battle.Map
{
	public partial class GroundLayer : TileMapLayer
	{
		public double Height { get; private set; }
		
		public double GetHeight(Vector2I position)
		{
			return (double)GetCellTileData(position).GetCustomData("RelativeHeight") + Height;
		}
		
		public override void _Ready()
		{
			Height = ((GetNode<Control>("../../../") as Scene).Level.BaseHeight);
		}
	}
}

using Godot;
using System;

namespace Drumstalotajs.Battle.Map.Layers
{
	public partial class GroundLayer : Layer
	{
		public double Height { get; private set; }
		
		public double GetHeight(Vector2I position)
		{
			if (GetCellAtlasCoords(position).Equals(new Vector2I(-1, -1))) return -1.0;
			return (double)GetCellTileData(position).GetCustomData("RelativeHeight") + Height;
		}
		
		public double GetHeight(Vector2 position)
		{
			return GetHeight(GetCellPos(position));
		}
		
		public override void _Ready()
		{
			Height = ((GetNode<Control>("../../../") as Scene).Level.BaseHeight);
		}
	}
}

using Godot;
using System;

namespace Drumstalotajs.Battle.Map.Layers
{
	public partial class Layer : TileMapLayer
	{
		public Vector2I GetCellPos(Vector2 position)
		{
			return LocalToMap(ToLocal(position));
		}
	}
}

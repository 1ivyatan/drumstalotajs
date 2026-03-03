using Godot;
using System;

namespace Drumstalotajs.Levels
{
	public partial class LevelMarker : Control
	{
		public void SetMarker(Resources.Levels.LevelProps levelProps)
		{
			Position = levelProps.Position;
			GD.Print(levelProps.Position);
		}
	}
}

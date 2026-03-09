using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Map
{
	public partial class Camera : Camera2D
	{
		public void SetLimits(Rect2 usedRect, int tileSize)
		{
			LimitLeft = (int)(usedRect.Position.X * tileSize);
			LimitRight = (int)((usedRect.Position.X + usedRect.Size.X) * tileSize);
			LimitTop = (int)(usedRect.Position.Y * tileSize);
			LimitBottom = (int)((usedRect.Size.Y * tileSize) + (usedRect.Position.Y * tileSize));
		}
	}
}

using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.UI
{
	public partial class UIOverlay : CanvasLayer
	{
		private PauseMenu pauseMenu;
		
		public override void _Ready()
		{
			pauseMenu = GetNode<Control>("PauseMenu") as PauseMenu;
		}
	}
}

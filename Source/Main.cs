using Godot;
using System;

namespace Drumstalotajs
{
	public partial class Main : Node
	{
		private Managers.SceneManager SceneManager { get; set; }
		
		public override void _Ready()
		{
			SceneManager = GetNode<Node>("SceneManager") as Managers.SceneManager;

			SceneManager?.Start();
		}
	}
}

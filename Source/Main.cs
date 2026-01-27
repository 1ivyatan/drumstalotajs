using Godot;
using System;

namespace Drumstalotajs
{
	public partial class Main : Node
	{
		public Managers.SceneManager SceneManager { get; private set; }
		
		public override void _Ready()
		{
			this.SceneManager = GetNode<Node>("SceneManager") as Managers.SceneManager;
			this.SceneManager.Start();
		}
	}
}

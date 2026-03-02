using Godot;
using System;

namespace Drumstalotajs.Levels
{
	public partial class Scene : Node
	{
		private Button _toLevelButton { get; set; }
		
		public override void _Ready()
		{
			_toLevelButton = GetNode<Button>("ToBattleButton");
			_toLevelButton.Connect("pressed", Callable.From(() => {
				(GetNode("..") as Managers.SceneManager).Battle("1");
			}));
		}
	}
}

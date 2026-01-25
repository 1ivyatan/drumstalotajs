using Godot;
using System;

namespace Drumstalotajs.Start
{
	public partial class Scene : Node
	{
		private Button _toLevelButton { get; set; }
		
		public override void _Ready()
		{
			_toLevelButton = GetNode<Button>("ToLevelButton");
			_toLevelButton.Connect("pressed", Callable.From(() => {
				(GetNode("..") as Managers.SceneManager).LoadScene("Battle");
			}));
		}
	}
}

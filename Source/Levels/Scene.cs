using Godot;
using System;

namespace Drumstalotajs.Levels
{
	public partial class Scene : Node
	{
		private Button _toLevelButton { get; set; }
		private Button _toStartButton { get; set; }
		
		private void LoadList()
		{
			
		}
		
		public override void _Ready()
		{
			_toLevelButton = GetNode<Button>("ToBattleButton");
			_toStartButton = GetNode<Button>("ToStartButton");
			
			LoadList();
			
			_toLevelButton.Connect("pressed", Callable.From(() => {
				(GetNode("..") as Managers.SceneManager).Battle("1");
			}));
			
			_toStartButton.Connect("pressed", Callable.From(() => {
				(GetNode("..") as Managers.SceneManager).Start();
			}));
		}
	}
}

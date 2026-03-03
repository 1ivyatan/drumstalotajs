using Godot;
using System;

namespace Drumstalotajs.Levels
{
	public partial class Scene : Node
	{
		[Export] private Resources.Levels.LevelPack LevelPack { get; set; }
		
		private LevelContainer _levelContainer;
		private Button _toLevelButton;
		private Button _toStartButton;
		
		private void LoadList()
		{
			_levelContainer.LoadLevels(LevelPack);
		}
		
		public override void _Ready()
		{
			_toLevelButton = GetNode<Button>("ToBattleButton");
			_toStartButton = GetNode<Button>("ToStartButton");
			_levelContainer = GetNode<Control>("LevelContainer") as LevelContainer;
			
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

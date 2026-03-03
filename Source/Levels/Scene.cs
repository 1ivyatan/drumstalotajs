using Godot;
using System;

namespace Drumstalotajs.Levels
{
	public partial class Scene : Node
	{
		[Export] private Resources.Levels.LevelPack LevelPack { get; set; }
		
		private Resources.Levels.LevelProps SelectedLevel;
		
		private LevelMarkerContainer _levelMarkerContainer;
		private Button _toLevelButton;
		private Button _toStartButton;
		
		private void SelectLevel(Resources.Levels.LevelProps levelProps)
		{
			SelectedLevel = levelProps;
		}
		
		private void LoadList()
		{
			_levelMarkerContainer.LoadLevels(LevelPack);
			_levelMarkerContainer.SelectedLevel += SelectLevel;
		}
		
		public override void _Ready()
		{
			_toLevelButton = GetNode<Button>("InfoPanel/VBoxContainer/ToBattleButton");
			_toStartButton = GetNode<Button>("Map/ToStartButton");
			_levelMarkerContainer = GetNode<Control>("Map/LevelContainer/LevelMarkerContainer") as LevelMarkerContainer;
			
			LoadList();
			
			_toLevelButton.Pressed += () => (GetNode("..") as Managers.SceneManager).Battle("1");
			_toStartButton.Pressed += () => (GetNode("..") as Managers.SceneManager).Start();
		}
	}
}

using Godot;
using System;

namespace Drumstalotajs.Levels
{
	public partial class Scene : Node
	{
		[Export] private Resources.Levels.LevelPack LevelPack { get; set; }
		
		private Resources.Levels.LevelProps SelectedLevel;
		
		private LevelMarkerContainer _levelMarkerContainer;
		private InfoContainer _infoContainer;
		private Button _toStartButton;
		private Button _toBattleButton;
		
		private void StartBattle()
		{
			GD.Print(123);
			//if (SelectedLevel.Unlocked)
			//{
		//		_toBattleButton.Pressed += () => (GetNode("..") as Managers.SceneManager).Battle(SelectedLevel.Level);
		//	}
		}
		
		private void SelectLevel(Resources.Levels.LevelProps levelProps)
		{
			SelectedLevel = levelProps;
			_infoContainer.SetInfo(levelProps);
			_toBattleButton.Disabled = !levelProps.Unlocked;
		}
		
		private void LoadList()
		{
			_levelMarkerContainer.LoadLevels(LevelPack);
			_levelMarkerContainer.SelectedLevel += SelectLevel;
		}
		
		public override void _Ready()
		{
			_toStartButton = GetNode<Button>("Map/ToStartButton");
			_toBattleButton = GetNode<Button>("InfoPanel/InfoContainer/ToBattleButton");
			_levelMarkerContainer = GetNode<Control>("Map/LevelContainer/LevelMarkerContainer") as LevelMarkerContainer;
			_infoContainer = GetNode<Control>("InfoPanel/InfoContainer") as InfoContainer;
			
			LoadList();
			
			_toStartButton.Pressed += () => (GetNode("..") as Managers.SceneManager).Start();
			_toBattleButton.Pressed += StartBattle;
		}
	}
}

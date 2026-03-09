using Godot;
using System;

namespace Drumstalotajs.Levels
{
	public partial class LevelMarkerContainer : Control
	{
		[Signal] public delegate void SelectedLevelEventHandler(Resources.Levels.LevelProps levelProps);
		
		private void SpawnLevelButton(Resources.Levels.LevelProps levelProps)
		{
			LevelMarker levelMarker = ResourceLoader.Load<PackedScene>("res://Scenes/Levels/LevelMarker.tscn").Instantiate() as LevelMarker;
			levelMarker.SetMarker(levelProps);
			
			levelMarker.Connect("Selected", Callable.From((Resources.Levels.LevelProps levelProps) => {
				EmitSignal(SignalName.SelectedLevel, levelProps);
			}));
			
			AddChild(levelMarker);
		}
		
		public void LoadLevels(Resources.Levels.LevelPack levelPack)
		{
			foreach (Resources.Levels.LevelProps levelProps in levelPack.Levels)
			{
				SpawnLevelButton(levelProps);
			}
		}
	}
}

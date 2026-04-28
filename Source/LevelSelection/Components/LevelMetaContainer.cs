using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.LevelSelection.Components;

public partial class LevelMetaContainer : Control
{
	[Export] private Button _loadCustomLevel;
	
	public override void _Ready()
	{
		Utilities.Editor.EditorControl(_loadCustomLevel);
		
		_loadCustomLevel.Pressed += () => {
			if (Utilities.Editor.IsEditor())
			{
				
			}
		};
	}
}

using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Resources.Levels;

namespace Drumstalotajs.Managers.Saves;

public partial class SaveManager : Node
{
	[Export] public string SavePath { get; private set; } = "";
	[Export] public LevelSet[] LevelSets { get; private set; } = [];
	
	public override void _EnterTree()
	{
	}
	
	public LevelSet GetLevelSet(string name)
	{
		return LevelSets.FirstOrDefault(s => s.Name == name);
	}
}

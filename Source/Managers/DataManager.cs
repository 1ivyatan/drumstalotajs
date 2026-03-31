using Godot;
using System;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.Saves;
using Drumstalotajs.Resources.Sets.LevelSets;

namespace Drumstalotajs.Managers.Scenes;

public partial class DataManager : Node
{
	[Export] private LevelSet[] LevelSets { get; set; }
	[Export] private String SavePath { get; set; }
	
	public OrderedDictionary<LevelSet, Progress> LevelSetProgress { get; private set; }
	
	public override void _Ready()
	{
		LevelSetProgress = new OrderedDictionary<LevelSet, Progress>();
		foreach (LevelSet levelSet in LevelSets)
		{
			LevelSetProgress.Add(levelSet, Files.SafeLoadResource<Progress>($"{SavePath}/{levelSet.Name}.tres"));
		}
	}
}

using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Resources.Saves;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Managers.Saves;

public partial class SaveManager : Node
{
	[Export] private string SavePath { get; set; } = "user://save";
	[Export] private string KeyPath { get; set; } = "user://key";

	public SecureFile<Save> Save { get; private set; } = null;
	public Save SaveData => Save?.Data;

	[Export] public LevelSet[] LevelSets { get; private set; } = [];
	
	public override void _EnterTree()
	{
		Load();
	}
	
	/* levelset */
	public LevelSet GetLevelSet(string name)
	{
		return LevelSets.FirstOrDefault(s => s.Name == name);
	}
	
	public bool IsLevelUnlocked(LevelSet levelSet, int order)
	{
		//if (LevelSets.Scores.Count == 0) LevelSets[levelSet]
		if (SaveData.Scores.ContainsKey(levelSet))
		{
			
		} else
		{
			GD.Print(order);
			
		}
		return false;
	}
	
	private void Load()
	{
		Save = SecureFile<Save>.OpenOrCreate(SavePath, KeyPath);
		GD.Print($"SaveManager load state: {Save.LastLoadState}");
	}

	public void SaveProgress()
	{
		if (Save == null)
		{
			Load();
		}

		Save.Save();
	}
}

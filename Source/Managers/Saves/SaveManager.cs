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
	// this is the path that SaveManager looks for. In godot inspector,
	// user could write "user://save", "user://save.data" and so on
	[Export] private string SavePath { get; set; } = "user://save";
	// key file, user could write "user://key"
	[Export] private string KeyPath { get; set; } = "user://key";
	public SecureFile<Save> Save { get; private set; }

	[Export] public LevelSet[] LevelSets { get; private set; } = [];
	
	public override void _EnterTree()
	{
		Load();
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
	
	public LevelSet GetLevelSet(string name)
	{
		return LevelSets.FirstOrDefault(s => s.Name == name);
	}
}

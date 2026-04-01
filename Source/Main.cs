using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;
using Drumstalotajs.Managers.Scenes;
using Drumstalotajs.Managers.Data;

namespace Drumstalotajs;

public partial class Main : Node
{
	public SceneManager SceneManager { get; private set; }
	public DataManager DataManager { get; private set; }
	
	public override void _Ready()
	{
		DataManager = GetNode("DataManager") as DataManager;
		SceneManager = GetNode("SceneManager") as SceneManager;
		SceneManager.Start();
	}
}

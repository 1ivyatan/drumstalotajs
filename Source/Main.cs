using Godot;
using System;
using Drumstalotajs.Utils;
using Drumstalotajs.Managers;
using Drumstalotajs.Managers.Scenes;

namespace Drumstalotajs;

public partial class Main : Node
{
	public SceneManager SceneManager { get; private set; }
	public DataManager DataManager { get; private set; }
	
	public override void _Ready()
	{
		SceneManager = GetNode("SceneManager") as SceneManager;
		DataManager = GetNode("DataManager") as DataManager;
		SceneManager.Start();
	}
}

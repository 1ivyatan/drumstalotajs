using Godot;
using System;
using Drumstalotajs.Utilities;
using Drumstalotajs.Managers.Scenes;

namespace Drumstalotajs;

public partial class Main : Node
{
	public SceneManager SceneManager { get; private set; }
	
	public override void _Ready()
	{
		SceneManager = GetNode("SceneManager") as SceneManager;
		SceneManager.Start();
	}
}

using Godot;
using System;
using Drumstalotajs.Utilities;
using Drumstalotajs.Managers.Scenes;
using Drumstalotajs.Managers.Toasts;

namespace Drumstalotajs;

public partial class Main : Node
{
	[Export] public SceneManager SceneManager { get; private set; }
	[Export] public ToastManager ToastManager { get; private set; }
	
	public override void _Ready()
	{
		SceneManager.Start();
	}
}

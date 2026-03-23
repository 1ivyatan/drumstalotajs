using Godot;
using System;

namespace drumstalotajs;

public partial class Main : Node
{
	private Managers.SceneManager sceneManager;
	private Managers.CursorManager cursorManager;
	
	public override void _Ready()
	{
		sceneManager = GetNode<Node>("SceneManager") as Managers.SceneManager;
		cursorManager = GetNode<Node>("CursorManager") as Managers.CursorManager;
		sceneManager.SwitchedScene += (string name) => {
			cursorManager.ResetCursor();
		};
		sceneManager.StartScene();
	}
}

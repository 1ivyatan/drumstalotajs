using Godot;
using Godot.Collections;
using System;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Managers.Scenes;

public partial class SceneManager : Node
{
	[Export] private Dictionary<string, string> Scenes;
	public Node CurrentScene { get; private set; } = null;
	public SceneState State { get; private set; } = SceneState.RUNNING;
	
	public void PauseScene() { if (State != SceneState.LOADING) State = SceneState.RUNNING; }
	public void ResumeScene() { if (State != SceneState.LOADING) State = SceneState.RUNNING; }
	
	private Node LoadScene(string name)
	{
		PackedScene scene = Files.SafeLoadResource<PackedScene>(Scenes[name]);
		if (scene != null)
		{
			State = SceneState.LOADING;
			return scene.Instantiate();
		}
		return null;
	}
	
	private void SetScene(Node scene)
	{
		if (scene != null)
		{
			foreach (var node in GetChildren())
			{
				node.QueueFree();
				RemoveChild(node);
			}
			AddChild(scene);
			CurrentScene = scene;
		}
		State = SceneState.RUNNING;
	}
}

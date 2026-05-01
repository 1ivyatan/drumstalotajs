using Godot;
using Godot.Collections;
using System;
using Drumstalotajs.Utilities;
using System.Threading.Tasks;

namespace Drumstalotajs.Managers.Scenes;

public partial class SceneManager : Node
{
	[Export] private Dictionary<string, string> Scenes;
	[Export] private FadeCurtainContainer FadeCurtainContainer;
	public Node CurrentScene { get; private set; } = null;
	public SceneState State { get; private set; } = SceneState.Running;
	
	public void PauseScene() { if (State != SceneState.Loading) State = SceneState.Paused; }
	public void ResumeScene() { if (State != SceneState.Loading) State = SceneState.Loading; }
	
	private async Task<Node> LoadScene(string name)
	{
		await FadeCurtainContainer.FadeIn();
		PackedScene scene = Files.SafeLoadResource<PackedScene>(Scenes[name]);
		if (scene != null)
		{
			State = SceneState.Loading;
			return scene.Instantiate();
		} else
		{
			State = SceneState.Error;
			return null;
		}
	}
	
	private async Task SetScene(Node scene)
	{
		if (scene != null)
		{
			foreach (var node in GetChildren())
			{
				if (node is FadeCurtainContainer) continue;
				node.QueueFree();
				RemoveChild(node);
			}
			AddChild(scene);
			MoveChild(scene, 0);
			CurrentScene = scene;
			State = SceneState.Running;
			await FadeCurtainContainer.FadeOut();
		} else
		{
			State = SceneState.Error;
		}
	}
}

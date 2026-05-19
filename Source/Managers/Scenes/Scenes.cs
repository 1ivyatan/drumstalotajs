using Godot;
using System;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Battle;
using Drumstalotajs.Managers.Audio;

namespace Drumstalotajs.Managers.Scenes;

public partial class SceneManager : Node
{
	public async void Start()
	{
		Node scene = await LoadScene("Start");
		Nodes.GetRoot().AudioManager.SetAudioMode(AudioMode.Main);
		await SetScene(scene);
	}
	
	public async void LevelSelection()
	{
		Node scene = await LoadScene("LevelSelection");
		Nodes.GetRoot().AudioManager.SetAudioMode(AudioMode.Planning);
		await SetScene(scene);
	}
	
	public async void Editor()
	{
		Node scene = await LoadScene("Editor");
		await SetScene(scene);
	}
	
	public async void Battle(string mapPath)
	{
		BattleScene scene = await LoadScene("Battle") as BattleScene;
		Nodes.GetRoot().AudioManager.SetAudioMode(AudioMode.Battle);
		await scene.Open(mapPath);
		await SetScene(scene);
	}
	
	public async void Battle(LevelSet levelSet, LevelProps level)
	{
		BattleScene scene = await LoadScene("Battle") as BattleScene;
		Nodes.GetRoot().AudioManager.SetAudioMode(AudioMode.Battle, level.BgMusic);
		await scene.Open(levelSet, level);
		await SetScene(scene);
	}
}

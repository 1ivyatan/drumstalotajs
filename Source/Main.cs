using Godot;
using System;
using Drumstalotajs.Utilities;
using Drumstalotajs.Managers.Scenes;
using Drumstalotajs.Managers.Toasts;
using Drumstalotajs.Managers.Saves;
using Drumstalotajs.Managers.Audio;
using Drumstalotajs.Managers.Settings;

namespace Drumstalotajs;

public partial class Main : Node
{
	[Export] public SceneManager SceneManager { get; private set; }
	[Export] public ToastManager ToastManager { get; private set; }
	[Export] public SaveManager SaveManager { get; private set; }
	[Export] public AudioManager AudioManager { get; private set; }
	[Export] public SettingsManager SettingsManager { get; private set; }
	
	public override void _Ready()
	{
		SettingsManager.ChangedSettings += () => {
			AudioManager.SetMasterVolume(SettingsManager.MasterVolume);
			AudioManager.SetMusicVolume(SettingsManager.MusicVolume);
			AudioManager.SetSfxVolume(SettingsManager.SfxVolume);
		};
		SettingsManager.LoadSettings();
		SceneManager.Start();
	}
	
	public void Exit()
	{
		SettingsManager.CloseSettings();
		SettingsManager.SaveSettings();
		SaveManager.SaveProgress();
		GetTree().Quit();
	}
}

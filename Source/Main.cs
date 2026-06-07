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
	
	[Export] private Control _exitDialogContainer;
	[Export] private ConfirmationDialog _exitDialog;
	
	public override void _Ready()
	{
		SettingsManager.ChangedSettings += () => {
			AudioManager.SetMasterVolume(SettingsManager.MasterVolume);
			AudioManager.SetMusicVolume(SettingsManager.MusicVolume);
			AudioManager.SetSfxVolume(SettingsManager.SfxVolume);
			GetWindow().Mode = SettingsManager.MaxWindow
				? Window.ModeEnum.Maximized
				: Window.ModeEnum.Windowed;
		//	GD.Print(SettingsManager.MaxWindow);
		};
		_exitDialog.Canceled += () => {
			_exitDialog.Hide();
			_exitDialogContainer.Visible = false;
		};
		_exitDialog.CloseRequested += () => {
			_exitDialog.Hide();
			_exitDialogContainer.Visible = false;
		};
		_exitDialog.Confirmed += () => {
			_exitDialog.Hide();
			_exitDialogContainer.Visible = false;
			Exit();
		};
		DisplayServer.WindowSetMinSize(new Vector2I(640, 480));
		SettingsManager.LoadSettings();
		SceneManager.Start();
	}
	
	public override void _Notification(int what)
	{
		if (what == NotificationWMCloseRequest)
		{
			ExitPrompt();
		}
	}

	public void ExitPrompt()
	{
		_exitDialogContainer.Visible = true;
		_exitDialog.Popup();
	}
	
	public void Exit()
	{
		SettingsManager.CloseSettings();
		SettingsManager.SaveSettings();
		SaveManager.SaveProgress();
		GetTree().Quit();
	}
}

using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Managers.Settings;

namespace Drumstalotajs.Components.Settings;

public partial class SettingsWindow : Control
{
	[Signal] public delegate void CloseRequestedEventHandler();
	[Export] private Button _exitButton;
	
	[Export] private HSlider _masterVolumeSlider;
	[Export] private Label _masterVolumeValue;
	
	[Export] private HSlider _musicVolumeSlider;
	[Export] private Label _musicVolumeValue;
	
	[Export] private HSlider _sfxVolumeSlider;
	[Export] private Label _sfxVolumeValue;
	
	private SettingsManager _settingsManager;
	
	public override void _Ready()
	{
		_settingsManager = Nodes.GetRoot().SettingsManager;
		_settingsManager.ChangedSettings += () => {
			_masterVolumeSlider.Value = _settingsManager.MasterVolume;
			_masterVolumeValue.Text = $"{Math.Round(_settingsManager.MasterVolume * 100)}%";
			_musicVolumeSlider.Value = _settingsManager.MusicVolume;
			_musicVolumeValue.Text = $"{Math.Round(_settingsManager.MusicVolume * 100)}%";
			_sfxVolumeSlider.Value = _settingsManager.SfxVolume;
			_sfxVolumeValue.Text = $"{Math.Round(_settingsManager.SfxVolume * 100)}%";
			
		};
		_masterVolumeSlider.ValueChanged += (double value) => {_settingsManager.MasterVolume = value;};
		_musicVolumeSlider.ValueChanged += (double value) => {_settingsManager.MusicVolume = value;};
		_sfxVolumeSlider.ValueChanged += (double value) => {_settingsManager.SfxVolume = value;};
		_exitButton.Pressed += () => {
			_settingsManager.SaveSettings();
			EmitSignal(SignalName.CloseRequested);
		};
	}
}

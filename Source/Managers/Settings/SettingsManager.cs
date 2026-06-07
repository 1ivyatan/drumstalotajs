using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources;
using Drumstalotajs.Components.Settings;

namespace Drumstalotajs.Managers.Settings;

public partial class SettingsManager : Node
{
	[Signal] public delegate void ChangedSettingsEventHandler();
	[Export] private string _settingsPath;
	[Export] private SettingsWindowContainer _settingsWindowContainer;
	
	public double MasterVolume { get; set { field = value; EmitSignal(SignalName.ChangedSettings); } } = 1;
	public double MusicVolume { get; set { field = value; EmitSignal(SignalName.ChangedSettings); } } = 1;
	public double SfxVolume { get; set { field = value; EmitSignal(SignalName.ChangedSettings); } } = 1;
	public bool MaxWindow { get; set { field = value; EmitSignal(SignalName.ChangedSettings); } } = false;
	public Vector2I WindowSize { get; set { field = value; EmitSignal(SignalName.ChangedSettings); } } = new Vector2I(1280, 720);
	
	private bool _oldMaxWindow = false;
	
	public override void _Ready()
	{
		GetWindow().SizeChanged += () => {
			var max = DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Maximized;
			MaxWindow = max;
			
			if (_oldMaxWindow && (_oldMaxWindow != MaxWindow) || !max)
			{
				WindowSize = GetWindow().Size;
			}
			
			_oldMaxWindow = max;
		};
	}
	
	public void OpenSettings()
	{
		_settingsWindowContainer.OpenWindow();
	}
	
	public void CloseSettings()
	{
		_settingsWindowContainer.CloseWindow();
	}
	
	public void LoadSettings()
	{
		ConfigFile file = new ConfigFile();
		if (file.Load(_settingsPath) == Error.Ok)
		{
			MasterVolume = (double)file.GetValue("Audio", "MasterVolume", 1);
			MusicVolume = (double)file.GetValue("Audio", "MusicVolume", 1);
			SfxVolume = (double)file.GetValue("Audio", "SfxVolume", 1);
			MaxWindow = (bool)file.GetValue("Graphics", "MaxWindow", false);
			WindowSize = (Vector2I)file.GetValue("Graphics", "WindowSize", new Vector2I(1280, 720));
		}
	}
	
	public void SaveSettings()
	{
		ConfigFile file = new ConfigFile();
		file.Load(_settingsPath);
		file.SetValue("Audio", "MasterVolume", MasterVolume);
		file.SetValue("Audio", "MusicVolume", MusicVolume);
		file.SetValue("Audio", "SfxVolume", SfxVolume);
		file.SetValue("Graphics", "MaxWindow", MaxWindow);
		file.SetValue("Graphics", "WindowSize", WindowSize);
		file.Save(_settingsPath);
	}
}

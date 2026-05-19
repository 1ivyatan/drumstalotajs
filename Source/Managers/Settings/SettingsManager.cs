using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources;

namespace Drumstalotajs.Managers.Settings;

public partial class SettingsManager : Node
{
	[Signal] public delegate void ChangedSettingsEventHandler();
	[Export] private string _settingsPath;
	
	public double MasterVolume { get; set { field = value; EmitSignal(SignalName.ChangedSettings); } } = 1;
	public double MusicVolume { get; set { field = value; EmitSignal(SignalName.ChangedSettings); } } = 1;
	public double SfxVolume { get; set { field = value; EmitSignal(SignalName.ChangedSettings); } } = 1;
	
	public override void _Notification(int what)
	{
		if (what == NotificationWMCloseRequest)
		{
			SaveSettings();
		}
	}
	
	public void LoadSettings()
	{
		ConfigFile file = new ConfigFile();
		if (file.Load(_settingsPath) == Error.Ok)
		{
			MasterVolume = (double)file.GetValue("Audio", "MasterVolume", 1);
			MusicVolume = (double)file.GetValue("Audio", "MusicVolume", 1);
			SfxVolume = (double)file.GetValue("Audio", "SfxVolume", 1);
		}
	}
	
	public void SaveSettings()
	{
		ConfigFile file = new ConfigFile();
		file.Load(_settingsPath);
		file.SetValue("Audio", "MasterVolume", MasterVolume);
		file.SetValue("Audio", "MusicVolume", MusicVolume);
		file.SetValue("Audio", "SfxVolume", SfxVolume);
		file.Save(_settingsPath);
	}
}

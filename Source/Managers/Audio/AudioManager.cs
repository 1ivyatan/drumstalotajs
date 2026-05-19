using Godot;
using Godot.Collections;
using System;
using System.Linq;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Managers.Audio;

public partial class AudioManager : Node
{
	[Export] private Dictionary<UiMusic, AudioStream> _uiMusic = new();
	[Export] private AudioStreamPlayer _uiMusicPlayer;
	public AudioMode AudioMode { get; private set; } = AudioMode.Mute;
	public int MasterBusIndex { get; private set; } = -1;
	public int MusicBusIndex { get; private set; } = -1;
	public int SfxBusIndex { get; private set; } = -1;
	private bool Paused { get; set; }
	
	public override void _Ready()
	{
		MasterBusIndex = AudioServer.GetBusIndex("Master");
		MusicBusIndex = AudioServer.GetBusIndex("Music");
		SfxBusIndex = AudioServer.GetBusIndex("Sfx");
	}
	
	public void SetMasterVolume(double masterLinear) {
		if (MasterBusIndex != -1) AudioServer.SetBusVolumeLinear(MasterBusIndex, (float)masterLinear); }
	
	public void SetMusicVolume(double musicLinear) {
		if (MusicBusIndex != -1) AudioServer.SetBusVolumeLinear(MusicBusIndex, (float)musicLinear); }
	
	public void SetSfxVolume(double sfxLinear) {
		if (SfxBusIndex != -1) AudioServer.SetBusVolumeLinear(SfxBusIndex, (float)sfxLinear); }
		
	public double GetMasterVolume()
	{
		if (MasterBusIndex != -1) return AudioServer.GetBusVolumeLinear(MasterBusIndex);
		else return 0;
	}
	
	public double GetMusicVolume()
	{
		if (MusicBusIndex != -1) return AudioServer.GetBusVolumeLinear(MusicBusIndex);
		else return 0;
	}
	
	public double GetSfxVolume()
	{
		if (SfxBusIndex != -1) return AudioServer.GetBusVolumeLinear(SfxBusIndex);
		else return 0;
	}
	
	public void SetAudioMode(AudioMode mode, UiMusic uiMusic = UiMusic.BattleOne)
	{
		AudioMode = mode;
		switch (mode)
		{
			case AudioMode.Main:
				SetUiMusic(UiMusic.Main);
				break;
			case AudioMode.Planning:
				SetUiMusic(UiMusic.Planning);
				break;
			case AudioMode.Mute:
				_uiMusicPlayer.Stop();
				_uiMusicPlayer.Stream = null;
				break;
			case AudioMode.Battle:
				SetUiMusic(uiMusic);
				break;
			default: break;
		}
		Paused = false;
	}
	
	public void PauseAll()
	{
		if (_uiMusicPlayer.Playing) _uiMusicPlayer.StreamPaused = true;
		Paused = true;
	}
	
	public void ResumeAll()
	{
		if (_uiMusicPlayer.StreamPaused) _uiMusicPlayer.StreamPaused = false;
		Paused = false;
	}
	
	public void SetUiMusic(UiMusic musicType)
	{
		if (!_uiMusic.ContainsKey(musicType)) return;
		if (_uiMusicPlayer.Stream != _uiMusic[musicType])
		{
			_uiMusicPlayer.Stop();
			_uiMusicPlayer.Stream = _uiMusic[musicType];
			_uiMusicPlayer.Play();
		} else
		{
			_uiMusicPlayer.Stop();
			_uiMusicPlayer.Play();
		}
	}
}

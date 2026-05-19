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
	private bool Paused { get; set; }
	
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

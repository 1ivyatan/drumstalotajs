using Godot;
using Godot.Collections;
using System;
using System.Linq;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Managers.Audio;

public partial class AudioManager : Node
{
	[Export] private Dictionary<UiMusic, AudioStream> _uiMusic = new();
	[Export] private AudioStreamPlayer _uiPlayer;
	
	public override void _Ready()
	{
	}
	
	public void SetUiMusic(UiMusic musicType)
	{
		if (musicType == UiMusic.Mute)
		{
			_uiPlayer.Stop();
			_uiPlayer.Stream = null;
		} else
		{
			if (_uiPlayer.Stream != _uiMusic[musicType])
			{
				_uiPlayer.Stream = _uiMusic[musicType];
				_uiPlayer.Play();
			}
		}
	}
}

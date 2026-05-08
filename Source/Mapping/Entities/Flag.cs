using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Flag : Sprite2D
{
	[Export] private Texture2D _enemy;
	[Export] private Texture2D _player;
	
	public void SetFlag(bool player)
	{
		if (player) Texture = _player;
		else Texture = _enemy;
	}
}

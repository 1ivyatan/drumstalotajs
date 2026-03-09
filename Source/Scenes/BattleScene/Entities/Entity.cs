using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Map.Entities
{
	public partial class Entity : Area2D
	{
		[Export] public Resources.Entities.Entity EntityResource { get; private set; }
	}
}

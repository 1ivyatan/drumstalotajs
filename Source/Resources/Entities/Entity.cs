using Godot;
using System;

namespace Drumstalotajs.Resources.Entities
{
	[GlobalClass]
	public partial class Entity : Resource
	{
		[Export] public string Name { get; set; }
		[Export] public int Type { get; set; }
		[Export] public double Height { get; set; }
		[Export] public Texture2D[] Sprites { get; set; }
		
		public Scenes.BattleScene.Map.Entities.Type GetEntityType()
		{ 
			return (Scenes.BattleScene.Map.Entities.Type)Type;
		}
	}
}

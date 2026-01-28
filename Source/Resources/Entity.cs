using Godot;
using System;

namespace Drumstalotajs.Resources
{
	[GlobalClass]
	public partial class Entity : Resource
	{
		[Export]
		public int Id { get; set; }
		
		[Export]
		public int Type
		{ 
			get; 
			set
			{
				if (!Enum.IsDefined(typeof(Battle.Entities.Type), value))
				{
					field = -1;
				} else
				{
					field = value;
				}
			}
		}
		
		[Export]
		public Texture2D[] Sprites { get; set; }
		
		public Entity() : this(-1, (int)Battle.Entities.Type.None, null) {}
		
		public Entity(int id, int type, Texture2D[] sprites)
		{
			Id = id;
			Type = type;
			Sprites = sprites ?? null;
		}
		
	}
}

using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Defense : Entity
	{
		public Resources.Entities.Defense DefenseResource => EntityResource as Resources.Entities.Defense;
		
		private Sprite2D _sprite;
		
		/* change this! */
		public void Destroy()
		{
			QueueFree();
			GetParent().RemoveChild(this);
		}
		
		public override void _Ready()
		{
			_sprite = GetNode<Sprite2D>("Sprite");
			
			if (EntityResource != null)
			{
				_sprite.Texture = EntityResource.Sprites[0];
			}
		}
	}
}

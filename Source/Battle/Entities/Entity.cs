using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Entity : Node2D
	{
		[Export]
		public Resources.Entity EntityResource { get; private set; }
		
		
	}
}

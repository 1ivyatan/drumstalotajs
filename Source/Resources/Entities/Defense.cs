using Godot;
using System;

namespace Drumstalotajs.Resources.Entities
{
	[GlobalClass]
	public partial class Defense : Entity
	{
		[Export]
		public double Thickness { get; set; }
		
		[Export]
		public double MaterialFactor { get; set; }
		
		[Export]
		public double Height { get; set; }
	}
}

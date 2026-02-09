using Godot;
using System;

namespace Drumstalotajs.Resources.Entities
{
	[GlobalClass]
	public partial class Device : Entity
	{
		[Export]
		public double StartingAngle { get; set; }
		
		[Export]
		public double AngleRadius { get; set; }
		
		[Export]
		public bool CanRotate { get; set; }
	}
}

using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Resources.Mapping.Entities;

[GlobalClass]
public partial class DevicePropertiesData : EntityPropertiesData
{
	[ExportGroup("Device")]
	[Export] public double MinAngle { get; set; } = 10;
	[Export] public double MaxAngle { get; set; } = 80;
	[Export] public double TraverseRadius { get; set; } = 15;
	[Export] public double MuzzleVelocity { get; set; } = 20;
	
	[ExportGroup("Supplies")]
	[Export] public int Shells { get; set; } = 40;
	[Export] public int ResupplyTurns { get; set; } = 10;
	
	[ExportGroup("Shell")]
	[Export] public double Caliber { get; set; } = 81; /* mm */
	[Export] public double ExplosiveFill { get; set; } = .7; /* kg */
	[Export] public double TotalWeight { get; set; } = 4; /* kg */
	[Export] public double LethalRadius { get; set; } = -1; /* m */
	[Export] public double CasualityRadius { get; set; } = -1; /* m */
	[Export] public double DragCoefficient { get; set; } = .5; 
	[Export] public double BallisticCoefficient { get; set; } = .5;
}

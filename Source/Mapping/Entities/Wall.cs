using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Wall : Entity
{
	public double Height { get; private set; } = 0;
	
	public override void _Ready()
	{
		if (Properties != null && Properties is WallPropertiesData wallProperties)
		{
			Height = wallProperties.Height;
		}
	}
}

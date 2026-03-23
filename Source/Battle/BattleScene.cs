using Godot;
using System;

namespace drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	private Resources.Maps.Meta MetaData { get; set; }
	
	private Mapping.Map map;

	public override void _Ready()
	{
		map = GetNode<Node2D>("Map") as Mapping.Map;
		map.LoadMap(MetaData);
		map.Mode = Mapping.Map.MapMode.VIEW;
		
		/* cursor */
		map.Camera.StateChange += (Mapping.Camera.MapCamera.MapCameraState state) => {
			GD.Print(state);
		};
	}
	
	public void LoadMap(Resources.Maps.Meta mapMeta)
	{
		MetaData = mapMeta;
	}
}

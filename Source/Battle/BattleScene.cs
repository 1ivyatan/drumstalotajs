using Godot;
using System;

namespace drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	private Resources.Maps.Meta MetaData { get; set; }
	
	private Mapping.Map map;
	private Managers.CursorManager cursorManager;

	public override void _Ready()
	{
		map = GetNode<Node2D>("Map") as Mapping.Map;
		cursorManager = GetNode<Node>("../../CursorManager") as Managers.CursorManager;
		map.LoadMap(MetaData);
		map.Mode = Mapping.Map.MapMode.VIEW;
		
		map.Camera.StateChange += (Mapping.Camera.MapCamera.MapCameraState state) => {
			switch (state)
			{
				case Mapping.Camera.MapCamera.MapCameraState.IDLE:
					cursorManager.ArrowCursor();
					break;
				case Mapping.Camera.MapCamera.MapCameraState.ZOOM:
				case Mapping.Camera.MapCamera.MapCameraState.ZOOMDRAG:
					cursorManager.ZoomCursor();
					break;
				case Mapping.Camera.MapCamera.MapCameraState.DRAG:
					cursorManager.DragCursor();
					break;
				default:
					cursorManager.ResetCursor();
					break;
			}
			GD.Print(state);
		};
	}
	
	public void LoadMap(Resources.Maps.Meta mapMeta)
	{
		MetaData = mapMeta;
	}
}

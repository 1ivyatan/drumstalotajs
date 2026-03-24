using Godot;
using System;

namespace drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	private Resources.Maps.Meta MetaData { get; set; }
	public BattleSceneState State { get; private set; } = BattleSceneState.LOADING;
	
	private Mapping.Map map;
	private Managers.CursorManager cursorManager;
	private Components.Modals.Modal pauseModal;

	public override void _Ready()
	{
		map = GetNode<Node2D>("Map") as Mapping.Map;
		pauseModal = GetNode<Control>("UI/PauseModal") as Components.Modals.Modal;
		cursorManager = GetNode<Node>("../../CursorManager") as Managers.CursorManager;
		map.LoadMap(MetaData);
		map.Mode = Mapping.Map.MapMode.VIEW;
		
		pauseModal.Closed += Resume;
		
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
		};
		
		State = BattleSceneState.ACTIVE;
	}
	
	public void LoadMap(Resources.Maps.Meta mapMeta)
	{
		MetaData = mapMeta;
	}
	
	public void Pause()
	{
		State = BattleSceneState.PAUSED;
		pauseModal.Open();
	}
	
	public void Resume()
	{
		State = BattleSceneState.ACTIVE;
	}
}

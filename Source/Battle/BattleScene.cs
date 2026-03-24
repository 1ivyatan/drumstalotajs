using Godot;
using System;

namespace drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	private Resources.Maps.Meta MetaData { get; set; }
	public BattleSceneState State { get; private set; } = BattleSceneState.LOADING;
	
	private Mapping.Map map;
	private Managers.SceneManager sceneManager;
	private Managers.CursorManager cursorManager;
	private Components.Modals.Modal pauseModal;
	private Stages.StageManager stageManager;

	public override void _Ready()
	{
		map = GetNode<Node2D>("Map") as Mapping.Map;
		sceneManager = GetNode<Node>("../") as Managers.SceneManager;
		pauseModal = GetNode<Control>("UI/PauseModal") as Components.Modals.Modal;
		cursorManager = GetNode<Node>("../../CursorManager") as Managers.CursorManager;
		stageManager = GetNode<CanvasLayer>("StageManager") as Stages.StageManager;
		map.LoadMap(MetaData);
		map.Mode = Mapping.Map.MapMode.VIEW;
		
		pauseModal.Closed += Resume;
		
		(pauseModal.GetModalWindow().GetNode<Control>("PauseMenu") as TopPanels.PauseMenu).ToLevels += ExitBattle;
		
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
		stageManager.DevicePlacementStage();
		State = BattleSceneState.ACTIVE;
	}
	
	private void ExitBattle()
	{
		sceneManager.LevelsScene();
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

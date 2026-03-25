using Godot;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	[Export] private Resources.Maps.Meta metaData;
	
	private Vector2I[] groundLayerAtlas;
	private Vector2I[] decorationLayerAtlas;
	
	private Mapping.Map map;
	private HelpContainer helpContainer;
	private MetaContainer metaContainer;
	private Managers.ToastManager toastManager;
	
	private double heightFactor = 1;
	private double aziFactor = 1;
	
	private Vector2I selectedPosition;
	private Entities.Entity selectedEntity = null;
	private bool saving = false;
	
	public override void _Ready()
	{
		map = GetNode<Node2D>("Map") as Mapping.Map;
		helpContainer = GetNode<PanelContainer>("UI/HelpContainer") as HelpContainer;
		metaContainer = GetNode<PanelContainer>("UI/MetaContainer") as MetaContainer;
		toastManager = GetNode<Control>("../../Overlay/ToastManager") as Managers.ToastManager;
		groundLayerAtlas = map.GroundLayer.GetTileAtlas();
		decorationLayerAtlas = map.DecorationLayer.GetTileAtlas();
		
		if (metaData != null)
		{
			map.LoadMap(metaData);
		}
		
		map.Selector.HoveredGround += (Vector2I cellPos) => {
			selectedPosition = cellPos;
		};
		
		map.Selector.HoveredEntity += (Entities.Entity entity) => {
			selectedEntity = entity;
		};
		
		map.Selector.UnhoveredEntity += () => {
			selectedEntity = null;
		};
		
		map.Selector.DisappearedSelectedEntity += () => {
			selectedEntity = null;
		};
		
		map.Mode = Mapping.Map.MapMode.EDIT;
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (!map.Loaded || map.Mode != Mapping.Map.MapMode.EDIT) return;
		if (@event is InputEventKey keyEvent && keyEvent.Pressed)
		{
			switch (keyEvent.Keycode)
			{
				case Key.H when !keyEvent.Echo:
					helpContainer.ToggleContainer();
					break;
				case Key.M when !keyEvent.Echo:
					metaContainer.ToggleContainer();
					break;
				case Key.S when !keyEvent.Echo && keyEvent.CtrlPressed:
					SaveMap();
					break;
				case Key.R:
					ChangeGroundHeight(selectedPosition, keyEvent.ShiftPressed ? ( heightFactor * 0.1 ) : heightFactor);
					break;
				case Key.F:
					ChangeGroundHeight(selectedPosition, keyEvent.ShiftPressed ? ( -heightFactor * 0.1 ) : -heightFactor);
					break;
				case Key.W:
					ChangeEntityAzimuth(selectedEntity, keyEvent.ShiftPressed ? ( aziFactor * 0.1 ) : aziFactor);
					break;
				case Key.Q:
					ChangeEntityAzimuth(selectedEntity, keyEvent.ShiftPressed ? ( -aziFactor * 0.1 ) : -aziFactor);
					break;
				case Key.E when !keyEvent.Echo && !keyEvent.CtrlPressed:
					NextEntity(selectedPosition);
					break;
			}
		}
						
					/*
					case Key.G:
						if (keyEvent.Echo) return;
						NextTileFromAtlas(map.GroundLayer, groundLayerAtlas, map.CurrentCellPos);
						break;
					case Key.D:
						if (keyEvent.Echo) return;
						NextTileFromAtlas(map.DecorationLayer, decorationLayerAtlas, map.CurrentCellPos);
						break;
					case Key.E:
						if (keyEvent.Echo) return;
						if (keyEvent.CtrlPressed) return;
						NextEntity(map.EntityLayer, map.CurrentCellPos);
						break;
					case Key.R:
						ChangeGroundHeight(map.CurrentCellPos, keyEvent.ShiftPressed ? ( heightFactor * 0.1 ) : heightFactor);
						break;
					case Key.F:
						ChangeGroundHeight(map.CurrentCellPos, keyEvent.ShiftPressed ? ( -heightFactor * 0.1 ) : -heightFactor);
						break;
					case Key.W:
						ChangeEntityAzimuth(selectedEntity, keyEvent.ShiftPressed ? ( aziFactor * 0.1 ) : aziFactor);
						break;
					case Key.Q:
						ChangeEntityAzimuth(selectedEntity, keyEvent.ShiftPressed ? ( -aziFactor * 0.1 ) : -aziFactor);
						break;*/
	}
	
	private void SaveMap()
	{
		if (saving == false)
		{
			Resources.Maps.Meta metaData = metaContainer.ExportMeta();
			Resources.Maps.Map mapData = new Resources.Maps.Map();
			mapData.GroundLayer = map.GroundLayer.ExportTiles();
			mapData.DecorationLayer = map.DecorationLayer.ExportTiles();
			mapData.EntityLayer = map.EntityLayer.ExportTiles();
			ResourceSaver.Save(mapData, $"res://Exports/Maps/Map.tres");
			ResourceSaver.Save(metaData, $"res://Exports/Maps/Meta.tres");
			
			saving = true;
			toastManager.SpawnToast("Save successful");
			SceneTreeTimer delayToSaveAgain = GetTree().CreateTimer(5f);
			delayToSaveAgain.Connect("timeout", Callable.From(() => {
				saving = false;
			}));
		}
	}
	private void NextEntity(Vector2I cellPos)
	{
		Mapping.Layers.EntityLayer entityLayer = map.EntityLayer;
		Vector2 localPosCentered = entityLayer.CellToLocalPos(cellPos, true);
		Entities.Entity[] entities = entityLayer.FlashEntities(localPosCentered, 1);
		
		if (entities == null || entities.Length == 0)
		{
			entityLayer.SpawnEntity(localPosCentered, 1);
		} else
		{
			int id = entities[0].EntityResource.Id;
			int length = entityLayer.EntityScenes.Count;
			int index = entityLayer.EntityScenes.IndexOf(id);
			int next = index + 1;
			
			map.EntityLayer.RemoveEntity(entities[0]);
			
			//if (next < length)
			//{
			//	int nextId = entityLayer.EntityScenes.GetAt(next).Key;
			//	entityLayer.SpawnEntity(localPosCentered, nextId);
			//}
			
			//
		}
		/*
		if (entities == null) {
			
		} else {
			
			map.EntityLayer.RemoveEntity(entities[0]);
			
			if (next < length)
			{
				int nextId = entityLayer.EntityScenes.GetAt(next).Key;
				entityLayer.SpawnEntity(localPosCentered, nextId);
			}
		}*/
	}
	
	/*
	private void NextTileFromAtlas(Mapping.Layers.Layer layer, Vector2I[] atlas, Vector2I cellPos)
	{
		Vector2I currentAtlas = layer.GetCellAtlasCoords(cellPos);
		int index = Array.IndexOf(atlas, currentAtlas);
		int length = atlas.Length;
		int next = index + 1;
		
		if (next < length)
		{
			layer.SetCell(cellPos, 0, atlas[next], 0);
		} else
		{
			layer.EraseCell(cellPos);
		}
	}*/
	
	private void ChangeGroundHeight(Vector2I cellPos, double change)
	{
		double newHeight = map.GroundLayer.GetRelHeight(cellPos) + change;
		map.GroundLayer.SetRelHeight(cellPos, newHeight);
		toastManager.Clear();
		toastManager.SpawnToast($"{newHeight}m");
	}
	
	private void ChangeEntityAzimuth(Entities.Entity entity, double change)
	{
		if (entity != null)
		{
			double newAzimuth = Math.Round(entity.Azimuth + change, 3);
			entity.Azimuth = newAzimuth;
			toastManager.Clear();
			toastManager.SpawnToast($"{entity.Azimuth}deg");
			map.Selector.RefreshHightlighter();
		}
	}
}

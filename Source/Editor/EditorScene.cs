using Godot;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	private Dictionary<Vector2I, double> relativeHeights;
	private Vector2I[] groundLayerAtlas;
	private Vector2I[] decorationLayerAtlas;
	
	private Mapping.Map map;
	private PanelContainer helpContainer;
	private Managers.ToastManager toastManager;
	
	private double heightFactor = 1;
	
	public override void _Ready()
	{
		map = GetNode<Node2D>("Map") as Mapping.Map;
		helpContainer = GetNode<PanelContainer>("UI/HelpContainer");
		toastManager = GetNode<Control>("../../Overlay/ToastManager") as Managers.ToastManager;
		groundLayerAtlas = map.GroundLayer.GetTileAtlas();
		decorationLayerAtlas = map.DecorationLayer.GetTileAtlas();
		relativeHeights = new Dictionary<Vector2I, double>();
		map.Editing = true;
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent)
		{
			if (keyEvent.Pressed)
			{
				switch (keyEvent.Keycode)
				{
					case Key.H:
						if (keyEvent.Echo) return;
						helpContainer.SetVisible(!helpContainer.Visible);
						break;
					case Key.S:
						if (keyEvent.Echo) return;
						if (keyEvent.CtrlPressed)
						{
							GD.Print("Ctrl+S pressed manually");
						}
						break;
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
						NextEntity(map.EntityLayer, map.CurrentCellPos);
						break;
					case Key.R:
						ChangeGroundHeight(map.CurrentCellPos, keyEvent.ShiftPressed ? ( heightFactor * 0.1 ) : heightFactor);
						break;
					case Key.F:
						ChangeGroundHeight(map.CurrentCellPos, keyEvent.ShiftPressed ? ( -heightFactor * 0.1 ) : -heightFactor);
						break;
				}
			}
		}
	}
	
	private void NextEntity(Mapping.Layers.EntityLayer entityLayer, Vector2I cellPos)
	{
		Vector2 localPos = entityLayer.CellToLocalPos(cellPos);
		Vector2 localPosCentered = entityLayer.CellToLocalPos(cellPos, true);
		Entities.Entity[] entities = entityLayer.Flash(localPos, 1);
		if (entities == null) {
			entityLayer.SpawnEntity(localPosCentered, 1);
		} else {
			int id = entities[0].EntityResource.Id;
			int length = entityLayer.EntityScenes.Count;
			int index = entityLayer.EntityScenes.IndexOf(id);
			int next = index + 1;
			
			map.EntityLayer.RemoveEntity(entities[0]);
			
			if (next < length)
			{
				int nextId = entityLayer.EntityScenes.GetAt(next).Key;
				entityLayer.SpawnEntity(localPosCentered, nextId);
			}
			
			GD.Print(entityLayer.Entities.Count);
		}
	}
	
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
	}
	
	private void ChangeGroundHeight(Vector2I cellPos, double change)
	{
		if (!relativeHeights.ContainsKey(cellPos))
		{
			double defaultHeight = (double)map.GroundLayer.GetCellTileData(cellPos).GetCustomData("DefaultRelHeightee");
			relativeHeights.Add(cellPos, defaultHeight);
		}
		
		relativeHeights[cellPos] = Math.Round(relativeHeights[cellPos] + change, 3);
		toastManager.Clear();
		toastManager.SpawnToast($"{relativeHeights[cellPos]}m");
	}
}

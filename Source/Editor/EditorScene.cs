using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	private Dictionary<Vector2I, double> relativeHeights;
	private Vector2I[] groundLayerAtlas;
	
	private Mapping.Map map;
	private RichTextLabel help;
	private Managers.ToastManager toastManager;
	
	private double heightFactor = 1;
	
	public override void _Ready()
	{
		map = GetNode<Node2D>("Map") as Mapping.Map;
		help = GetNode<RichTextLabel>("UI/Help");
		toastManager = GetNode<Control>("../../Overlay/ToastManager") as Managers.ToastManager;
		groundLayerAtlas = map.GroundLayer.GetTileAtlas();
		relativeHeights = new Dictionary<Vector2I, double>();
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
						help.SetVisible(!help.Visible);
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
			relativeHeights.Add(cellPos, 0);
		}
		
		relativeHeights[cellPos] = Math.Round(relativeHeights[cellPos] + change, 3);
		toastManager.Clear();
		toastManager.SpawnToast($"{relativeHeights[cellPos]}m");
	}
}

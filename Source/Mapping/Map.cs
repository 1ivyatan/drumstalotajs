using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Scores;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	public MapMode Mode { get; 
		set {
			field = value;
			
			switch (value)
			{
				case MapMode.Locked:
					Selector.Mode = SelectorMode.Locked;
					break;
				case MapMode.HiddenInteractable:
					Selector.Mode = SelectorMode.HiddenInteractable;
					break;
				case MapMode.Interactable:
					Selector.Mode = SelectorMode.Interactable;
					break;
				case MapMode.Edit:
					Selector.Mode = SelectorMode.Interactable;
					break;
				default: break;
			}
		}
	} = MapMode.Locked;
	public MapState State { get; private set; } = MapState.Empty;
	
	public GroundLayer GroundLayer { get; private set; }
	public OverlayLayer OverlayLayer { get; private set; }
	public Selector Selector { get; private set; }
	
	public Resources.Mapping.Map MapData { get; private set; } = null;
	public Resources.Mapping.MapMeta MapMeta { get; private set; } = null;
	
	public override void _Ready()
	{
		OverlayLayer = GetNode("OverlayLayer") as OverlayLayer;
		GroundLayer = GetNode("GroundLayer") as GroundLayer;
		Selector = GetNode("Selector") as Selector;
	}
	
	public void Load(MapMeta mapMeta)
	{
		State = MapState.Loading;
		MapMeta = mapMeta;
		MapData = mapMeta.LoadMap();
		State = MapState.Done;
	}
}

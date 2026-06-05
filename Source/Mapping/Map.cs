using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Mapping.Projectiles;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Resources.Mapping.Layers;
using System.Threading.Tasks;
using Drumstalotajs.Mapping.Entities;
using Drumstalotajs.Mapping.Markers;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	[Signal] public delegate void StateChangeEventHandler(MapState state);
	[Signal] public delegate void ModeChangeEventHandler(MapMode mode);
	[Signal] public delegate void EditedEventHandler();
	
	[Export] public FrameBackground FrameBackground { get; private set; }
	[Export] public GroundLayer GroundLayer { get; private set; }
	[Export] public AtlasLayer DecorationLayer { get; private set; }
	[Export] public EntityLayer EntityLayer { get; private set; }
	[Export] public OverlayLayer OverlayLayer { get; private set; }
	[Export] public ProjectileLayer ProjectileLayer { get; private set; }
	[Export] public MarkerLayer MarkerLayer { get; private set; }
	[Export] public Selector Selector { get; private set; }
	[Export] public Camera Camera { get; private set; }
	
	public MapState State { get; 
		private set {
			field = value;
			EmitSignal(SignalName.StateChange, (int)field);
		}
	} = MapState.Initialized;
	
	public MapMode Mode { get;
		set {
			if (State == MapState.Loading || State == MapState.Initialized) return;
			field = value;
			switch (field)
			{
				case MapMode.Locked:
					Selector.Mode = SelectorMode.Locked;
					Camera.Mode = CameraMode.Locked;
					break;
				case MapMode.HiddenInteractable:
					Selector.Mode = SelectorMode.Invisible;
					Camera.Mode = CameraMode.DragOnly;
					break;
				case MapMode.Interactable:
					Selector.Mode = SelectorMode.Interactable;
					Camera.Mode = CameraMode.DragOnly;
					break;
				case MapMode.Editing:
					Selector.Mode = SelectorMode.Editing;
					Camera.Mode = CameraMode.DragOnly;
					break;
				default: break;
			}
			EmitSignal(SignalName.ModeChange, (int)field);
		}
	} = MapMode.Locked;
	
	public MapResource CurrentLoadedMap { get; private set; } = null;
	public Vector2I SquareMetersPerCell { get; private set; } = Constants.Mapping.TileSize;
	public Vector2 CellCoefficient { get; private set; } = new Vector2(1.0f, 1.0f);
	
	public override void _Ready()
	{
		MoveChild(Camera, -1);
	}
	
	public MapResource Export()
	{
		return new MapResource(this);
	}
	
	public async Task Load(string mapResourcePath)
	{
		if (mapResourcePath == null || mapResourcePath.Length == 0)
		{
			return;
		}
		
		State = MapState.Loading;
		try {
			var data = Files.SafeLoadResource<MapResource>(mapResourcePath, false);
			CurrentLoadedMap = data;
			SquareMetersPerCell = CurrentLoadedMap.MetersPerCell;
			CellCoefficient = (
				new Vector2((float)SquareMetersPerCell.X, (float)SquareMetersPerCell.Y) / 
				new Vector2((float)Constants.Mapping.TileSize.X, (float)Constants.Mapping.TileSize.Y)
			);
			GroundLayer.Load(data.GroundLayer);
			DecorationLayer.Load(data.DecorationLayer);
			EntityLayer.Load(data.EntityLayer);
			OverlayLayer.Load(data.OverlayLayer);
			CalibrateView();
			State = MapState.Done;
		} catch (Exception e)
		{
			GD.Print(e);
			State = MapState.Error;
		}
	}
	
	public void CalibrateView()
	{
		Camera.Calibrate();
		FrameBackground.Calibrate();
	}
	
	public Vector2I ViewportMouseToMap()
	{
		/*
		Vector2 mouseLocalPos = GroundLayer.GetLocalMousePosition();
		Vector2I mapPos = GroundLayer.LocalToMap(mouseLocalPos);
		Rect2I usedRect = GroundLayer.GetUsedRect();
		if (!usedRect.HasArea()) return mapPos;
		return new Vector2I(
			Mathf.Clamp(mapPos.X, usedRect.Position.X, usedRect.End.X - 1),
			Mathf.Clamp(mapPos.Y, usedRect.Position.Y, usedRect.End.Y - 1)
		);*/
		Vector2 mouseLocalPos = GroundLayer.GetLocalMousePosition();
		return GroundLayer.LocalToMap(mouseLocalPos);
	}
	
	public Vector2 ViewportMouseToLocal()
	{
		/*
		Vector2 mouseLocalPos = GroundLayer.GetLocalMousePosition();
		Vector2I mapPos = GroundLayer.LocalToMap(mouseLocalPos);
		Rect2I usedRect = GroundLayer.GetUsedRect();
		if (!usedRect.HasArea()) return mapPos;
		return new Vector2I(
			Mathf.Clamp(mapPos.X, usedRect.Position.X, usedRect.End.X - 1),
			Mathf.Clamp(mapPos.Y, usedRect.Position.Y, usedRect.End.Y - 1)
		);*/
		return GetLocalMousePosition();
	}
	
	public Vector2 ViewportToMilCoords()
	{
		var mousePos = ViewportMouseToMap();
		var cellRect = GroundLayer.GetUsedRect();

		var x = mousePos.X - cellRect.Position.X;
		var bY = cellRect.Position.Y + cellRect.Size.Y - 1;
		var y = bY - mousePos.Y;

		return new Vector2(x, y);
	}
	
	public Vector2 LocalPosToMilCoords(Vector2 localPos)
	{
		var pos = GroundLayer.LocalToMap(localPos);
		var cellRect = GroundLayer.GetUsedRect();

		var x = pos.X - cellRect.Position.X;
		var bY = cellRect.Position.Y + cellRect.Size.Y - 1;
		var y = bY - pos.Y;

		return new Vector2(x, y);
	}
	
	public string FormatMilCoords(Vector2I coords)
	{
		return $"[{coords.X:D2} {coords.Y:D2}]";
	}
	
	public string FormatMilCoords(Vector2 coords)
	{
		int x = Mathf.RoundToInt(coords.X * 10f);
		int y = Mathf.RoundToInt(coords.Y * 10f);
		return $"[{x:D2} {y:D2}]";
	}
	
	public Rect2 GetMapRect()
	{
		var rect = GroundLayer.GetUsedRect();
		rect.Size *= GroundLayer.TileSize;
		rect.Position *= GroundLayer.TileSize;
		return rect;
	}
	
	public bool ViewportMouseOnMap()
	{
		var cellRect = GroundLayer.GetUsedRect();
		var rect = new Rect2();
		var cell = new Vector2((float)Constants.Mapping.TileSize.X, (float)Constants.Mapping.TileSize.Y);
		rect.Position = cellRect.Position * cell;
		rect.Size = cellRect.Size * cell;
		return rect.HasPoint(ViewportMouseToLocal());
	}
	
	public void AddSceneTile(
		AtlasLayer layer, 
		string atlas,
		Vector2I position,
		int source = 0
	)
	{
		bool added = false;
		if (Types.Vector2I.ValidVector2I(atlas))
		{
			Vector2I coords = Types.Vector2I.StringToVector2I(atlas);
				
			if (!(layer is GroundLayer groundLayer) && IsEmpty(position))
			{
				return;
			}
					
			layer.AddTile(position, coords, source);
			added = true;
		}
		
		if (added)
		{
			EmitEdit();
		}
	}
	
	public async void AddTile(
		BaseLayer layer, 
		string atlas,
		Vector2I position
	)
	{
		bool added = false;
		if (layer is AtlasLayer atlasLayer)
		{
			if (Types.Vector2I.ValidVector2I(atlas))
			{
				Vector2I coords = Types.Vector2I.StringToVector2I(atlas);
				
				if (!(atlasLayer is GroundLayer groundLayer) && IsEmpty(position))
				{
					return;
				}
					
				atlasLayer.AddTile(position, coords);
				//await ToSignal(atlasLayer, "TileSpawned");
				added = true;
			}
		} else if (layer is SceneLayer sceneLayer)
		{
			if (GroundLayer.GetCellAtlasCoords(position) != Constants.Vector2I.Negative)
			{
				sceneLayer.AddTile(position, atlas);
				//await ToSignal(sceneLayer, "TileSpawned");
				added = true;
			}
		}
		
		if (added)
		{
			EmitEdit();
		}
	}
	
	public void RemoveTile(BaseLayer layer, Vector2I position)
	{
		if (layer is AtlasLayer atlasLayer)
		{
			atlasLayer.RemoveTile(position);
		} else if (layer is SceneLayer sceneLayer)
		{
			sceneLayer.RemoveTile(position);
		}
	}
	
	public double GetTotalTileHeight(Vector2I position)
	{
		var groundTile = GroundLayer.GetTile(position);
		if (groundTile != null)
		{
			var groundHeight = groundTile.GetFullHeight();
			Entity entity = EntityLayer.GetInstance(position);
			return groundHeight + (entity != null ? entity.Properties.Height : 0);
		} else return GroundLayer.BaseHeight;
	}
	
	public bool IsEmpty(Vector2I position)
	{
		return GroundLayer.GetCellAtlasCoords(position) == Constants.Vector2I.Negative;
	}
	
	public FilteredTiles Flash(Vector2I position, bool strict = false)
	{
		return Selector.GetTiles(position, strict);
	}
	
	private void EmitEdit()
	{
		if (State != MapState.Loading && Mode == MapMode.Editing)
		{
			EmitSignal(SignalName.Edited);
		}
	}
}

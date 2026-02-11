using Godot;
using System;

namespace Drumstalotajs.Battle.Map
{
	public partial class Selector : Node2D
	{
		[Signal]
		public delegate void SelectedEntityEventHandler(int entityType, Vector2I tilePosition);
		
		[Signal]
		public delegate void SelectedEmptyEntityEventHandler(Vector2I tilePosition);
		
		[Signal]
		public delegate void HoveredGroundEventHandler(Vector2I tilePosition);
		
		[Signal]
		public delegate void HoveredEmptyGroundEventHandler(Vector2I tilePosition);
		
		public enum SelectorLayer { Ground, Entity, All }
		public enum SelectorFilterMode { Fitlered, All }
		
		public SelectorLayer Layer { get; set; }
		public SelectorFilterMode FilterMode { get; set; }
		public Battle.Entities.Type[] Filter { get; set; }

		public bool Enabled { get; set; }
		
		private Map.Widget _mapWidget;
		private TileMapLayer _groundLayer;
		private TileMapLayer _entityLayer;
		private Sprite2D _highlighter;
		private Vector2I _currentCellPos;
		
		public override void _Ready()
		{
			_highlighter = GetNode<Sprite2D>("Highlighter");
			_mapWidget = GetParent<Node2D>() as Map.Widget;
			_groundLayer = _mapWidget.GetNode<TileMapLayer>("GroundLayer");
			_entityLayer = _mapWidget.GetNode<TileMapLayer>("EntityLayer");
			_currentCellPos = new Vector2I(0, 0);
			Enabled = false;
			Visible = false;
		}
		
		public override void _Input(InputEvent @event)
		{
			if (Enabled)
			{
				if (@event is InputEventMouse mouseEvent)
				{
					Vector2 globalMousePos = GetGlobalMousePosition();
					Vector2 localMousePos = _groundLayer.ToLocal(globalMousePos);
					Vector2I cellPos = _groundLayer.LocalToMap(localMousePos);
					Entities.Type entityType = (Entities.Type)_entityLayer.GetCellAlternativeTile(cellPos);
					
					if (mouseEvent is InputEventMouseButton mouseClick)
					{
						_currentCellPos = cellPos;
						
						if (FilteredOut(cellPos, entityType))
						{
							Visible = false;
							return;
						}
						
						if (mouseClick.Pressed)
						{
							if (mouseClick.ButtonIndex == (MouseButton)1)
							{
								switch (Layer)
								{
									case SelectorLayer.Entity:
										EmitSignal("SelectedEntity", (int)entityType, cellPos);
										break;
									case SelectorLayer.All:
										if (AllowedEntity(entityType))
										{
											EmitSignal("SelectedEntity", (int)entityType, cellPos);
										} else
										{
											EmitSignal("SelectedEmptyEntity", cellPos);
										}
										break;
								}
							}
							
						}
					}
					
					if (mouseEvent is InputEventMouseMotion mouseMotion)
					{
						if (FilteredOut(cellPos, entityType))
						{
							Visible = false;
							return;
						}
						
						if (_currentCellPos == cellPos)
						{
							return;
						}
						
						switch (Layer)
						{
							case SelectorLayer.All:
							case SelectorLayer.Ground:
								EmitSignal("HoveredGround", cellPos);
								break;
						}
						
					}
					
					SetPosition((cellPos * Physics.Pixels) + new Vector2I(Physics.Pixels / 2, Physics.Pixels / 2));
					Visible = true;
				}
			} else
			{
				Visible = false;
			}
		}
		
		private bool FilteredOut(Vector2I cellPos, Entities.Type entityType)
		{
			switch (Layer)
			{
				case SelectorLayer.All:
					if (_groundLayer.GetCellAtlasCoords(cellPos).Equals(new Vector2I(-1, -1)) )
					{
						EmitSignal("HoveredEmptyGround", cellPos);
						return true;
					}
					break;
				case SelectorLayer.Ground:
					if (_groundLayer.GetCellAtlasCoords(cellPos).Equals(new Vector2I(-1, -1)) )
					{
						EmitSignal("HoveredEmptyGround", cellPos);
						return true;
					}
					break;
				case SelectorLayer.Entity:
					if (!AllowedEntity(entityType))
					{
						EmitSignal("SelectedEmptyEntity", cellPos);
						return true;
					}
					break;
				default: break;
			}
			
			return false;
		}
		
		private bool AllowedEntity(Entities.Type entityType)
		{
			return ((FilterMode == SelectorFilterMode.All && entityType != Entities.Type.None)
			|| (FilterMode == SelectorFilterMode.Fitlered && entityType != Entities.Type.None && Filter != null && Filter.Length > 0 && Filter.Contains(entityType)));
		}

		public override void _Process(double delta)
		{
		}
	}
}

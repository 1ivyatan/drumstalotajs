using Godot;
using System;

namespace Drumstalotajs.Battle.Map
{
	public partial class Selector : Node2D
	{
		[Signal]
		public delegate void SelectedEntityEventHandler(int entityType, Vector2I tilePosition);
		
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
		
		public override void _Ready()
		{
			_highlighter = GetNode<Sprite2D>("Highlighter");
			_mapWidget = GetParent<Node2D>() as Map.Widget;
			_groundLayer = _mapWidget.GetNode<TileMapLayer>("GroundLayer");
			_entityLayer = _mapWidget.GetNode<TileMapLayer>("EntityLayer");
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

					if (
						( Layer == SelectorLayer.Ground && 
						_groundLayer.GetCellAtlasCoords(cellPos).Equals(new Vector2I(-1, -1))) ||
						( Layer == SelectorLayer.Entity && 
						_entityLayer.GetCellAlternativeTile(cellPos) == -1 ) ||
						( Layer == SelectorLayer.All && 
						_groundLayer.GetCellAtlasCoords(cellPos).Equals(new Vector2I(-1, -1)) )
					)
					{
						Visible = false;
						return;
					} else
					{
						Entities.Type entityType = (Entities.Type)_entityLayer.GetCellAlternativeTile(cellPos);
						switch (Layer)
						{
							case SelectorLayer.Ground:
								
								break;
							case SelectorLayer.Entity:
								if (!AllowedEntity(entityType))
								{
									Visible = false;
									return;
								}
								break;
							case SelectorLayer.All:
								if (!AllowedEntity(entityType))
								{
									Visible = false;
									return;
								}
								break;
						}
						
						SetPosition((cellPos * Physics.Pixels) + new Vector2I(Physics.Pixels / 2, Physics.Pixels / 2));	
						Visible = true;
						
						if (mouseEvent is InputEventMouseButton mouseClick && mouseClick.Pressed)
						{
							/* left */
							if (mouseClick.ButtonIndex == (MouseButton)1)
							{
								switch (Layer)
								{
									case SelectorLayer.Ground: 
								
										break;
									case SelectorLayer.Entity:
										EmitSignal("SelectedEntity", (int)entityType, cellPos);
										break;
									case SelectorLayer.All:
										EmitSignal("SelectedEntity", (int)entityType, cellPos);
									break;
								}
							}
						}
					}
					
				}
			} else
			{
				Visible = false;
			}
		}
		
		private bool AllowedEntity(Entities.Type entityType)
		{
			if (FilterMode == SelectorFilterMode.All || (
				FilterMode == SelectorFilterMode.Fitlered &&
				Filter != null && Filter.Length > 0 &&
				Filter.Contains(entityType)
			))
			{
				return true;
			} else
			{
				return false;
			}
		}
		
		//private bool EntityInFilter
		//Filter

		public override void _Process(double delta)
		{
		}
	}
}

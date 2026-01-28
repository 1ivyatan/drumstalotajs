using Godot;
using System;

namespace Drumstalotajs.Battle.Map
{
	public partial class Selector : Node2D
	{
		public enum SelectorLayer { Ground, Entity, All }
		public enum SelectorFilterMode { None, Fitlered, All }
		
		public SelectorLayer Layer { get; set; }
		public SelectorFilterMode FilterMode { get; set; }
		public Battle.Entities.Type[] EntityFilters { get; set; }

		public bool Enabled { get; set; }
		
		private TileMapLayer _groundLayer;
		private TileMapLayer _entityLayer;
		private Sprite2D _highlighter;
		
		public override void _Ready()
		{
			_highlighter = GetNode<Sprite2D>("Highlighter");
			_groundLayer = GetNode<TileMapLayer>("../GroundLayer");
			_entityLayer = GetNode<TileMapLayer>("../EntityLayer");
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
						switch (Layer)
						{
							case SelectorLayer.Ground:
								
								break;
							case SelectorLayer.Entity:
								
								break;
							case SelectorLayer.All:
								
								break;
						}
						
						SetPosition((cellPos * Physics.Pixels) + new Vector2I(Physics.Pixels / 2, Physics.Pixels / 2));	
						Visible = true;
						
						if (mouseEvent is InputEventMouseButton mouseClick)
						{
							switch (Layer)
							{
								case SelectorLayer.Ground: 
								
									break;
								case SelectorLayer.Entity:
								
									break;
								case SelectorLayer.All:
								
								break;
							}
						}
					}
					
				}
			} else
			{
				Visible = false;
			}
		}

		public override void _Process(double delta)
		{
		}
	}
}

using Godot;
using System;
using Drumstalotajs.Mapping.Tiles.Overlays;
namespace Drumstalotajs.Mapping.Layers;

public partial class OverlayLayer : SceneLayer, ISceneLayer<OverlayTile>
{	
	public ISceneLayer<OverlayTile> AsISceneLayer => (ISceneLayer<OverlayTile>)this;

}

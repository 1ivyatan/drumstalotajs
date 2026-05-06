using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;

namespace Drumstalotajs.Mapping.Layers;

public partial class OverlayLayer : SceneLayer
{
	public async void PlaceHighlighter(Vector2I position)
	{
		await AddTile(position, "SelectorHighlight");
	}
	
	public void ClearAllHighlighters()
	{
		RemoveAllInstancesByName("SelectorHighlight");
	}
	
	public bool HasBlackTiles()
	{
		return false;
	}
	
	public void ClearAllBlackTiles()
	{
		//RemoveAllInstancesByName("SelectorHighlight");
	}
}

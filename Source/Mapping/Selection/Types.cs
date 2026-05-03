global using TileArray = Godot.Collections.Array<Drumstalotajs.Mapping.Tiles.Tile>;

global using FilteredTiles = Godot.Collections.Dictionary<
	Drumstalotajs.Mapping.Layers.BaseLayer, 
	Godot.Collections.Array
	//Godot.Collections.Array<Drumstalotajs.Mapping.Tiles.Tile>
>;

global using FilteredItemIds = Godot.Collections.Dictionary<Drumstalotajs.Mapping.Layers.BaseLayer, Godot.Collections.Array<int>>;

namespace Drumstalotajs.Mapping.Selection;

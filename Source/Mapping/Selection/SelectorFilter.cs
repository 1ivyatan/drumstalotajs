using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Selection;

public struct SelectorFilter
{
	public Layer[] Layers { get; set; }
	
	public SelectorFilter(Layer[] layers)
	{
		Layers = layers;
	}
	
	public SelectorFilter()
	{
		Layers = [];
	}
}

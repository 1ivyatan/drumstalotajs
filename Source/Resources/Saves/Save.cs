using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Resources.Saves;

[GlobalClass]
public partial class Save : Resource
{ 
	// simple props
	[Export] public string Text { get; set; } = "Hi!!!";
	[Export] public int Number { get; set; } = 123;
	
	// complex props
	[Export] public Dictionary MyDictionary { get; set; } = new();
	// this below may not necessary in practice
	[Export] public ImageTexture Image { get; set; } = null;
}

using System;
using Godot;

namespace Drumstalotajs.Utilities;

public static class Layers
{
	public static TileSetAtlasSource TintAtlasSource(TileMapLayer layer, int sourceId, Color tint)
	{
		TileSetSource source = layer.TileSet.GetSource(sourceId);
		if (source is not TileSetAtlasSource atlasSource)
		{
			return null;
		}

		TileSetAtlasSource copiedAtlasSource = (TileSetAtlasSource)atlasSource.Duplicate(true);
		if (atlasSource.Texture == null)
		{
			return copiedAtlasSource;
		}

		Image image = atlasSource.Texture.GetImage();
		image.Convert(Image.Format.Rgba8);

		for (int y = 0; y < image.GetHeight(); y++)
		{
			for (int x = 0; x < image.GetWidth(); x++)
			{
				image.SetPixel(x, y, image.GetPixel(x, y) * tint);
			}
		}

		ImageTexture tintedTexture = ImageTexture.CreateFromImage(image);
		copiedAtlasSource.Texture = tintedTexture;
		return copiedAtlasSource;
	}
}

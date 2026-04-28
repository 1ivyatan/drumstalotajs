using System;
using Godot;

namespace Drumstalotajs.Utilities;

public static class Layers
{
	public static ImageTexture GetCellTexture(TileMapLayer layer, Vector2I atlas)
	{
		if (layer == null || layer.TileSet == null || layer.TileSet.GetSourceCount() == 0)
		{
			return null;
		}

		int firstSourceId = layer.TileSet.GetSourceId(0);
		return GetCellTexture(layer, atlas, firstSourceId);
	}

	public static ImageTexture GetCellTexture(TileMapLayer layer, Vector2I atlas, int id)
	{
		if (layer == null || layer.TileSet == null)
		{
			return null;
		}

		if (id < 0)
		{
			return null;
		}

		if (atlas.X < 0 || atlas.Y < 0)
		{
			return null;
		}

		TileSetSource source = layer.TileSet.GetSource(id);
		if (source is not TileSetAtlasSource atlasSource || atlasSource.Texture == null)
		{
			return null;
		}

		Rect2I region = atlasSource.GetTileTextureRegion(atlas);
		if (region.Size.X <= 0 || region.Size.Y <= 0)
		{
			return null;
		}

		Image atlasImage = atlasSource.Texture.GetImage();
		Image tileImage = atlasImage.GetRegion(region);
		return ImageTexture.CreateFromImage(tileImage);
	}

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

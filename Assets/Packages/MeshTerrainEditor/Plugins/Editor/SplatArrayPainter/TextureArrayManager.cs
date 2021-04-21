using System.Collections.Generic;
using UnityEngine;

namespace MTE
{
    internal class TextureArrayManager
    {
        internal static Dictionary<Texture2DArray, CachedTextureArrayEntry> Cache
            = new Dictionary<Texture2DArray, CachedTextureArrayEntry>();

        public static bool IsMaterialUsingTexture(Material material, Texture texture)
        {
            if (!material || !texture)
            {
                return false;
            }

            Texture2DArray textureArray
                = material.GetTexture(SplatArrayConstants.TextureArrayPropertyName) as Texture2DArray;
            if (textureArray == null || !textureArray)
            {
                return false;
            }

            CachedTextureArrayEntry entry;
            if (!Cache.TryGetValue(textureArray, out entry))
            {
                return false;
            }

            return entry.Textures.Contains(texture);
        }
    }

    internal class CachedTextureArrayEntry
    {
        public Texture2DArray TextureArray { get; }
        public List<Texture> Textures { get; private set; }

        public CachedTextureArrayEntry(Texture2DArray array)
        {
            TextureArray = array;
        }

        public void RebuildTextures()
        {
            if (!TextureArray)
            {//missing reference: destroyed by editor or user, clear from cache
                Textures?.Clear();
                TextureArrayManager.Cache.Remove(TextureArray);
                return;
            }
            var arrayLength = TextureArray.depth;
            var textureWidth = TextureArray.width;
            var textureHeight = TextureArray.height;
            if (Textures == null)
            {
                Textures = new List<Texture>(arrayLength);
            }
            Textures.Resize(arrayLength);

            for (int i = 0; i < arrayLength; i++)
            {
                var pixels = TextureArray.GetPixels32(i);
                Texture2D texture = new Texture2D(textureWidth, textureHeight);
                texture.SetPixels32(pixels);
                texture.Apply();
                Textures[i] = texture;
            }

            Textures.RemoveAll(t => !t);
        }
    }
}
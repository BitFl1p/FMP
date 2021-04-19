using UnityEditor;
using UnityEngine;

namespace MTE
{
    internal abstract class Preview
    {
        public bool IsReady = false;

        public abstract void CreatePreviewObject();

        public abstract void SetPreviewTexture(Vector2 textureScale,
            Texture texture, Texture normalTexture);

        public abstract void SetPreviewMaskTexture(int maskIndex);

        public abstract void SetPreviewSize(float value);

        public void MoveTo(Vector3 worldPosition)
        {
            if(!previewObj) return;
            previewObj.transform.position = worldPosition;
        }

        public abstract void SetNormalizedBrushCenter(Vector2 normalizedBrushCenter);

        public abstract void SetNormalizedBrushSize(float normalizeBrushSize);

        public abstract void LoadPreview(Texture texture, float brushSizeInU3D, int brushIndex);

        public virtual void LoadPreview(Texture texture, float brushSizeInU3D, int brushIndex,
            GameObject target)
        {
            LoadPreview(texture, brushSizeInU3D, brushIndex);
        }

        internal void UnLoadPreview()
        {
            if (previewObj != null)
            {
                UnityEngine.Object.DestroyImmediate(previewObj);
                previewObj = null;
            }
            IsReady = false;
        }
        
        protected GameObject previewObj;
        
        protected virtual Vector2 GetPreviewSplatTextureScale(Material material, int splatIndex)
        {
            var splatXName = "_Splat" + splatIndex;
            if (material.HasProperty(splatXName))
            {
                return material.GetTextureScale(splatXName);
            }

            if (0 <= splatIndex && splatIndex <= 3)
            {
                var packedSplatName = "_PackedSplat0";
                if (material.HasProperty(packedSplatName))
                {
                    return material.GetTextureScale(packedSplatName);
                }
            }
            else if (4 <= splatIndex && splatIndex <= 7)
            {
                var packedSplatName = "_PackedSplat3";
                if (material.HasProperty(packedSplatName))
                {
                    return material.GetTextureScale(packedSplatName);
                }
            }

            return new Vector2(10, 10);
        }
    }
}
using UnityEngine;
using UnityEditor;

namespace MTE
{
    internal class ArrayTexturePreview : BuiltinPreview
    {
        public override void LoadPreview(Texture texture, float brushSizeInU3D, int brushIndex)
        {
            int splatIndex = -1;
            Material material = null;
            var target = Selection.activeGameObject;
            var meshRenderer = target.GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                return;
            }

            var m = meshRenderer.sharedMaterial;
            if (m == null)
            {
                return;
            }

            if (TextureArrayManager.IsMaterialUsingTexture(m, texture))
            {
                material = m;
            }

            if (material == null)
            {
                throw new System.InvalidOperationException(
                    "[MTE] Failed to load texture in to preview." +
                    " The selected texture is not used in selected target GameObjects' material.");
            }

            UnLoadPreview();

            CreatePreviewObject();

            previewObj.hideFlags = HideFlags.HideAndDontSave;

            var textureScale = GetPreviewSplatTextureScale(material, splatIndex);
            Texture normalTexture = null;//TODO When texture array of normal is ready, implement it.
            if (material.HasProperty("_Normal" + splatIndex))
            {
                normalTexture = material.GetTexture("_Normal" + splatIndex);
            }

            SetPreviewTexture(textureScale, texture, normalTexture);
            SetPreviewSize(brushSizeInU3D/2);
            SetPreviewMaskTexture(brushIndex);
            IsReady = true;
        }
    }
}
using UnityEngine;
using UnityEditor;

namespace MTE
{
    internal class BuiltinPreview : Preview
    {
        
        public override void CreatePreviewObject()
        {
            var shader = Shader.Find("Hidden/MTE/PaintTexturePreview");
            previewObj = new GameObject("MTEPreview");
            var projector = previewObj.AddComponent<Projector>();
            projector.material = new Material(shader);
            projector.orthographic = true;
            projector.nearClipPlane = -1000;
            projector.farClipPlane = 1000;
            projector.transform.Rotate(90, 0, 0);
        }

        //TODO refactor: LoadPreview() is duplicated in SRPPreview
        public override void LoadPreview(Texture texture, float brushSizeInU3D, int brushIndex)
        {
            int splatIndex = -1;
            Material material = null;
            foreach (var target in MTEContext.Targets)
            {
                var meshRenderer = target.GetComponent<MeshRenderer>();
                if (meshRenderer == null)
                {
                    continue;
                }
                var m = meshRenderer.sharedMaterial;
                if (m == null)
                {
                    continue;
                }

                if ((splatIndex = m.FindSplatTexture(texture)) >= 0)
                {
                    material = m;
                    break;
                }

            }
            if (material == null)
            {
                throw new System.InvalidOperationException(
                    "[MTE] Failed to load texture in to preview. The selected texture doesn't exist in any target GameObjects' material. Try refreshing the filter.");
            }

            UnLoadPreview();

            CreatePreviewObject();

            previewObj.hideFlags = HideFlags.HideAndDontSave;

            var textureScale = GetPreviewSplatTextureScale(material, splatIndex);
            Texture normalTexture = null;
            if (material.HasProperty("_Normal" + splatIndex))
            {
                normalTexture = material.GetTexture("_Normal" + splatIndex);
            }

            SetPreviewTexture(textureScale, texture, normalTexture);
            SetPreviewSize(brushSizeInU3D/2);
            SetPreviewMaskTexture(brushIndex);
            IsReady = true;
        }

        public override void LoadPreview(Texture texture, float brushSizeInU3D, int brushIndex,
            GameObject target)
        {
            if (target == null)
            {
                MTEDebug.LogError("Cannot load preview from an invalid GameObject.");
            }

            int splatIndex = -1;
            Material material = null;
            do
            {
                var meshRenderer = target.GetComponent<MeshRenderer>();
                if (meshRenderer == null)
                {
                    continue;
                }

                var m = meshRenderer.sharedMaterial;
                if (m == null)
                {
                    continue;
                }

                if ((splatIndex = m.FindSplatTexture(texture)) >= 0)
                {
                    material = m;
                    break;
                }

            } while (false);
            if (material == null)
            {
                throw new System.InvalidOperationException(
                    "[MTE] Failed to load texture in to preview. The selected texture doesn't exist in any target GameObjects' material. Try refreshing the filter.");
            }

            UnLoadPreview();

            CreatePreviewObject();

            previewObj.hideFlags = HideFlags.HideAndDontSave;

            var textureScale = GetPreviewSplatTextureScale(material, splatIndex);
            Texture normalTexture = null;
            if (material.HasProperty("_Normal" + splatIndex))
            {
                normalTexture = material.GetTexture("_Normal" + splatIndex);
            }

            SetPreviewTexture(textureScale, texture, normalTexture);
            SetPreviewSize(brushSizeInU3D/2);
            SetPreviewMaskTexture(brushIndex);
            IsReady = true;
        }

        public override void SetNormalizedBrushCenter(Vector2 normalizedBrushCenter)
        {
        }

        public override void SetNormalizedBrushSize(float normalizeBrushSize)
        {
        }

        public override void SetPreviewTexture(Vector2 textureScale, Texture texture, Texture normalTexture)
        {
            if(!previewObj) return;
            var projector = previewObj.GetComponent<Projector>();
            projector.material.SetTexture("_MainTex", texture);
            projector.material.SetTextureScale("_MainTex", textureScale);
            projector.material.SetTexture("_NormalTex", normalTexture);
            SceneView.RepaintAll();
        }

        public override void SetPreviewMaskTexture(int maskIndex)
        {
            if(!previewObj) return;
            var projector = previewObj.GetComponent<Projector>();
            projector.material.SetTexture("_MaskTex", MTEStyles.brushTextures[maskIndex]);
            projector.material.SetTextureScale("_MaskTex", Vector2.one);
            SceneView.RepaintAll();
        }

        public override void SetPreviewSize(float value)
        {
            if(!previewObj) return;
            var projector = previewObj.GetComponent<Projector>();
            projector.orthographicSize = value;
            SceneView.RepaintAll();
        }
    }
}
using UnityEngine;
using UnityEditor;

namespace MTE
{
    internal class SRPPreview : Preview
    {
        private RenderPipeline lastRenderPipeline = RenderPipeline.NotDetermined;

        public void LoadShader()
        {
            if (shader != null && RenderPipelineUtil.Current == lastRenderPipeline)
            {
                return;
            }

            lastRenderPipeline = RenderPipelineUtil.Current;

            var lwrpShaderRelativePath = Utility.GetUnityPath(Res.ShaderDir + "PaintTexturePreview_LWRP.shader");
            var urpShaderRelativePath = Utility.GetUnityPath(Res.ShaderDir + "PaintTexturePreview_URP.shader");

            switch (RenderPipelineUtil.Current)
            {
                case RenderPipeline.LWRP:
                    this.shader = AssetDatabase.LoadAssetAtPath<Shader>(lwrpShaderRelativePath);
                    if (shader == null)
                    {
                        MTEDebug.LogError("MTE Preview shader for LWRP is not found.");
                    }
                    else
                    {
                        MTEDebug.Log("Loaded Preview shader for LWRP.");
                    }
                    break;
                case RenderPipeline.URP:
                    this.shader = AssetDatabase.LoadAssetAtPath<Shader>(urpShaderRelativePath);
                    if (shader == null)
                    {
                        MTEDebug.LogError("MTE Preview shader for URP is not found.");
                    }
                    else
                    {
                        MTEDebug.Log("Loaded Preview shader for URP.");
                    }
                    break;
                //fallback to URP
                case RenderPipeline.HDRP://HDRP is not supported yet.
                default:
                    this.shader = AssetDatabase.LoadAssetAtPath<Shader>(urpShaderRelativePath);
                    if (shader == null)
                    {
                        MTEDebug.LogError("MTE Preview shader for URP (fallback) is not found.");
                    }
                    else
                    {
                        MTEDebug.Log("Loaded Preview shader for URP (fallback).");
                    }
                    break;
            }
        }

        public override void LoadPreview(Texture texture, float brushSizeInU3D, int brushIndex)
        {
            LoadShader();

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

        public override void CreatePreviewObject()
        {
            previewObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var boxCollider = previewObj.GetComponent<BoxCollider>();
            Object.DestroyImmediate(boxCollider);
            previewObj.name = "MTEPreview";

            var meshRenderer = previewObj.GetComponent<MeshRenderer>();
            var material = new Material(shader);
            meshRenderer.sharedMaterial = material;

            previewObj.transform.eulerAngles = new Vector3(90, 0, 0);
        }

        public override void SetNormalizedBrushCenter(Vector2 normalizedBrushCenter)
        {
            var renderer = previewObj.GetComponent<MeshRenderer>();
            renderer.sharedMaterial.SetVector("_BrushCenter", normalizedBrushCenter);
        }

        public override void SetNormalizedBrushSize(float normalizeBrushSize)
        {
            var renderer = previewObj.GetComponent<MeshRenderer>();
            renderer.sharedMaterial.SetFloat("_NormalizedBrushSize", normalizeBrushSize);
        }

        public override void SetPreviewTexture(Vector2 textureScale,
            Texture texture, Texture normalTexture)
        {
            if(!previewObj) return;
            var renderer = previewObj.GetComponent<MeshRenderer>();
            renderer.sharedMaterial.SetTexture("_MainTex", texture);
            renderer.sharedMaterial.SetTextureScale("_MainTex", textureScale);
            renderer.sharedMaterial.SetTexture("_NormalTex", normalTexture);
            SceneView.RepaintAll();
        }

        public override void SetPreviewMaskTexture(int maskIndex)
        {
            if(!previewObj) return;
            var renderer = previewObj.GetComponent<MeshRenderer>();
            renderer.sharedMaterial.SetTexture("_MaskTex", MTEStyles.brushTextures[maskIndex]);
            renderer.sharedMaterial.SetTextureScale("_MaskTex", Vector2.one);
            SceneView.RepaintAll();
        }

        public override void SetPreviewSize(float halfBrushSizeInUnityUnit)
        {
            if(!previewObj) return;
            previewObj.transform.localScale = new Vector3(
                halfBrushSizeInUnityUnit*2,
                halfBrushSizeInUnityUnit*2, 10000);
            
            SceneView.RepaintAll();
        }

        private Shader shader;
    }
}
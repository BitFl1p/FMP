using System.Collections.Generic;
using System.IO;
using MTE.Undo;
using UnityEditor;
using UnityEngine;
using static MTE.SplatArrayConstants;
using MTE.Internal;

namespace MTE
{
    internal static class SplatArrayConstants
    {
        public const string TextureArrayPropertyName = "_SplatArray";

        public static readonly string[] ControlTexturePropertyNames
            = {"_Control0", "_Control1", "_Control2"};
    }
    
    /// <summary>
    /// Splat texture-array editor
    /// </summary>
    /// <remarks>
    /// naming convention:
    ///     control textures, "_Control0/1/2"
    ///     splat albedo texture-array, "_SplatArray"
    /// </remarks>
    internal class SplatArrayPainter : IEditor
    {
        public int Id { get; } = 10;

        public bool Enabled { get; set; } = true;

        public string Name { get; } = "SplatArrayPainter";

        public Texture Icon { get; } =
            EditorGUIUtility.IconContent("TerrainInspector.TerrainToolSplat").image;

        public string Header { get { return StringTable.Get(C.SplatArrayPainter_Header); } }

        public string Description { get { return StringTable.Get(C.SplatArrayPainter_Description); } }

        public bool WantMouseMove { get; } = true;

        public bool WillEditMesh { get; } = false;


        #region Parameters

        #region Constant
        // default
        const float DefaultBrushSize = 1;
        const float DefaultBrushFlow = 0.5f;
        // min/max
        const float MinBrushSize = 0.1f;
        const float MaxBrushSize = 10f;
        const float MinBrushFlow = 0.01f;
        const float MaxBrushFlow = 1f;
        const int MaxHotkeyNumberForTexture = 8;
        #endregion

        public int brushIndex;
        public float brushSize;
        public float brushFlow;
        private int selectedTextureIndex;

        /// <summary>
        /// Index of selected texture in the texture list; not the layer index.
        /// </summary>
        public int SelectedTextureIndex
        {
            get { return this.selectedTextureIndex; }
            set
            {
                var textureListCount = TextureList.Count;
                if (value < textureListCount)
                {
                    this.selectedTextureIndex = value;
                }
            }
        }

        /// <summary>
        /// Index of selected brush
        /// </summary>
        public int BrushIndex
        {
            get { return brushIndex; }
            set
            {
                if (brushIndex != value)
                {
                    preview.SetPreviewMaskTexture(value);

                    brushIndex = value;
                }
            }
        }


        /// <summary>
        /// Brush size (unit: 1 BrushUnit)
        /// </summary>
        public float BrushSize
        {
            get { return brushSize; }
            set
            {
                value = Mathf.Clamp(value, MinBrushSize, MaxBrushSize);

                if (!MathEx.AmostEqual(brushSize, value))
                {
                    brushSize = value;

                    EditorPrefs.SetFloat("MTE_SplatArrayPainter.brushSize", value);
                    {
                        //preview size for SelectedGameObject mode are set in OnSceneGUI
                    }
                }
            }
        }

        //real brush size
        private float BrushSizeInU3D { get { return BrushSize * Settings.BrushUnit; } }

        /// <summary>
        /// Brush flow
        /// </summary>
        public float BrushFlow
        {
            get { return brushFlow; }
            set
            {
                value = Mathf.Clamp(value, MinBrushFlow, MaxBrushFlow);
                if (Mathf.Abs(brushFlow - value) > 0.0001f)
                {
                    brushFlow = value;
                    EditorPrefs.SetFloat("MTE_SplatArrayPainter.brushFlow", value);
                }
            }
        }
        #endregion

        public SplatArrayPainter()
        {
            MTEContext.EnableEvent += (sender, args) =>
            {
                if (MTEContext.editor == this)
                {
                    LoadSavedParamter();
                    LoadTextureList();
                    BuildEditingInfoForLegacyMode(Selection.activeGameObject);
                    if (TextureList.Count != 0)
                    {
                        if (SelectedTextureIndex < 0)
                        {
                            SelectedTextureIndex = 0;
                        }
                        preview.LoadPreview(TextureList[SelectedTextureIndex],
                            BrushSizeInU3D,
                            BrushIndex);
                    }
                }
            };

            MTEContext.EditTypeChangedEvent += (sender, args) =>
            {
                if (MTEContext.editor == this)
                {
                    LoadSavedParamter();
                    LoadTextureList();
                    BuildEditingInfoForLegacyMode(Selection.activeGameObject);
                    if (TextureList.Count != 0)
                    {
                        if (SelectedTextureIndex < 0 || SelectedTextureIndex > TextureList.Count - 1)
                        {
                            SelectedTextureIndex = 0;
                        }
                        preview.LoadPreview(TextureList[SelectedTextureIndex],
                            BrushSizeInU3D,
                            BrushIndex);
                    }
                }
                else
                {
                    if (preview != null)
                    {
                        preview.UnLoadPreview();
                    }
                }
            };

            MTEContext.SelectionChangedEvent += (sender, args) =>
            {
                if (args.SelectedGameObject)
                {
                    BuildEditingInfoForLegacyMode(args.SelectedGameObject);
                }
            };

            MTEContext.TextureChangedEvent += (sender, args) =>
            {
                if (MTEContext.editor == this)
                {
                    LoadTextureList();
                    BuildEditingInfoForLegacyMode(Selection.activeGameObject);
                }
            };

            MTEContext.DisableEvent += (sender, args) =>
            {
                if (preview != null)
                {
                    preview.UnLoadPreview();
                }
            };

            MTEContext.EditTargetsLoadedEvent += (sender, args) =>
            {
                if (MTEContext.editor == this)
                {
                    LoadTextureList();
                }
            };

            // Load default parameters
            brushSize = DefaultBrushSize;
            brushFlow = DefaultBrushFlow;
        }

        private void LoadSavedParamter()
        {
            brushSize = EditorPrefs.GetFloat("MTE_SplatArrayPainter.brushSize", DefaultBrushSize);
            brushFlow = EditorPrefs.GetFloat("MTE_SplatArrayPainter.brushFlow", DefaultBrushFlow);
        }
        
        private GameObject targetGameObject { get; set; }
        private Mesh targetMesh { get; set; }
        private Material targetMaterial { get; set; }
        private Texture2D[] controlTextures { get; } = new Texture2D[3] {null, null, null};
        private void BuildEditingInfoForLegacyMode(GameObject gameObject)
        {
            //reset
            this.TextureList.Clear();
            this.targetGameObject = null;
            this.targetMaterial = null;
            this.targetMesh = null;

            //check gameObject
            if (!gameObject)
            {
                return;
            }
            var meshFilter = gameObject.GetComponent<MeshFilter>();
            if (!meshFilter)
            {
                return;
            }
            var meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (!meshRenderer)
            {
                return;
            }
            var material = meshRenderer.sharedMaterial;
            if (!material)
            {
                return;
            }
            if (!MTEShaders.IsMTETextureArrayShader(material.shader))
            {
                return;
            }

            //collect targets info
            this.targetGameObject = gameObject;
            this.targetMaterial = material;
            this.targetMesh = meshFilter.sharedMesh;
            // Texture
            LoadTextureList();
            LoadControlTextures();
            // Preview
            if (TextureList.Count != 0)
            {
                if (SelectedTextureIndex < 0 || SelectedTextureIndex > TextureList.Count - 1)
                {
                    SelectedTextureIndex = 0;
                }
                preview.LoadPreview(TextureList[SelectedTextureIndex],
                    BrushSizeInU3D,
                    BrushIndex);
            }
        }
        
        private static class Styles
        {
            public static string NoGameObjectSelectedHintText;

            private static bool unloaded= true;

            public static void Init()
            {
                if (!unloaded) return;
                NoGameObjectSelectedHintText
                    = StringTable.Get(C.Info_PleaseSelectAGameObjectWithVaildMesh);
                unloaded = false;
            }
        }

        public void DoArgsGUI()
        {
            Styles.Init();
            
            if (Selection.activeGameObject == null)
            {
                EditorGUILayout.HelpBox(Styles.NoGameObjectSelectedHintText, MessageType.Warning);
                return;
            }

            BrushIndex = Utility.ShowBrushes(BrushIndex);

            // Splat-textures
            if (!Settings.CompactGUI)
            {
                GUILayout.Label(StringTable.Get(C.Textures), MTEStyles.SubHeader);
            }
            EditorGUILayout.BeginVertical("box");
            {
                var textureListCount = TextureList.Count;
                if (textureListCount == 0)
                {
                    EditorGUILayout.LabelField(
                        StringTable.Get(C.Info_SplatArrayPainter_NoSplatTextureFoundOnSelectedObject),
                        GUILayout.Height(64));
                }
                else
                {
                    for (int i = 0; i < textureListCount; i += 4)
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            var oldBgColor = GUI.backgroundColor;
                            for (int j = 0; j < 4; j++)
                            {
                                if (i + j >= textureListCount) break;

                                EditorGUILayout.BeginVertical();
                                var texture = TextureList[i + j];
                                bool toggleOn = SelectedTextureIndex == i + j;
                                if (toggleOn)
                                {
                                    GUI.backgroundColor = new Color(62 / 255.0f, 125 / 255.0f, 231 / 255.0f);
                                }

                                GUIContent toggleContent;
                                if (i + j + 1 <= MaxHotkeyNumberForTexture)
                                {
                                    toggleContent = new GUIContent(texture,
                                        StringTable.Get(C.Hotkey) + ':' + StringTable.Get(C.NumPad) + (i + j + 1));
                                }
                                else
                                {
                                    toggleContent = new GUIContent(texture);
                                }

                                var new_toggleOn = GUILayout.Toggle(toggleOn,
                                    toggleContent, GUI.skin.button,
                                    GUILayout.Width(64), GUILayout.Height(64));
                                GUI.backgroundColor = oldBgColor;
                                if (new_toggleOn && !toggleOn)
                                {
                                    SelectedTextureIndex = i + j;
                                    // reload the preview
                                    preview.LoadPreview(texture, BrushSizeInU3D, BrushIndex);
                                }
                                EditorGUILayout.EndVertical();
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            EditorGUILayout.EndVertical();

            //Settings
            if (!Settings.CompactGUI)
            {
                EditorGUILayout.Space();
                GUILayout.Label(StringTable.Get(C.Settings), MTEStyles.SubHeader);
            }
            BrushSize = EditorGUILayoutEx.Slider(StringTable.Get(C.Size), "-", "+", BrushSize, MinBrushSize, MaxBrushSize);
            BrushFlow = EditorGUILayoutEx.SliderLog10(StringTable.Get(C.Flow), "[", "]", BrushFlow, MinBrushFlow, MaxBrushFlow);

            //Tools
            if (!Settings.CompactGUI)
            {
                EditorGUILayout.Space();
                GUILayout.Label(StringTable.Get(C.Tools), MTEStyles.SubHeader);
            }
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button(StringTable.Get(C.CreateTexture2DArray),
                        GUILayout.Width(100), GUILayout.Height(40)))
                    {
                        Texture2DArrayGeneratorEditor.Open();
                    }
                    GUILayout.Space(20);
                    EditorGUILayout.LabelField(
                        StringTable.Get(C.Info_ToolDescription_CreateTexture2DArray),
                        MTEStyles.labelFieldWordwrap);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();

            GUILayout.FlexibleSpace();
            EditorGUILayout.HelpBox(StringTable.Get(C.Info_WillBeSavedInstantly),
                MessageType.Info, true);
        }
        
        public HashSet<Hotkey> DefineHotkeys()
        {
            var hashSet = new HashSet<Hotkey>
            {
                new Hotkey(this, KeyCode.Minus, () =>
                {
                    BrushSize -= 1;
                    MTEEditorWindow.Instance.Repaint();
                }),
                new Hotkey(this, KeyCode.Equals, () =>
                {
                    BrushSize += 1;
                    MTEEditorWindow.Instance.Repaint();
                }),
                new Hotkey(this, KeyCode.LeftBracket, () =>
                {
                    BrushFlow -= 0.01f;
                    MTEEditorWindow.Instance.Repaint();
                }),
                new Hotkey(this, KeyCode.RightBracket, () =>
                {
                    BrushFlow += 0.01f;
                    MTEEditorWindow.Instance.Repaint();
                }),
            };

            for (int i = 0; i < MaxHotkeyNumberForTexture; i++)
            {
                int index = i;
                var hotkey = new Hotkey(this, KeyCode.Keypad0+index+1, () =>
                {
                    SelectedTextureIndex = index;
                    // reload the preview
                    preview.LoadPreview(TextureList[SelectedTextureIndex], BrushSizeInU3D, BrushIndex);
                    MTEEditorWindow.Instance.Repaint();
                });
                hashSet.Add(hotkey);
            }

            return hashSet;
        }

        // buffers of editing helpers
        private float[] BrushStrength = new float[1024 * 1024];//buffer for brush blending to forbid re-allocate big array every frame when painting.

        public void OnSceneGUI()
        {
            var e = Event.current;

            if (preview == null || !preview.IsReady || TextureList.Count == 0)
            {
                return;
            }

            if (e.commandName == "UndoRedoPerformed")
            {
                SceneView.RepaintAll();
                return;
            }

            if (!(EditorWindow.mouseOverWindow is SceneView))
            {
                return;
            }

            // do nothing when mouse middle/right button, control/alt key is pressed
            if (e.button != 0 || e.control || e.alt)
                return;

            HandleUtility.AddDefaultControl(0);
            var ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            RaycastHit raycastHit;

            if (!targetGameObject || !targetMaterial || !targetMesh)
            {
                return;
            }

            if (!Physics.Raycast(ray, out raycastHit, Mathf.Infinity, ~targetGameObject.layer))
            {
                return;
            }
            
            var currentBrushSize = BrushSizeInU3D/2;

            if (Settings.ShowBrushRect)
            {
                Utility.ShowBrushRect(raycastHit.point, currentBrushSize);
            }

            var controlIndex = SelectedTextureIndex / 4;
            Debug.Assert(0 <= controlIndex && controlIndex <= 3);
            var controlTexture = controlTextures[controlIndex];
            var controlWidth = controlTexture.width;
            var controlHeight = controlTexture.height;
            var meshSize = targetGameObject.GetComponent<MeshRenderer>().bounds.size.x;
            var brushSizeInTexel = (int) Mathf.Round(BrushSizeInU3D/meshSize*controlWidth);
            preview.SetNormalizedBrushSize(BrushSizeInU3D/meshSize);
            preview.SetNormalizedBrushCenter(raycastHit.textureCoord);
            preview.SetPreviewSize(BrushSizeInU3D/2);
            preview.MoveTo(raycastHit.point);
            SceneView.RepaintAll();

            if ((e.type == EventType.MouseDrag && e.alt == false && e.shift == false && e.button == 0) ||
                (e.type == EventType.MouseDown && e.shift == false && e.alt == false && e.button == 0))
            {
                // 1. Collect all sections to be modified
                var sections = new List<Color[]>();

                var pixelUV = raycastHit.textureCoord;
                var pX = Mathf.FloorToInt(pixelUV.x * controlWidth);
                var pY = Mathf.FloorToInt(pixelUV.y * controlHeight);
                var x = Mathf.Clamp(pX - brushSizeInTexel / 2, 0, controlWidth - 1);
                var y = Mathf.Clamp(pY - brushSizeInTexel / 2, 0, controlHeight - 1);
                var width = Mathf.Clamp((pX + brushSizeInTexel / 2), 0, controlWidth) - x;
                var height = Mathf.Clamp((pY + brushSizeInTexel / 2), 0, controlHeight) - y;

                for (var i = 0; i < controlTextures.Length; i++)
                {
                    var texture = controlTextures[i];
                    if (texture == null) continue;
                    sections.Add(texture.GetPixels(x, y, width, height, 0));
                }

                // 2. Modify target
                var replaced = sections[controlIndex];
                var maskTexture = (Texture2D) MTEStyles.brushTextures[BrushIndex];
                BrushStrength = new float[brushSizeInTexel * brushSizeInTexel];
                for (var i = 0; i < brushSizeInTexel; i++)
                {
                    for (var j = 0; j < brushSizeInTexel; j++)
                    {
                        BrushStrength[j * brushSizeInTexel + i] =
                            maskTexture.GetPixelBilinear(((float) i) / brushSizeInTexel,
                                ((float) j) / brushSizeInTexel).a;
                    }
                }

                var controlColor = new Color();
                controlColor[SelectedTextureIndex % 4] = 1.0f;
                for (var i = 0; i < height; i++)
                {
                    for (var j = 0; j < width; j++)
                    {
                        var index = (i * width) + j;
                        var Stronger =
                            BrushStrength[
                                Mathf.Clamp((y + i) - (pY - brushSizeInTexel / 2), 0,
                                    brushSizeInTexel - 1) *
                                brushSizeInTexel +
                                Mathf.Clamp((x + j) - (pX - brushSizeInTexel / 2), 0,
                                    brushSizeInTexel - 1)] *
                            BrushFlow;
                        replaced[index] = Color.Lerp(replaced[index], controlColor, Stronger);
                    }
                }

                if (e.type == EventType.MouseDown)
                {
                    using (new UndoTransaction())
                    {
                        var material = targetMaterial;
                        if (material.HasProperty(ControlTexturePropertyNames[0]))
                        {
                            Texture2D texture = (Texture2D) material.GetTexture(ControlTexturePropertyNames[0]);
                            if (texture != null)
                            {
                                var originalColors = texture.GetPixels();
                                UndoRedoManager.Instance().Push(a =>
                                {
                                    texture.ModifyPixels(a);
                                    texture.Apply();
                                    Save(texture);
                                }, originalColors, "Paint control texture");
                            }
                        }

                        if (material.HasProperty(ControlTexturePropertyNames[1]))
                        {
                            Texture2D texture = (Texture2D) material.GetTexture(ControlTexturePropertyNames[1]);
                            if (texture != null)
                            {
                                var originalColors = texture.GetPixels();
                                UndoRedoManager.Instance().Push(a =>
                                {
                                    texture.ModifyPixels(a);
                                    texture.Apply();
                                    Save(texture);
                                }, originalColors, "Paint control texture");
                            }
                        }

                        if (material.HasProperty(ControlTexturePropertyNames[2]))
                        {
                            Texture2D texture = (Texture2D) material.GetTexture(ControlTexturePropertyNames[2]);
                            if (texture != null)
                            {
                                var originalColors = texture.GetPixels();
                                UndoRedoManager.Instance().Push(a =>
                                {
                                    texture.ModifyPixels(a);
                                    texture.Apply();
                                    Save(texture);
                                }, originalColors, "Paint control texture");
                            }
                        }
                    }
                }

                controlTexture.SetPixels(x, y, width, height, replaced);
                controlTexture.Apply();

                // 3. Normalize other control textures
                NormalizeWeightsLegacy(sections);
                for (var i = 0; i < controlTextures.Length; i++)
                {
                    var texture = controlTextures[i];
                    if (texture == null)
                    {
                        continue;
                    }

                    if (texture == controlTexture)
                    {
                        continue;
                    }

                    texture.SetPixels(x, y, width, height, sections[i]);
                    texture.Apply();
                }
            }
            else if (e.type == EventType.MouseUp && e.alt == false && e.button == 0)
            {
                foreach (var texture in controlTextures)
                {
                    if (texture)
                    {
                        Save(texture);
                    }
                }
            }

            SceneView.RepaintAll();
        }

        private void NormalizeWeightsLegacy(List<Color[]> sections)
        {
            var colorCount = sections[0].Length;
            for (var i = 0; i < colorCount; i++)
            {
                var total = 0f;
                for (var j = 0; j < sections.Count; j++)
                {
                    var color = sections[j][i];
                    total += color[0] + color[1] + color[2] + color[3];
                    if(j == SelectedTextureIndex/4)
                    {
                        total -= color[SelectedTextureIndex%4];
                    }
                }
                if(total > 0.01)
                {
                    var a = sections[SelectedTextureIndex/4][i][SelectedTextureIndex%4];
                    var k = (1 - a)/total;

                    for (var j = 0; j < sections.Count; j++)
                    {
                        for (var l = 0; l < 4; l++)
                        {
                            if(!(j == SelectedTextureIndex/4 && l == SelectedTextureIndex%4))
                            {
                                sections[j][i][l] *= k;
                            }
                        }
                    }
                }
                else
                {
                    for (var j = 0; j < sections.Count; j++)
                    {
                        sections[j][i][SelectedTextureIndex%4] = (j != SelectedTextureIndex/4) ? 0 : 1;
                    }
                }
            }
        }

        private static void Save(Texture2D texture)
        {
            if(texture == null)
            {
                throw new System.ArgumentNullException("texture");
            }
            var path = AssetDatabase.GetAssetPath(texture);
            var bytes = texture.EncodeToPNG();
            if(bytes == null || bytes.Length == 0)
            {
                throw new System.Exception("[MTE] Failed to save texture to png file.");
            }
            File.WriteAllBytes(path, bytes);
            MTEDebug.LogFormat("Texture<{0}> saved to <{1}>.", texture.name, path);
        }

        private Preview preview
        {
            get
            {
                if (RenderPipelineUtil.Current == RenderPipeline.Builtin)
                {
                    return this.builtinPreview;
                }

                return this.srpPreview;
            }
        }
        ArrayTexturePreview builtinPreview = new ArrayTexturePreview();
        SRPPreview srpPreview = new SRPPreview();//FIXME override SRPPreview.LoadPreview

        private List<Texture> TextureList = new List<Texture>(16);

        /// <summary>
        /// load all splat textures form targets
        /// </summary>
        private void LoadTextureList()
        {
            TextureList.Clear();
            
            MTEDebug.Log("Loading layer textures on selected GameObject...");
            LoadTargetTextures(targetGameObject);
            //A texture-array is always readable.
            MTEDebug.LogFormat("{0} layer textures loaded.", TextureList.Count);
        }

        private void LoadTargetTextures(GameObject target)
        {
            if (!target)
            {
                return;
            }
            var meshRenderer = target.GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                return;
            }
            var material = meshRenderer.sharedMaterial;

            if (!material)
            {
                return;
            }

            if (!CheckIfMaterialAssetPathAvailable(material))
            {
                return;
            }

            Shader shader = material.shader;
            if (shader == null)
            {
                MTEDebug.LogWarning($"Material<{material.name}> doesn't use a valid shader!");
                return;
            }

            if (!shader.name.Contains("MTE") || !shader.name.Contains("TextureArray"))
            {
                MTEDebug.LogWarning(
                    $"Material<{material.name}> doesn't use a MTE TextureArray shader!");
                return;
            }

            var propertyCount = ShaderUtil.GetPropertyCount(shader);
            for (int j = 0; j < propertyCount; j++)
            {
                if (ShaderUtil.GetPropertyType(shader, j) == ShaderUtil.ShaderPropertyType.TexEnv)
                {
                    var propertyName = ShaderUtil.GetPropertyName(shader, j); //propertyName should be _Splat0/1/2/3/4
                    if (propertyName == TextureArrayPropertyName)
                    {
                        var textureArray =
                            material.GetTexture(TextureArrayPropertyName) as Texture2DArray;
                        if (textureArray != null && textureArray.depth > 0)
                        {
                            //TODO refactor this for possible NRE
                            CachedTextureArrayEntry entry;
                            if (!TextureArrayManager.Cache.TryGetValue(textureArray, out entry))
                            {
                                entry = new CachedTextureArrayEntry(textureArray);
                                entry.RebuildTextures();
                                if (entry.Textures != null && entry.Textures.Count > 0)
                                {
                                    TextureArrayManager.Cache.Add(textureArray, entry);
                                }
                            }

                            if (entry.Textures != null)
                            {
                                TextureList.AddRange(entry.Textures);
                            }
                        }
                    }
                }
            }
        }


        private void LoadControlTextures()
        {
            if (!targetMaterial)
            {
                return;
            }

            for (int i = 0; i < ControlTexturePropertyNames.Length; i++)
            {
                var controlPropertyName = ControlTexturePropertyNames[i];
                var controlTexture = targetMaterial.GetTexture(controlPropertyName) as Texture2D;
                if (controlTexture == null)
                {
                    MTEDebug.LogWarning(
                        $"[MTE] \"{controlPropertyName}\" is not assigned" +
                        $" or existing in material<{targetMaterial.name}>.");
                }
                controlTextures[i] = controlTexture;
            }
        }

        private static bool CheckIfMaterialAssetPathAvailable(Material material)
        {
            var relativePathOfMaterial = AssetDatabase.GetAssetPath(material);
            if (relativePathOfMaterial.StartsWith("Resources"))
            {//built-in material
                return false;
            }
            return true;
        }
    }
}
namespace ShaderToolboxPro.URP
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEditor.Rendering.Universal.ShaderGUI;
    using UnityEngine;

    public class DefaultLitShaderGUI : ToolboxShaderGUI
    {
        MaterialProperty baseColorProp = null;
        const string baseColorName = "_BaseColor";
        const string baseColorLabel = "Base Color";
        const string baseColorTooltip = "Albedo color applied to entire mesh.";

        MaterialProperty baseTexProp = null;
        const string baseTexName = "_BaseMap";
        const string baseTexLabel = "Base Texture";
        const string baseTexTooltip = "Albedo texture applied to entire mesh.";

        MaterialProperty workflowModeProp = null;
        const string workflowModeName = "_WorkflowMode";
        const string workflowModeLabel = "Workflow Mode";
        const string workflowModeTooltip = "";

        MaterialProperty metallicProp = null;
        const string metallicName = "_Metallic";
        MaterialProperty metallicTexProp = null;
        const string metallicTexName = "_MetallicGlossMap";
        const string metallicLabel = "Metallic";
        const string metallicTooltip = "How metallic the surface should be." + 
            "\n1 represents a metal, and 0 represents a non-metal." + 
            "\nVery few objects in the real world use values around 0.5.";

        MaterialProperty specularColorProp = null;
        const string specularColorName = "_SpecColor";
        MaterialProperty specularTexProp = null;
        const string specularTexName = "_SpecGlossMap";
        const string specularColorLabel = "Specular Color";
        const string specularColorTooltip = "What color should be used for specular highlights.";

        MaterialProperty smoothnessProp = null;
        const string smoothnessName = "_Smoothness";
        MaterialProperty smoothnessTexProp = null;
        const string smoothnessTexName = "_SmoothnessMap";
        const string smoothnessLabel = "Smoothness";
        const string smoothnessTooltip = "How smooth the surface of the object should be." + 
            "\n1 reprsents a highly polished surface. 0 represents a very rough or matter surface.";

        MaterialProperty convertFromRoughnessProp = null;
        const string convertFromRoughnessName = "_ConvertFromRoughness";
        const string convertFromRoughnessLabel = "Convert From Roughness";
        const string convertFromRoughnessTooltip = "Does this material use a roughness texture instead of smoothness?";

        MaterialProperty normalStrengthProp = null;
        const string normalStrengthName = "_BumpScale";
        MaterialProperty normalMapProp = null;
        const string normalMapName = "_BumpMap";
        const string normalStrengthLabel = "Normal Map";
        const string normalStrengthTooltip = "Normal map modifies the surface normals for finer lighting detail." + 
            "1 represents 'standard' strength.";

        MaterialProperty heightmapStrengthProp = null;
        const string heightmapStrengthName = "_Parallax";
        MaterialProperty heightmapTexProp = null;
        const string heightmapTexName = "_ParallaxMap";
        const string heightmapStrengthLabel = "Heightmap";
        const string heightmapStrengthTooltip = "A heightmap can be used to 'fake' raised and lower sections on the surface.";

        MaterialProperty occlusionStrengthProp = null;
        const string occlusionStrengthName = "_OcclusionStrength";
        MaterialProperty occlusionTexProp = null;
        const string occlusionTexName = "_OcclusionMap";
        const string occlusionStrengthLabel = "Ambient Occlusion";
        const string occlusionStrengthTooltip = "Amount of ambient occlusion falling on the surface." + 
            "\n1 represents a fully lit part of the surface, while 0 means a fully shadowed area.";

        MaterialProperty emissionColorProp = null;
        const string emissionColorName = "_EmissionColor";
        MaterialProperty emissionTexProp = null;
        const string emissionTexName = "_EmissionMap";
        const string emissionColorLabel = "Emission Color";
        const string emissionColorTooltip = "The amount of emissive light to use on the surface." + 
            "\nWhereas Base Color is influenced by scene lighting, emissive color is visible regardless of whether the object is in shadow.";
        
        MaterialProperty receiveShadowsProp = null;
        const string receiveShadowsName = "_ReceiveShadows";
        const string receiveShadowsLabel = "Receive Shadows";
        const string receiveShadowsTooltip = "Toggle whether to render realtime shadows from other objects influenced by the scene lights.";

        MaterialProperty alphaClipProp = null;
        const string alphaClipName = "_AlphaClip";
        const string alphaClipLabel = "Alpha Clip";
        const string alphaClipTooltip = "Should this object use alpha clipping?";

        MaterialProperty alphaClipThresholdProp = null;
        const string alphaClipThresholdName = "_Cutoff";
        const string alphaClipThresholdLabel = "Threshold";
        const string alphaClipThresholdTooltip = "Pixels with an alpha value below this threshold are culled.";

        private const string cullName = "_Cull";
        private const string cullLabel = "Render Face";
        private const string cullTooltip = "Choose which sides of the mesh faces to render.";

        private const string blendModeName = "_Blend";
        private const string blendModeLabel = "Blend Mode";
        private const string blendModeTooltip = "How Unity should blend this mesh with previously drawn objects.";

        private const string surfaceTypeName = "_Surface";
        private const string surfaceTypeLabel = "Surface Type";
        private const string surfaceTypeTooltip = "Whether the mesh is rendered opaque or transparent.";

        private const string zWriteName = "_ZWrite";

        private enum SurfaceType
        {
            Opaque = 0,
            Transparent = 1
        }

        private enum RenderFace
        {
            Front = 2,
            Back = 1,
            Both = 0
        }

        private enum BlendMode
        {
            Alpha = 0,
            Premultiply = 1,
            Additive = 2,
            Multiply = 3
        }

        private static readonly string[] surfaceTypeNames = Enum.GetNames(typeof(SurfaceType));
        private static readonly string[] renderFaceNames = Enum.GetNames(typeof(RenderFace));
        private static readonly string[] blendModeNames = Enum.GetNames(typeof(BlendMode));

        private bool shouldRenderMetallic = false;
        private bool shouldRenderSpecular = false;

        private bool firstTimeOpen = true;

        protected override void FindProperties(MaterialProperty[] props)
        {
            baseColorProp = FindProperty(baseColorName, props, true);
            baseTexProp = FindProperty(baseTexName, props, true);
            workflowModeProp = FindProperty(workflowModeName, props, false);
            metallicProp = FindProperty(metallicName, props, false);
            metallicTexProp = FindProperty(metallicTexName, props, false);
            specularColorProp = FindProperty(specularColorName, props, false);
            specularTexProp = FindProperty(specularTexName, props, false);
            smoothnessProp = FindProperty(smoothnessName, props, false);
            smoothnessTexProp = FindProperty(smoothnessTexName, props, false);
            convertFromRoughnessProp = FindProperty(convertFromRoughnessName, props, false);
            normalMapProp = FindProperty(normalMapName, props, false);
            normalStrengthProp = FindProperty(normalStrengthName, props, false);
            heightmapStrengthProp = FindProperty(heightmapStrengthName, props, false);
            heightmapTexProp = FindProperty(heightmapTexName, props, false);
            occlusionStrengthProp = FindProperty(occlusionStrengthName, props, false);
            occlusionTexProp = FindProperty(occlusionTexName, props, false);
            emissionColorProp = FindProperty(emissionColorName, props, false);
            emissionTexProp = FindProperty(emissionTexName, props, false);

            alphaClipProp = FindProperty(alphaClipName, props, false);
            alphaClipThresholdProp = FindProperty(alphaClipThresholdName, props, true);
            receiveShadowsProp = FindProperty(receiveShadowsName, props, false);
        }

        private void SetBlendMode(BlendMode blendMode, SurfaceType surfaceType, Material material)
        {
            var srcBlendRGB = UnityEngine.Rendering.BlendMode.One;
            var dstBlendRGB = UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;
            var srcBlendA = UnityEngine.Rendering.BlendMode.One;
            var dstBlendA = UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;

            if (surfaceType == SurfaceType.Transparent)
            {
                switch (blendMode)
                {
                    case BlendMode.Alpha:
                        {
                            srcBlendRGB = UnityEngine.Rendering.BlendMode.SrcAlpha;
                            dstBlendRGB = UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;
                            srcBlendA = UnityEngine.Rendering.BlendMode.One;
                            dstBlendA = dstBlendRGB;
                            break;
                        }
                    case BlendMode.Premultiply:
                        {
                            srcBlendRGB = UnityEngine.Rendering.BlendMode.One;
                            dstBlendRGB = UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;
                            srcBlendA = srcBlendRGB;
                            dstBlendA = dstBlendRGB;
                            break;
                        }
                    case BlendMode.Additive:
                        {
                            srcBlendRGB = UnityEngine.Rendering.BlendMode.SrcAlpha;
                            dstBlendRGB = UnityEngine.Rendering.BlendMode.One;
                            srcBlendA = UnityEngine.Rendering.BlendMode.One;
                            dstBlendA = dstBlendRGB;
                            break;
                        }
                    case BlendMode.Multiply:
                        {
                            srcBlendRGB = UnityEngine.Rendering.BlendMode.DstColor;
                            dstBlendRGB = UnityEngine.Rendering.BlendMode.Zero;
                            srcBlendA = UnityEngine.Rendering.BlendMode.Zero;
                            dstBlendA = UnityEngine.Rendering.BlendMode.One;
                            break;
                        }
                }
            }
            else
            {
                srcBlendRGB = UnityEngine.Rendering.BlendMode.One;
                dstBlendRGB = UnityEngine.Rendering.BlendMode.Zero;
                srcBlendA = UnityEngine.Rendering.BlendMode.One;
                dstBlendA = UnityEngine.Rendering.BlendMode.Zero;
            }

            material.SetFloat("_SrcBlend", (float)srcBlendRGB);
            material.SetFloat("_DstBlend", (float)dstBlendRGB);
            material.SetFloat("_SrcBlendAlpha", (float)srcBlendA);
            material.SetFloat("_DstBlendAlpha", (float)dstBlendA);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            base.OnGUI(materialEditor, properties);
            FindProperties(properties);

            if (firstTimeOpen)
            {
                materialScopeList.RegisterHeaderScope(new GUIContent("Surface Options"), 1u << 0, DrawSurfaceOptions);
                materialScopeList.RegisterHeaderScope(new GUIContent("Lit Properties"), 1u << 1, DrawLitProperties);
                firstTimeOpen = false;
            }
        }

        protected override void DrawBanner()
        {
            // Try and retrieve the banner texture if we don't have it yet.
            if (bannerTexture == null)
            {
                bannerTexture = Resources.Load<Texture2D>("DefaultLitBanner");
            }

            if (bannerTexture != null)
            {
                float width = Mathf.Max(EditorGUIUtility.currentViewWidth, bannerTexture.height);
                float height = Mathf.Min(bannerTexture.height * (width / bannerTexture.width), bannerTexture.height);
                var bannerRect = new Rect(0, 0, width, height);
                GUI.DrawTexture(bannerRect, bannerTexture, ScaleMode.ScaleToFit, true);
                GUILayout.Space(height);
            }
            else
            {
                EditorGUILayout.LabelField("Default Lit", headerStyle);
            }

            EditorGUILayout.LabelField("A PBR shader effect, just like URP's Lit shader", descriptionStyle);
            GUILayout.Space(5);
        }

        protected void DrawDividerLine(Color color)
        {
            EditorGUILayout.Space(5);
            var rect = EditorGUILayout.BeginHorizontal();
            Handles.color = color;
            Handles.DrawLine(new Vector2(rect.x - 15, rect.y), new Vector2(rect.width + 15, rect.y));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5);
        }

        private void DrawSurfaceOptions(Material material)
        {
            EditorGUILayout.Space(3);

            var surfaceType = (SurfaceType)material.GetFloat(surfaceTypeName);
            var renderFace = (RenderFace)material.GetFloat(cullName);
            var blendMode = (BlendMode)material.GetFloat(blendModeName);
            var workflowMode = (LitGUI.WorkflowMode)material.GetFloat(workflowModeName);

            shouldRenderMetallic = (metallicTexProp != null);
            shouldRenderSpecular = (specularTexProp != null);

            // Show the workflow mode only if it exists and there is actually a choice between both.
            if (workflowModeProp != null && metallicTexProp != null && specularTexProp != null)
            {
                EditorGUI.BeginChangeCheck();
                {
                    workflowMode = (LitGUI.WorkflowMode)EditorGUILayout.EnumPopup(new GUIContent(workflowModeLabel, workflowModeTooltip), workflowMode);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(material, "Modify Workflow Mode");
                    material.SetFloat(workflowModeName, (float)workflowMode);

                    if (workflowMode == LitGUI.WorkflowMode.Specular)
                    {
                        material.EnableKeyword("_SPECULAR_SETUP");
                    }
                    else
                    {
                        material.DisableKeyword("_SPECULAR_SETUP");
                    }

                    EditorUtility.SetDirty(material);
                }

                shouldRenderMetallic = (workflowMode == LitGUI.WorkflowMode.Metallic);
                shouldRenderSpecular = (workflowMode == LitGUI.WorkflowMode.Specular);
            }

            // Display opaque/transparent options.
            bool surfaceTypeChanged = false;
            EditorGUI.BeginChangeCheck();
            {
                surfaceType = (SurfaceType)EditorGUILayout.EnumPopup(new GUIContent(surfaceTypeLabel, surfaceTypeTooltip), surfaceType);
            }
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(material, "Modify Surface Type");
                surfaceTypeChanged = true;
            }

            // Display culling options.
            EditorGUI.BeginChangeCheck();
            {
                renderFace = (RenderFace)EditorGUILayout.EnumPopup(new GUIContent(cullLabel, cullTooltip), renderFace);
            }
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(material, "Modify Render Faces");

                switch (renderFace)
                {
                    case RenderFace.Both:
                        {
                            material.SetFloat(cullName, 0);
                            break;
                        }
                    case RenderFace.Back:
                        {
                            material.SetFloat(cullName, 1);
                            break;
                        }
                    case RenderFace.Front:
                        {
                            material.SetFloat(cullName, 2);
                            break;
                        }
                }

                EditorUtility.SetDirty(material);
            }

            // Display blend mode options.
            bool blendModeChanged = false;
            if (surfaceType == SurfaceType.Transparent)
            {
                EditorGUI.BeginChangeCheck();
                {
                    blendMode = (BlendMode)EditorGUILayout.EnumPopup(new GUIContent(blendModeLabel, blendModeTooltip), blendMode);
                }

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(material, "Modify Blend Mode");

                    blendModeChanged = true;
                    material.SetFloat(blendModeName, (float)blendMode);
                    EditorUtility.SetDirty(material);
                }
            }

            bool alphaClip = material.GetFloat(alphaClipName) > 0.5f;

            // Display alpha clip options.
            EditorGUI.BeginChangeCheck();
            {
                alphaClip = EditorGUILayout.Toggle(new GUIContent(alphaClipLabel, alphaClipTooltip), alphaClip);
            }
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(material, "Toggle Alpha Clip");
                surfaceTypeChanged = true;
            }

            material.SetFloat(alphaClipName, alphaClip ? 1.0f : 0.0f);

            if (surfaceTypeChanged || blendModeChanged)
            {
                switch (surfaceType)
                {
                    case SurfaceType.Opaque:
                        {
                            material.SetOverrideTag("RenderType", "Opaque");
                            SetBlendMode(blendMode, surfaceType, material);
                            material.SetFloat(zWriteName, 1);
                            material.SetFloat(surfaceTypeName, 0);

                            alphaClip = material.GetFloat(alphaClipName) >= 0.5f;
                            if (alphaClip)
                            {
                                material.EnableKeyword("_ALPHATEST_ON");
                                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                                material.SetOverrideTag("RenderType", "TransparentCutout");
                            }
                            else
                            {
                                material.DisableKeyword("_ALPHATEST_ON");
                                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
                                material.SetOverrideTag("RenderType", "Opaque");
                            }


                            break;
                        }
                    case SurfaceType.Transparent:
                        {
                            alphaClip = material.GetFloat(alphaClipName) >= 0.5f;
                            if (alphaClip)
                            {
                                material.EnableKeyword("_ALPHATEST_ON");
                            }
                            else
                            {
                                material.DisableKeyword("_ALPHATEST_ON");
                            }
                            material.SetOverrideTag("RenderType", "Transparent");
                            SetBlendMode(blendMode, surfaceType, material);
                            material.SetFloat(zWriteName, 0);
                            material.SetFloat(surfaceTypeName, 1);

                            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                            break;
                        }
                }

                EditorUtility.SetDirty(material);
            }

            alphaClip = material.GetFloat(alphaClipName) >= 0.5f;
            if (alphaClip)
            {
                EditorGUI.indentLevel++;
                materialEditor.ShaderProperty(alphaClipThresholdProp, new GUIContent(alphaClipThresholdLabel, alphaClipThresholdTooltip));
                EditorGUI.indentLevel--;
            }

            if (receiveShadowsProp != null)
            {
                bool receiveShadows = material.GetFloat(receiveShadowsName) > 0.5f;

                EditorGUI.BeginChangeCheck();
                {
                    receiveShadows = EditorGUILayout.Toggle(new GUIContent(receiveShadowsLabel, receiveShadowsTooltip), receiveShadows);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(material, "Toggle Receive Shadows");

                    material.SetFloat(receiveShadowsName, receiveShadows ? 1.0f : 0.0f);

                    if (receiveShadows)
                    {
                        material.DisableKeyword("_RECEIVE_SHADOWS_OFF");
                    }
                    else
                    {
                        material.EnableKeyword("_RECEIVE_SHADOWS_OFF");
                    }

                    EditorUtility.SetDirty(material);
                }
            }
        }

        private void DrawLitProperties(Material material)
        {
            EditorGUILayout.Space(3);

            EditorGUILayout.LabelField("Note: Base Texture tiling & offset settings are used for all Lit texture maps.", tinyLabelStyle);

            materialEditor.ShaderProperty(baseColorProp, new GUIContent(baseColorLabel, baseColorTooltip));
            materialEditor.ShaderProperty(baseTexProp, new GUIContent(baseTexLabel, baseTexTooltip));

            if (shouldRenderMetallic)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(metallicLabel, metallicTooltip),
                    metallicTexProp, metallicProp);
            }

            if (shouldRenderSpecular)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(specularColorLabel, specularColorTooltip),
                    specularTexProp, specularColorProp);
            }

            if (smoothnessProp != null)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(smoothnessLabel, smoothnessTooltip),
                    smoothnessTexProp, smoothnessProp);

                bool convertFromRough = material.GetFloat(convertFromRoughnessName) > 0.5f;

                EditorGUI.BeginChangeCheck();
                {
                    convertFromRough = EditorGUILayout.Toggle(new GUIContent(convertFromRoughnessLabel, convertFromRoughnessTooltip), convertFromRough);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(material, "Modify Convert From Rough");
                    material.SetFloat(convertFromRoughnessName, convertFromRough ? 1.0f : 0.0f);
                    EditorUtility.SetDirty(material);
                }
            }

            if (normalMapProp != null)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(normalStrengthLabel, normalStrengthTooltip),
                    normalMapProp, normalStrengthProp);
            }

            if (heightmapTexProp != null)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(heightmapStrengthLabel, heightmapStrengthTooltip),
                    heightmapTexProp, heightmapStrengthProp);
            }

            if (occlusionTexProp != null)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(occlusionStrengthLabel, occlusionStrengthTooltip),
                    occlusionTexProp, occlusionStrengthProp);
            }

            if (emissionTexProp != null)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(emissionColorLabel, emissionColorTooltip),
                    emissionTexProp, emissionColorProp);
            }
        }
    }
}

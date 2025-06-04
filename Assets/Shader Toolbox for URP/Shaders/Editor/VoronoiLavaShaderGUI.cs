namespace ShaderToolboxPro.URP
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEditor.Rendering.Universal.ShaderGUI;
    using UnityEngine;

    public class VoronoiLavaShaderGUI : ToolboxShaderGUI
    {
        MaterialProperty baseColorProp = null;
        const string baseColorName = "_BaseColor";
        const string baseColorLabel = "Base Color";
        const string baseColorTooltip = "Albedo color applied to entire mesh." + 
            "\n\nApplies to layer 1.";

        MaterialProperty baseTexProp = null;
        const string baseTexName = "_BaseMap";
        const string baseTexLabel = "Base Texture";
        const string baseTexTooltip = "Albedo texture applied to entire mesh." +
            "\n\nApplies to layer 1.";

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
            "\nVery few objects in the real world use values around 0.5." +
            "\n\nApplies to layer 1.";

        MaterialProperty specularColorProp = null;
        const string specularColorName = "_SpecColor";
        MaterialProperty specularTexProp = null;
        const string specularTexName = "_SpecGlossMap";
        const string specularColorLabel = "Specular Color";
        const string specularColorTooltip = "What color should be used for specular highlights." +
            "\n\nApplies to layer 1.";

        MaterialProperty smoothnessProp = null;
        const string smoothnessName = "_Smoothness";
        MaterialProperty smoothnessTexProp = null;
        const string smoothnessTexName = "_SmoothnessMap";
        const string smoothnessLabel = "Smoothness";
        const string smoothnessTooltip = "How smooth the surface of the object should be." +
            "\n1 reprsents a highly polished surface. 0 represents a very rough or matter surface." +
            "\n\nApplies to layer 1.";

        MaterialProperty convertFromRoughnessProp = null;
        const string convertFromRoughnessName = "_ConvertFromRoughness";
        const string convertFromRoughnessLabel = "Convert From Roughness";
        const string convertFromRoughnessTooltip = "Does this material use a roughness texture instead of smoothness?" +
            "\n\nApplies to layer 1.";

        MaterialProperty normalStrengthProp = null;
        const string normalStrengthName = "_BumpScale";
        MaterialProperty normalMapProp = null;
        const string normalMapName = "_BumpMap";
        const string normalStrengthLabel = "Normal Map";
        const string normalStrengthTooltip = "Normal map modifies the surface normals for finer lighting detail." +
            "1 represents 'standard' strength." +
            "\n\nApplies to layer 1.";

        MaterialProperty heightmapStrengthProp = null;
        const string heightmapStrengthName = "_Parallax";
        MaterialProperty heightmapTexProp = null;
        const string heightmapTexName = "_ParallaxMap";
        const string heightmapStrengthLabel = "Heightmap";
        const string heightmapStrengthTooltip = "A heightmap can be used to 'fake' raised and lower sections on the surface." +
            "\n\nApplies to layer 1.";

        MaterialProperty occlusionStrengthProp = null;
        const string occlusionStrengthName = "_OcclusionStrength";
        MaterialProperty occlusionTexProp = null;
        const string occlusionTexName = "_OcclusionMap";
        const string occlusionStrengthLabel = "Ambient Occlusion";
        const string occlusionStrengthTooltip = "Amount of ambient occlusion falling on the surface." +
            "\n1 represents a fully lit part of the surface, while 0 means a fully shadowed area." +
            "\n\nApplies to layer 1.";

        MaterialProperty emissionColorProp = null;
        const string emissionColorName = "_EmissionColor";
        MaterialProperty emissionTexProp = null;
        const string emissionTexName = "_EmissionMap";
        const string emissionColorLabel = "Emission Color";
        const string emissionColorTooltip = "The amount of emissive light to use on the surface." +
            "\nWhereas Base Color is influenced by scene lighting, emissive color is visible regardless of whether the object is in shadow." +
            "\n\nApplies to layer 1.";

        MaterialProperty baseColor2Prop = null;
        const string baseColor2Name = "_BaseColor_2";
        const string baseColor2Label = "Base Color";
        const string baseColor2Tooltip = "Albedo color applied to entire mesh." +
            "\n\nApplies to layer 2.";

        MaterialProperty baseTex2Prop = null;
        const string baseTex2Name = "_BaseMap_2";
        const string baseTex2Label = "Base Texture";
        const string baseTex2Tooltip = "Albedo texture applied to entire mesh." +
            "\n\nApplies to layer 2.";

        MaterialProperty metallic2Prop = null;
        const string metallic2Name = "_Metallic_2";
        MaterialProperty metallicTex2Prop = null;
        const string metallicTex2Name = "_MetallicGlossMap_2";
        const string metallic2Label = "Metallic";
        const string metallic2Tooltip = "How metallic the surface should be." +
            "\n1 represents a metal, and 0 represents a non-metal." +
            "\nVery few objects in the real world use values around 0.5." +
            "\n\nApplies to layer 2.";

        MaterialProperty specularColor2Prop = null;
        const string specularColor2Name = "_SpecColor_2";
        MaterialProperty specularTex2Prop = null;
        const string specularTex2Name = "_SpecGlossMap_2";
        const string specularColor2Label = "Specular Color";
        const string specularColor2Tooltip = "What color should be used for specular highlights." +
            "\n\nApplies to layer 2.";

        MaterialProperty smoothness2Prop = null;
        const string smoothness2Name = "_Smoothness_2";
        MaterialProperty smoothnessTex2Prop = null;
        const string smoothnessTex2Name = "_SmoothnessMap_2";
        const string smoothness2Label = "Smoothness";
        const string smoothness2Tooltip = "How smooth the surface of the object should be." +
            "\n1 reprsents a highly polished surface. 0 represents a very rough or matter surface." +
            "\n\nApplies to layer 2.";

        MaterialProperty convertFromRoughness2Prop = null;
        const string convertFromRoughness2Name = "_ConvertFromRoughness_2";
        const string convertFromRoughness2Label = "Convert From Roughness";
        const string convertFromRoughness2Tooltip = "Does this material use a roughness texture instead of smoothness?" +
            "\n\nApplies to layer 2.";

        MaterialProperty normalStrength2Prop = null;
        const string normalStrength2Name = "_BumpScale_2";
        MaterialProperty normalMap2Prop = null;
        const string normalMap2Name = "_BumpMap_2";
        const string normalStrength2Label = "Normal Map";
        const string normalStrength2Tooltip = "Normal map modifies the surface normals for finer lighting detail." +
            "1 represents 'standard' strength." +
            "\n\nApplies to layer 2.";

        MaterialProperty heightmapStrength2Prop = null;
        const string heightmapStrength2Name = "_Parallax_2";
        MaterialProperty heightmapTex2Prop = null;
        const string heightmapTex2Name = "_ParallaxMap_2";
        const string heightmapStrength2Label = "Heightmap";
        const string heightmapStrength2Tooltip = "A heightmap can be used to 'fake' raised and lower sections on the surface." +
            "\n\nApplies to layer 2.";

        MaterialProperty occlusionStrength2Prop = null;
        const string occlusionStrength2Name = "_OcclusionStrength_2";
        MaterialProperty occlusionTex2Prop = null;
        const string occlusionTex2Name = "_OcclusionMap_2";
        const string occlusionStrength2Label = "Ambient Occlusion";
        const string occlusionStrength2Tooltip = "Amount of ambient occlusion falling on the surface." +
            "\n1 represents a fully lit part of the surface, while 0 means a fully shadowed area." +
            "\n\nApplies to layer 2.";

        MaterialProperty emissionColor2Prop = null;
        const string emissionColor2Name = "_EmissionColor_2";
        MaterialProperty emissionTex2Prop = null;
        const string emissionTex2Name = "_EmissionMap_2";
        const string emissionColor2Label = "Emission Color";
        const string emissionColor2Tooltip = "The amount of emissive light to use on the surface." +
            "\nWhereas Base Color is influenced by scene lighting, emissive color is visible regardless of whether the object is in shadow." +
            "\n\nApplies to layer 2.";

        MaterialProperty voronoiDensityProp = null;
        const string voronoiDensityName = "_VoronoiDensity";
        const string voronoiDensityLabel = "Voronoi Density";
        const string voronoiDensityTooltip = "";

        MaterialProperty voronoiAngleOffsetProp = null;
        const string voronoiAngleOffsetName = "_VoronoiAngleOffset";
        const string voronoiAngleOffsetLabel = "Voronoi Angle Offset";
        const string voronoiAngleOffsetTooltip = "";

        MaterialProperty voronoiThicknessProp = null;
        const string voronoiThicknessName = "_VoronoiThickness";
        const string voronoiThicknessLabel = "Voronoi Thickness";
        const string voronoiThicknessTooltip = "";

        MaterialProperty voronoiFalloffProp = null;
        const string voronoiFalloffName = "_VoronoiFalloff";
        const string voronoiFalloffLabel = "Voronoi Falloff";
        const string voronoiFalloffTooltip = "";

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
            workflowModeProp = FindProperty(workflowModeName, props, true);
            metallicProp = FindProperty(metallicName, props, true);
            metallicTexProp = FindProperty(metallicTexName, props, true);
            specularColorProp = FindProperty(specularColorName, props, true);
            specularTexProp = FindProperty(specularTexName, props, true);
            smoothnessProp = FindProperty(smoothnessName, props, true);
            smoothnessTexProp = FindProperty(smoothnessTexName, props, true);
            convertFromRoughnessProp = FindProperty(convertFromRoughnessName, props, true);
            normalMapProp = FindProperty(normalMapName, props, true);
            normalStrengthProp = FindProperty(normalStrengthName, props, true);
            heightmapStrengthProp = FindProperty(heightmapStrengthName, props, true);
            heightmapTexProp = FindProperty(heightmapTexName, props, true);
            occlusionStrengthProp = FindProperty(occlusionStrengthName, props, true);
            occlusionTexProp = FindProperty(occlusionTexName, props, true);
            emissionColorProp = FindProperty(emissionColorName, props, true);
            emissionTexProp = FindProperty(emissionTexName, props, true);

            baseColor2Prop = FindProperty(baseColor2Name, props, true);
            baseTex2Prop = FindProperty(baseTex2Name, props, true);
            metallic2Prop = FindProperty(metallic2Name, props, true);
            metallicTex2Prop = FindProperty(metallicTex2Name, props, true);
            specularColor2Prop = FindProperty(specularColor2Name, props, true);
            specularTex2Prop = FindProperty(specularTex2Name, props, true);
            smoothness2Prop = FindProperty(smoothness2Name, props, true);
            smoothnessTex2Prop = FindProperty(smoothnessTex2Name, props, true);
            convertFromRoughness2Prop = FindProperty(convertFromRoughness2Name, props, true);
            normalMap2Prop = FindProperty(normalMap2Name, props, true);
            normalStrength2Prop = FindProperty(normalStrength2Name, props, true);
            heightmapStrength2Prop = FindProperty(heightmapStrength2Name, props, true);
            heightmapTex2Prop = FindProperty(heightmapTex2Name, props, true);
            occlusionStrength2Prop = FindProperty(occlusionStrength2Name, props, true);
            occlusionTex2Prop = FindProperty(occlusionTex2Name, props, true);
            emissionColor2Prop = FindProperty(emissionColor2Name, props, true);
            emissionTex2Prop = FindProperty(emissionTex2Name, props, true);

            voronoiDensityProp = FindProperty(voronoiDensityName, props, true);
            voronoiAngleOffsetProp = FindProperty(voronoiAngleOffsetName, props, true);
            voronoiThicknessProp = FindProperty(voronoiThicknessName, props, true);
            voronoiFalloffProp = FindProperty(voronoiFalloffName, props, true);

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
                materialScopeList.RegisterHeaderScope(new GUIContent("Voronoi Properties"), 1u << 1, DrawVoronoiProperties);
                materialScopeList.RegisterHeaderScope(new GUIContent("First Layer Properties"), 1u << 2, DrawFirstLayerProperties);
                materialScopeList.RegisterHeaderScope(new GUIContent("Second Layer Properties"), 1u << 3, DrawSecondLayerProperties);
                firstTimeOpen = false;
            }
        }

        protected override void DrawBanner()
        {
            // Try and retrieve the banner texture if we don't have it yet.
            if (bannerTexture == null)
            {
                bannerTexture = Resources.Load<Texture2D>("VoronoiLavaBanner");
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
                EditorGUILayout.LabelField("Voronoi Lava", headerStyle);
            }

            EditorGUILayout.LabelField("An effect which picks between two PBR layers based on a Voronoi edge pattern.", descriptionStyle);
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

            if(GUILayout.Button(new GUIContent("Swap Texture Layers", "Swap all properties in Layer 1 with Layer 2.")))
            {
                SwapLayers(material);
            }
        }

        private void DrawVoronoiProperties(Material material)
        {
            materialEditor.ShaderProperty(voronoiDensityProp, new GUIContent(voronoiDensityLabel, voronoiDensityTooltip));
            materialEditor.ShaderProperty(voronoiAngleOffsetProp, new GUIContent(voronoiAngleOffsetLabel, voronoiAngleOffsetTooltip));
            materialEditor.ShaderProperty(voronoiThicknessProp, new GUIContent(voronoiThicknessLabel, voronoiThicknessTooltip));
            materialEditor.ShaderProperty(voronoiFalloffProp, new GUIContent(voronoiFalloffLabel, voronoiFalloffTooltip));
        }

        private void DrawFirstLayerProperties(Material material)
        {
            EditorGUILayout.Space(3);

            EditorGUILayout.LabelField("Note: Base Texture tiling & offset settings are used for all Layer 1 texture maps.", tinyLabelStyle);

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

        private void DrawSecondLayerProperties(Material material)
        {
            EditorGUILayout.Space(3);

            EditorGUILayout.LabelField("Note: Base Texture tiling & offset settings are used for all Layer 2 texture maps.", tinyLabelStyle);

            materialEditor.ShaderProperty(baseColor2Prop, new GUIContent(baseColor2Label, baseColor2Tooltip));
            materialEditor.ShaderProperty(baseTex2Prop, new GUIContent(baseTex2Label, baseTex2Tooltip));

            if (shouldRenderMetallic)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(metallic2Label, metallic2Tooltip),
                    metallicTex2Prop, metallic2Prop);
            }

            if (shouldRenderSpecular)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(specularColor2Label, specularColor2Tooltip),
                    specularTex2Prop, specularColor2Prop);
            }

            if (smoothnessProp != null)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(smoothness2Label, smoothness2Tooltip),
                    smoothnessTex2Prop, smoothness2Prop);

                bool convertFromRough = material.GetFloat(convertFromRoughness2Name) > 0.5f;

                EditorGUI.BeginChangeCheck();
                {
                    convertFromRough = EditorGUILayout.Toggle(new GUIContent(convertFromRoughness2Label, convertFromRoughness2Tooltip), convertFromRough);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(material, "Modify Convert From Rough");
                    material.SetFloat(convertFromRoughness2Name, convertFromRough ? 1.0f : 0.0f);
                    EditorUtility.SetDirty(material);
                }
            }

            if (normalMapProp != null)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(normalStrength2Label, normalStrength2Tooltip),
                    normalMap2Prop, normalStrength2Prop);
            }

            if (heightmapTexProp != null)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(heightmapStrength2Label, heightmapStrength2Tooltip),
                    heightmapTex2Prop, heightmapStrength2Prop);
            }

            if (occlusionTexProp != null)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(occlusionStrength2Label, occlusionStrength2Tooltip),
                    occlusionTex2Prop, occlusionStrength2Prop);
            }

            if (emissionTexProp != null)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent(emissionColor2Label, emissionColor2Tooltip),
                    emissionTex2Prop, emissionColor2Prop);
            }
        }

        public void SwapLayers(Material material)
        {
            var baseColor = material.GetColor(baseColorName);
            var baseTex = material.GetTexture(baseTexName);
            var metallic = material.GetFloat(metallicName);
            var metallicTex = material.GetTexture(metallicTexName);
            var specularColor = material.GetColor(specularColorName);
            var specularTex = material.GetTexture(specularTexName);
            var smoothness = material.GetFloat(smoothnessName);
            var smoothnessTex = material.GetTexture(smoothnessTexName);
            var convertFromRoughness = material.GetFloat(convertFromRoughnessName);
            var normalTex = material.GetTexture(normalMapName);
            var normalStrength = material.GetFloat(normalStrengthName);
            var heightmapStrength = material.GetFloat(heightmapStrengthName);
            var heightmapTex = material.GetTexture(heightmapTexName);
            var occlusionStrength = material.GetFloat(occlusionStrengthName);
            var occlusionTex = material.GetTexture(occlusionTexName);
            var emissionColor = material.GetColor(emissionColorName);
            var emissionTex = material.GetTexture(emissionTexName);

            var baseColor2 = material.GetColor(baseColor2Name);
            var baseTex2 = material.GetTexture(baseTex2Name);
            var metallic2 = material.GetFloat(metallic2Name);
            var metallicTex2 = material.GetTexture(metallicTex2Name);
            var specularColor2 = material.GetColor(specularColor2Name);
            var specularTex2 = material.GetTexture(specularTex2Name);
            var smoothness2 = material.GetFloat(smoothness2Name);
            var smoothnessTex2 = material.GetTexture(smoothnessTex2Name);
            var convertFromRoughness2 = material.GetFloat(convertFromRoughness2Name);
            var normalTex2 = material.GetTexture(normalMap2Name);
            var normalStrength2 = material.GetFloat(normalStrength2Name);
            var heightmapStrength2 = material.GetFloat(heightmapStrength2Name);
            var heightmapTex2 = material.GetTexture(heightmapTex2Name);
            var occlusionStrength2 = material.GetFloat(occlusionStrength2Name);
            var occlusionTex2 = material.GetTexture(occlusionTex2Name);
            var emissionColor2 = material.GetColor(emissionColor2Name);
            var emissionTex2 = material.GetTexture(emissionTex2Name);

            Undo.RecordObject(material, "Swap Texture Layers");

            material.SetColor(baseColorName, baseColor2);
            material.SetTexture(baseTexName, baseTex2);
            material.SetFloat(metallicName, metallic2);
            material.SetTexture(metallicTexName, metallicTex2);
            material.SetColor(specularColorName, specularColor2);
            material.SetTexture(specularTexName, specularTex2);
            material.SetFloat(smoothnessName, smoothness2);
            material.SetTexture(smoothnessTexName, smoothnessTex2);
            material.SetFloat(convertFromRoughnessName, convertFromRoughness2);
            material.SetTexture(normalMapName, normalTex2);
            material.SetFloat(normalStrengthName, normalStrength2);
            material.SetFloat(heightmapStrengthName, heightmapStrength2);
            material.SetTexture(heightmapTexName, heightmapTex2);
            material.SetFloat(occlusionStrengthName, occlusionStrength2);
            material.SetTexture(occlusionTexName, occlusionTex2);
            material.SetColor(emissionColorName, emissionColor2);
            material.SetTexture(emissionTexName, emissionTex2);

            material.SetColor(baseColor2Name, baseColor);
            material.SetTexture(baseTex2Name, baseTex);
            material.SetFloat(metallic2Name, metallic);
            material.SetTexture(metallicTex2Name, metallicTex);
            material.SetColor(specularColor2Name, specularColor);
            material.SetTexture(specularTex2Name, specularTex);
            material.SetFloat(smoothness2Name, smoothness);
            material.SetTexture(smoothnessTex2Name, smoothnessTex);
            material.SetFloat(convertFromRoughness2Name, convertFromRoughness);
            material.SetTexture(normalMap2Name, normalTex);
            material.SetFloat(normalStrength2Name, normalStrength);
            material.SetFloat(heightmapStrength2Name, heightmapStrength);
            material.SetTexture(heightmapTex2Name, heightmapTex);
            material.SetFloat(occlusionStrength2Name, occlusionStrength);
            material.SetTexture(occlusionTex2Name, occlusionTex);
            material.SetColor(emissionColor2Name, emissionColor);
            material.SetTexture(emissionTex2Name, emissionTex);

            EditorUtility.SetDirty(material);
        }
    }
}

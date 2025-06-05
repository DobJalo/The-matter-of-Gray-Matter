namespace ShaderToolboxPro.URP
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEngine;

    public sealed class DissolveShaderGUI : DefaultLitShaderGUI
    {
        MaterialProperty noiseScaleProp = null;
        const string noiseScaleName = "_NoiseScale";
        const string noiseScaleLabel = "Noise Scale";
        const string noiseScaleTooltip = "Scale of the noise pattern used for the glowing edge.";

        MaterialProperty noiseStrengthProp = null;
        const string noiseStrengthName = "_NoiseStrength";
        const string noiseStrengthLabel = "Noise Strength";
        const string noiseStrengthTooltip = "How strongly the noise pattern raises or lowers the glowing edge.";

        MaterialProperty originTypeProp = null;
        const string originTypeName = "_ORIGIN_TYPE";
        const string originTypeLabel = "Origin Type";
        const string originTypeTooltip = "Should the effect dissolve objects on one side of a plane, or dissolve based on distance from a point in space?";

        MaterialProperty cutoffPointProp = null;
        const string cutoffPointName = "_CutoffPoint";
        const string cutoffPointLabel = "Cutoff Point";
        const string cutoffPointTooltip = "In Plane mode: Parameter denoting a point on the plane.\n" +
            "In Point mode: Origin point of the dissolve effect.";

        MaterialProperty cutoffDirectionProp = null;
        const string cutoffDirectionName = "_CutoffDirection";
        const string cutoffDirectionLabel = "Cutoff Direction";
        const string cutoffDirectionTooltip = "Parameter denoting the direction the plane faces.";

        MaterialProperty cutoffHeightProp = null;
        const string cutoffHeightName = "_CutoffHeight";
        const string cutoffHeightLabel = "Cutoff Height";
        const string cutoffHeightTooltip = "The distance from the origin point or the plane before pixels start being cutoff.";

        MaterialProperty flipDirectionProp = null;
        const string flipDirectionName = "_FlipDirection";
        const string flipDirectionLabel = "Flip Direction";
        const string flipDirectionTooltip = "When enabled, the dissolve acts in the opposite direction from the plane or origin point.";

        MaterialProperty glowColorProp = null;
        const string glowColorName = "_GlowColor";
        const string glowColorLabel = "Glow Color";
        const string glowColorTooltip = "Color of the glowing edge. Can be emissive.";

        MaterialProperty glowThicknessProp = null;
        const string glowThicknessName = "_GlowThickness";
        const string glowThicknessLabel = "Glow Thickness";
        const string glowThicknessTooltip = "Thickness of the glowing edge.";

        MaterialProperty useWorldSpaceProp = null;
        const string useWorldSpaceName = "_UseWorldSpace";
        const string useWorldSpaceLabel = "Use World Space";
        const string useWorldSpaceTooltip = "Should the plane/origin values be specified in world or object space?";

        MaterialProperty useEmissionProp = null;
        const string useEmissionName = "_UseEmission";
        const string useEmissionLabel = "Use Emission";
        const string useEmissionTooltip = "Should the glow color be output to the Emission or Base Color channel?";

        private bool firstTimeOpen = true;

        protected override void FindProperties(MaterialProperty[] props)
        {
            base.FindProperties(props);

            noiseScaleProp = FindProperty(noiseScaleName, props, true);
            noiseStrengthProp = FindProperty(noiseStrengthName, props, true);
            originTypeProp = FindProperty(originTypeName, props, true);
            cutoffPointProp = FindProperty(cutoffPointName, props, true);
            cutoffDirectionProp = FindProperty(cutoffDirectionName, props, true);
            cutoffHeightProp = FindProperty(cutoffHeightName, props, true);
            flipDirectionProp = FindProperty(flipDirectionName, props, true);
            glowColorProp = FindProperty(glowColorName, props, true);
            glowThicknessProp = FindProperty(glowThicknessName, props, true);
            useWorldSpaceProp = FindProperty(useWorldSpaceName, props, true);
            useEmissionProp = FindProperty(useEmissionName, props, true);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            base.OnGUI(materialEditor, properties);
            FindProperties(properties);

            if (firstTimeOpen)
            {
                materialScopeList.RegisterHeaderScope(new GUIContent("Dissolve Properties"), 1u << 2, DrawDissolveOptions);
                firstTimeOpen = false;
            }

            materialEditor.serializedObject.ApplyModifiedProperties();
        }

        protected override void DrawBanner()
        {
            // Try and retrieve the banner texture if we don't have it yet.
            if (bannerTexture == null)
            {
                bannerTexture = Resources.Load<Texture2D>("DissolveBanner");
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
                EditorGUILayout.LabelField("Dissolve", headerStyle);
            }

            EditorGUILayout.LabelField("A dissolve effect which can use a plane defined in world space to perform the cutoff.", descriptionStyle);
            GUILayout.Space(5);
        }

        private void DrawDissolveOptions(Material material)
        {
            EditorGUILayout.Space(3);

            materialEditor.ShaderProperty(noiseScaleProp, new GUIContent(noiseScaleLabel, noiseScaleTooltip));
            materialEditor.ShaderProperty(noiseStrengthProp, new GUIContent(noiseStrengthLabel, noiseStrengthTooltip));
            materialEditor.ShaderProperty(originTypeProp, new GUIContent(originTypeLabel, originTypeTooltip));

            var isPlaneEnabled = material.IsKeywordEnabled("_ORIGIN_TYPE_PLANE");

            materialEditor.ShaderProperty(cutoffPointProp, new GUIContent(cutoffPointLabel, cutoffPointTooltip));

            if(isPlaneEnabled)
            {
                materialEditor.ShaderProperty(cutoffDirectionProp, new GUIContent(cutoffDirectionLabel, cutoffDirectionTooltip));
            }
                
            materialEditor.ShaderProperty(cutoffHeightProp, new GUIContent(cutoffHeightLabel, cutoffHeightTooltip));
            materialEditor.ShaderProperty(flipDirectionProp, new GUIContent(flipDirectionLabel, flipDirectionTooltip));
            materialEditor.ShaderProperty(glowColorProp, new GUIContent(glowColorLabel, glowColorTooltip));
            materialEditor.ShaderProperty(glowThicknessProp, new GUIContent(glowThicknessLabel, glowThicknessTooltip));
            materialEditor.ShaderProperty(useWorldSpaceProp, new GUIContent(useWorldSpaceLabel, useWorldSpaceTooltip));
            materialEditor.ShaderProperty(useEmissionProp, new GUIContent(useEmissionLabel, useEmissionTooltip));
        }
    }
}

namespace ShaderToolboxPro.URP
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEngine;

    public sealed class GlassShaderGUI : DefaultLitShaderGUI
    {
        MaterialProperty refractiveIndexProp = null;
        const string refractiveIndexName = "_RefractiveIndex";
        const string refractiveIndexLabel = "Refractive Index";
        const string refractiveIndexTooltip = "How strongly light should bend through the object. Higher values mean lower bending.";

        MaterialProperty glassStrengthProp = null;
        const string glassStrengthName = "_GlassStrength";
        const string glassStrengthLabel = "Glass Strength";
        const string glassStrengthTooltip = "Proportion of final base color that uses the scene color.";

        MaterialProperty fresnelPowerProp = null;
        const string fresnelPowerName = "_FresnelPower";
        const string fresnelPowerLabel = "Fresnel Power";
        const string fresnelPowerTooltip = "Thickness of the Fresnel light. Higher values cover less surface.";

        MaterialProperty fresnelColorProp = null;
        const string fresnelColorName = "_FresnelColor";
        const string fresnelColorLabel = "Fresnel Color";
        const string fresnelColorTooltip = "Color of the Fresnel highlights.";

        MaterialProperty useEmissionProp = null;
        const string useEmissionName = "_UseEmission";
        const string useEmissionLabel = "Use Emission";
        const string useEmissionTooltip = "Should the Fresnel layer be applied to emission or base color?";

        MaterialProperty cameraTextureModeProp = null;
        const string cameraTextureModeName = "_CAMERA_TEXTURE_MODE";
        const string cameraTextureModeLabel = "Camera Texture Mode";
        const string cameraTextureModeTooltip = "Should this effect use the _CameraOpaqueTexture, or the _CameraTransparentTexture?\n\n" +
            "Warning: Transparent mode requires the experimental Copy Transparent Texture renderer feature from Shader Toolbox, which incurs a performance penalty. Consult the documentation for usage details.";
        
        private bool firstTimeOpen = true;

        protected override void FindProperties(MaterialProperty[] props)
        {
            base.FindProperties(props);

            refractiveIndexProp = FindProperty(refractiveIndexName, props, true);
            glassStrengthProp = FindProperty(glassStrengthName, props, true);
            fresnelPowerProp = FindProperty(fresnelPowerName, props, true);
            fresnelColorProp = FindProperty(fresnelColorName, props, true);
            useEmissionProp = FindProperty(useEmissionName, props, true);
            cameraTextureModeProp = FindProperty(cameraTextureModeName, props, true);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            base.OnGUI(materialEditor, properties);
            FindProperties(properties);

            if (firstTimeOpen)
            {
                materialScopeList.RegisterHeaderScope(new GUIContent("Glass Properties"), 1u << 2, DrawGlassOptions);
                materialScopeList.RegisterHeaderScope(new GUIContent("Advanced Settings"), 1u << 3, DrawAdvancedSettings);
                firstTimeOpen = false;
            }

            materialEditor.serializedObject.ApplyModifiedProperties();
        }

        protected override void DrawBanner()
        {
            // Try and retrieve the banner texture if we don't have it yet.
            if (bannerTexture == null)
            {
                bannerTexture = Resources.Load<Texture2D>("GlassBanner");
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
                EditorGUILayout.LabelField("Glass", headerStyle);
            }

            EditorGUILayout.LabelField("A glass effect which samples the camera opaque texture for screen-space refraction.", descriptionStyle);
            GUILayout.Space(5);
        }

        private void DrawGlassOptions(Material material)
        {
            EditorGUILayout.Space(3);

            materialEditor.ShaderProperty(refractiveIndexProp, new GUIContent(refractiveIndexLabel, refractiveIndexTooltip));
            materialEditor.ShaderProperty(glassStrengthProp, new GUIContent(glassStrengthLabel, glassStrengthTooltip));
            materialEditor.ShaderProperty(fresnelPowerProp, new GUIContent(fresnelPowerLabel, fresnelPowerTooltip));
            materialEditor.ShaderProperty(fresnelColorProp, new GUIContent(fresnelColorLabel, fresnelColorTooltip));
            materialEditor.ShaderProperty(useEmissionProp, new GUIContent(useEmissionLabel, useEmissionTooltip));
            materialEditor.ShaderProperty(cameraTextureModeProp, new GUIContent(cameraTextureModeLabel, cameraTextureModeTooltip));
        }

        private void DrawAdvancedSettings(Material material)
        {
            //materialEditor.RenderQueueField();
            //materialEditor.EnableInstancingField();
        }
    }
}

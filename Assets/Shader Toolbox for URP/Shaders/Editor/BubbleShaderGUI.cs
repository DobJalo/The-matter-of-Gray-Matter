namespace ShaderToolboxPro.URP
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEngine;

    public sealed class BubbleShaderGUI : DefaultLitShaderGUI
    {
        MaterialProperty colorRampTextureProp = null;
        const string colorRampTextureName = "_ColorRampTexture";
        const string colorRampTextureLabel = "Color Ramp Texture";
        const string colorRampTextureTooltip = "A thin horizontal strip texture with iridescent color values." + 
            "\nIf no texture is set, a color ramp will be generated in the shader.";

        MaterialProperty useColorRampTextureProp = null;
        const string useColorRampTextureName = "_USE_COLOR_RAMP_TEXTURE";
        
        MaterialProperty fresnelPowerProp = null;
        const string fresnelPowerName = "_FresnelPower";
        const string fresnelPowerLabel = "Fresnel Power";
        const string fresnelPowerTooltip = "The thickness of the bubble layer. Higher values cover less surface.";

        MaterialProperty fresnelNoiseStrengthProp = null;
        const string fresnelNoiseStrengthName = "_FresnelNoiseStrength";
        const string fresnelNoiseStrengthLabel = "Fresnel Noise Strength";
        const string fresnelNoiseStrengthTooltip = "How strongly noise affects the bubble layer colors.";

        MaterialProperty fresnelNoiseScaleProp = null;
        const string fresnelNoiseScaleName = "_FresnelNoiseScale";
        const string fresnelNoiseScaleLabel = "Fresnel Noise Scale";
        const string fresnelNoiseScaleTooltip = "How detailed the noise applied to the fresnel layer should be.";

        MaterialProperty iridescentStrengthProp = null;
        const string iridescentStrengthName = "_IridescentStrength";
        const string iridescentStrengthLabel = "Iridescent Strength";
        const string iridescentStrengthTooltip = "How visible the bubble layer is.";

        MaterialProperty iridescentFlowDirectionProp = null;
        const string iridescentFlowDirectionName = "_IridescentFlowDirection";
        const string iridescentFlowDirectionLabel = "Iridescent Flow Direction";
        const string iridescentFlowDirectionTooltip = "Which direction the Fresnel noise should be pointing.";

        MaterialProperty iridescentFlowSpeedProp = null;
        const string iridescentFlowSpeedName = "_IridescentFlowSpeed";
        const string iridescentFlowSpeedLabel = "Iridescent Flow Speed";
        const string iridescentFlowSpeedTooltip = "How quickly the noise scrolls in the flow direction.";

        MaterialProperty refractiveIndexProp = null;
        const string refractiveIndexName = "_RefractiveIndex";
        const string refractiveIndexLabel = "Refractive Index";
        const string refractiveIndexTooltip = "How strongly light should bend through the object. Higher values mean lower bending.";

        MaterialProperty useEmissionProp = null;
        const string useEmissionName = "_UseEmission";
        const string useEmissionLabel = "Use Emission";
        const string useEmissionTooltip = "Should the bubble layer be applied to emission or base color?";

        MaterialProperty cameraTextureModeProp = null;
        const string cameraTextureModeName = "_CAMERA_TEXTURE_MODE";
        const string cameraTextureModeLabel = "Camera Texture Mode";
        const string cameraTextureModeTooltip = "Should this effect use the _CameraOpaqueTexture, or the _CameraTransparentTexture?\n\n" +
            "Warning: Transparent mode requires the experimental Copy Transparent Texture renderer feature from Shader Toolbox, which incurs a performance penalty. Consult the documentation for usage details.";

        private bool firstTimeOpen = true;

        protected override void FindProperties(MaterialProperty[] props)
        {
            base.FindProperties(props);

            colorRampTextureProp = FindProperty(colorRampTextureName, props, true);
            useColorRampTextureProp = FindProperty(useColorRampTextureName, props, true);
            fresnelPowerProp = FindProperty(fresnelPowerName, props, true);
            fresnelNoiseStrengthProp = FindProperty(fresnelNoiseStrengthName, props, true);
            fresnelNoiseScaleProp = FindProperty(fresnelNoiseScaleName, props, true);
            iridescentStrengthProp = FindProperty(iridescentStrengthName, props, true);
            iridescentFlowDirectionProp = FindProperty(iridescentFlowDirectionName, props, true);
            iridescentFlowSpeedProp = FindProperty(iridescentFlowSpeedName, props, true);
            refractiveIndexProp = FindProperty(refractiveIndexName, props, true);
            useEmissionProp = FindProperty(useEmissionName, props, true);
            cameraTextureModeProp = FindProperty(cameraTextureModeName, props, true);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            base.OnGUI(materialEditor, properties);
            FindProperties(properties);

            if (firstTimeOpen)
            {
                materialScopeList.RegisterHeaderScope(new GUIContent("Bubble Properties"), 1u << 2, DrawBubbleOptions);
                firstTimeOpen = false;
            }

            materialEditor.serializedObject.ApplyModifiedProperties();
        }

        protected override void DrawBanner()
        {
            // Try and retrieve the banner texture if we don't have it yet.
            if (bannerTexture == null)
            {
                bannerTexture = Resources.Load<Texture2D>("BubbleBanner");
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
                EditorGUILayout.LabelField("Bubbles", headerStyle);
            }

            EditorGUILayout.LabelField("An iridescent, transparent bubble with customizable color ramps and animated flow properties.", descriptionStyle);
            GUILayout.Space(5);
        }

        private void DrawBubbleOptions(Material material)
        {
            EditorGUILayout.Space(3);
            
            materialEditor.TexturePropertySingleLine(new GUIContent(colorRampTextureLabel, colorRampTextureTooltip), colorRampTextureProp);
            var texture = material.GetTexture(colorRampTextureName);

            if (texture == null)
            {
                material.SetFloat(useColorRampTextureName, 0.0f);
                material.DisableKeyword(useColorRampTextureName);
            }
            else
            {
                GUILayout.Space(5);
                var rect = GUILayoutUtility.GetRect(EditorGUIUtility.fieldWidth + EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
                EditorGUI.DrawPreviewTexture(rect, texture);
                GUILayout.Space(5);

                material.SetFloat(useColorRampTextureName, 1.0f);
                material.EnableKeyword(useColorRampTextureName);
            }

            materialEditor.ShaderProperty(fresnelPowerProp, new GUIContent(fresnelPowerLabel, fresnelPowerTooltip));
            materialEditor.ShaderProperty(fresnelNoiseStrengthProp, new GUIContent(fresnelNoiseStrengthLabel, fresnelNoiseStrengthTooltip));
            materialEditor.ShaderProperty(fresnelNoiseScaleProp, new GUIContent(fresnelNoiseScaleLabel, fresnelNoiseScaleTooltip));
            materialEditor.ShaderProperty(iridescentStrengthProp, new GUIContent(iridescentStrengthLabel, iridescentStrengthTooltip));
            materialEditor.ShaderProperty(iridescentFlowDirectionProp, new GUIContent(iridescentFlowDirectionLabel, iridescentFlowDirectionTooltip));
            materialEditor.ShaderProperty(iridescentFlowSpeedProp, new GUIContent(iridescentFlowSpeedLabel, iridescentFlowSpeedTooltip));
            materialEditor.ShaderProperty(refractiveIndexProp, new GUIContent(refractiveIndexLabel, refractiveIndexTooltip));
            materialEditor.ShaderProperty(useEmissionProp, new GUIContent(useEmissionLabel, useEmissionTooltip));
            materialEditor.ShaderProperty(cameraTextureModeProp, new GUIContent(cameraTextureModeLabel, cameraTextureModeTooltip));
        }
    }
}

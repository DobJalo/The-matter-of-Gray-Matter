namespace ShaderToolboxPro.URP
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class GoochShaderGUI : ToolboxShaderGUI
    {
        MaterialProperty warmColorProp = null;
        const string warmColorName = "_WarmColor";
        const string warmColorLabel = "Warm Color";
        const string warmColorTooltip = "Color applied to lit side of the object.";

        MaterialProperty coldColorProp = null;
        const string coldColorName = "_ColdColor";
        const string coldColorLabel = "Cold Color";
        const string coldColorTooltip = "Color applied to unlit side of the object.";

        MaterialProperty temperatureOffsetProp = null;
        const string temperatureOffsetName = "_TemperatureOffset";
        const string temperatureOffsetLabel = "Temperature Offset";
        const string temperatureOffsetTooltip = "Skews the temperature towards cold (values below 1) or towards hot (values above 1).";

        MaterialProperty specularPowerProp = null;
        const string specularPowerName = "_SpecularPower";
        const string specularPowerLabel = "Specular Power";
        const string specularPowerTooltip = "Controls size of the specular highlight. Larger values result in smaller highlights.";

        MaterialProperty specularColorProp = null;
        const string specularColorName = "_SpecularColor";
        const string specularColorLabel = "Specular Color";
        const string specularColorTooltip = "Color of the specular highlights.";

        MaterialProperty useHCLColorSpaceProp = null;
        const string useHCLColorSpaceName = "_USE_HCL_COLOR_SPACE";
        const string useHCLColorSpaceLabel = "Use HCL Color Space";
        const string useHCLColorSpaceTooltip = "Use HCL color space to blend colors." +
            "\nBlending with HCL color space better preserves the luminosity of the input colors over RGB blending." + 
            "\nHCL color blending is slightly more expensive due to RGB->HCL conversion.";

        private bool firstTimeOpen = true;

        protected override void FindProperties(MaterialProperty[] props)
        {
            warmColorProp = FindProperty(warmColorName, props, true);
            coldColorProp = FindProperty(coldColorName, props, true);
            temperatureOffsetProp = FindProperty(temperatureOffsetName, props, true);
            specularPowerProp = FindProperty(specularPowerName, props, true);
            specularColorProp = FindProperty(specularColorName, props, true);
            useHCLColorSpaceProp = FindProperty(useHCLColorSpaceName, props, true);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            base.OnGUI(materialEditor, properties);
            FindProperties(properties);

            if (firstTimeOpen)
            {
                materialScopeList.RegisterHeaderScope(new GUIContent("Gooch Properties"), 1u << 0, DrawGoochOptions);
                firstTimeOpen = false;
            }

            materialEditor.serializedObject.ApplyModifiedProperties();
        }

        protected override void DrawBanner()
        {
            // Try and retrieve the banner texture if we don't have it yet.
            if (bannerTexture == null)
            {
                bannerTexture = Resources.Load<Texture2D>("GoochBanner");
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
                EditorGUILayout.LabelField("Gooch", headerStyle);
            }

            EditorGUILayout.LabelField("A non-photorealistic rendering technique used in computer-aided design which emphasizes normal directions.", descriptionStyle);
            GUILayout.Space(5);
        }

        private void DrawGoochOptions(Material material)
        {
            EditorGUILayout.Space(3);

            materialEditor.ShaderProperty(warmColorProp, new GUIContent(warmColorLabel, warmColorTooltip));
            materialEditor.ShaderProperty(coldColorProp, new GUIContent(coldColorLabel, coldColorTooltip));
            materialEditor.ShaderProperty(temperatureOffsetProp, new GUIContent(temperatureOffsetLabel, temperatureOffsetTooltip));
            materialEditor.ShaderProperty(specularPowerProp, new GUIContent(specularPowerLabel, specularPowerTooltip));
            materialEditor.ShaderProperty(specularColorProp, new GUIContent(specularColorLabel, specularColorTooltip));
            materialEditor.ShaderProperty(useHCLColorSpaceProp, new GUIContent(useHCLColorSpaceLabel, useHCLColorSpaceTooltip));
        }
    }
}

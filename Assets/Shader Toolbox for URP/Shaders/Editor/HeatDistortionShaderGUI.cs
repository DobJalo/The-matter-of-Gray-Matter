namespace ShaderToolboxPro.URP
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class HeatDistortionShaderGUI : ToolboxShaderGUI
    {
        MaterialProperty heatOriginPointProp = null;
        const string heatOriginPointName = "_HeatOriginPoint";
        const string heatOriginPointLabel = "Heat Origin Point";
        const string heatOriginPointTooltip = "The central point (in world space) from which the heat spreads.";

        MaterialProperty heatFalloffProp = null;
        const string heatFalloffName = "_HeatFalloff";
        const string heatFalloffLabel = "Heat Falloff";
        const string heatFalloffTooltip = "How swiftly the heat distortion strength is reduced at increasing distance from the origin.";

        MaterialProperty distortionMapProp = null;
        const string distortionMapName = "_DistortionMap";
        const string distortionMapLabel = "Distortion Map";
        const string distortionMapTooltip = "Flow map which controls the heat distortion pattern.";

        MaterialProperty distortionStrengthProp = null;
        const string distortionStrengthName = "_DistortionStrength";
        const string distortionStrengthLabel = "Distortion Strength";
        const string distortionStrengthTooltip = "How strongly the opaque texture gets distorted by the distortion map.";

        MaterialProperty distortionVelocityProp = null;
        const string distortionVelocityName = "_DistortionVelocity";
        const string distortionVelocityLabel = "Distortion Velocity";
        const string distortionVelocityTooltip = "How quickly the distortion map scrolls over the screen.";

        MaterialProperty distortionTilingProp = null;
        const string distortionTilingName = "_DistortionTiling";
        const string distortionTilingLabel = "Distortion Tiling";
        const string distortionTilingTooltip = "How many times the distortion map is tiled across the screen.";

        MaterialProperty cameraTextureModeProp = null;
        const string cameraTextureModeName = "_CAMERA_TEXTURE_MODE";
        const string cameraTextureModeLabel = "Camera Texture Mode";
        const string cameraTextureModeTooltip = "Should this effect use the _CameraOpaqueTexture, or the _CameraTransparentTexture?\n\n" +
            "Warning: Transparent mode requires the experimental Copy Transparent Texture renderer feature from Shader Toolbox, which incurs a performance penalty. Consult the documentation for usage details.";

        private bool firstTimeOpen = true;

        protected override void FindProperties(MaterialProperty[] props)
        {
            heatOriginPointProp = FindProperty(heatOriginPointName, props, true);
            heatFalloffProp = FindProperty(heatFalloffName, props, true);
            distortionMapProp = FindProperty(distortionMapName, props, true);
            distortionStrengthProp = FindProperty(distortionStrengthName, props, true);
            distortionVelocityProp = FindProperty(distortionVelocityName, props, true);
            distortionTilingProp = FindProperty(distortionTilingName, props, true);
            cameraTextureModeProp = FindProperty(cameraTextureModeName, props, true);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            base.OnGUI(materialEditor, properties);
            FindProperties(properties);

            if (firstTimeOpen)
            {
                materialScopeList.RegisterHeaderScope(new GUIContent("Heat Distortion Properties"), 1u << 0, DrawHeatOptions);
                firstTimeOpen = false;
            }

            materialEditor.serializedObject.ApplyModifiedProperties();
        }

        protected override void DrawBanner()
        {
            // Try and retrieve the banner texture if we don't have it yet.
            if (bannerTexture == null)
            {
                bannerTexture = Resources.Load<Texture2D>("HeatDistortionBanner");
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
                EditorGUILayout.LabelField("Heat Distortion", headerStyle);
            }

            EditorGUILayout.LabelField("An effect which distorts the camera opaque texture according to a flow map texture.", descriptionStyle);
            GUILayout.Space(5);
        }

        private void DrawHeatOptions(Material material)
        {
            EditorGUILayout.Space(3);

            materialEditor.ShaderProperty(heatOriginPointProp, new GUIContent(heatOriginPointLabel, heatOriginPointTooltip));
            materialEditor.ShaderProperty(heatFalloffProp, new GUIContent(heatFalloffLabel, heatFalloffTooltip));
            materialEditor.ShaderProperty(distortionMapProp, new GUIContent(distortionMapLabel, distortionMapTooltip));
            materialEditor.ShaderProperty(distortionStrengthProp, new GUIContent(distortionStrengthLabel, distortionStrengthTooltip));
            materialEditor.ShaderProperty(distortionVelocityProp, new GUIContent(distortionVelocityLabel, distortionVelocityTooltip));
            materialEditor.ShaderProperty(distortionTilingProp, new GUIContent(distortionTilingLabel, distortionTilingTooltip));
            materialEditor.ShaderProperty(cameraTextureModeProp, new GUIContent(cameraTextureModeLabel, cameraTextureModeTooltip));
        }
    }
}


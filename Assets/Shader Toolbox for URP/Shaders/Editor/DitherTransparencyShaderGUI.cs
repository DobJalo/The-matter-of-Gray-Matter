namespace ShaderToolboxPro.URP
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEngine;

    public sealed class DitherTransparencyShaderGUI : DefaultLitShaderGUI
    {
        MaterialProperty useDitherTextureProp = null;
        const string useDitherTextureName = "_USE_DITHER_TEXTURE";
        const string useDitherTextureLabel = "Use Dither Texture";
        const string useDitherTextureTooltip = "Should the shader use a texture to determine dither thresholds? Otherwise, a Bayer matrix is generated in-shader.";

        MaterialProperty ditherTextureProp = null;
        const string ditherTextureName = "_DitherTexture";
        const string ditherTextureLabel = "Dither Texture";
        const string ditherTextureTooltip = "A texture to use for the dither pattern. The red channel is sampled.";
        
        MaterialProperty ditherSizeProp = null;
        const string ditherSizeName = "_DitherSize";
        const string ditherSizeLabel = "Dither Scale";
        const string ditherSizeTooltip = "Size of the dithering pattern. Note that integer values work best.";

        MaterialProperty opacityProp = null;
        const string opacityName = "_Opacity";
        const string opacityLabel = "Opacity";
        const string opacityTooltip = "Multiplier applied to the object's alpha.";

        private bool firstTimeOpen = true;

        protected override void FindProperties(MaterialProperty[] props)
        {
            base.FindProperties(props);

            useDitherTextureProp = FindProperty(useDitherTextureName, props, true);
            ditherTextureProp = FindProperty(ditherTextureName, props, true);
            ditherSizeProp = FindProperty(ditherSizeName, props, true);
            opacityProp = FindProperty(opacityName, props, true);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            base.OnGUI(materialEditor, properties);
            FindProperties(properties);

            if (firstTimeOpen)
            {
                materialScopeList.RegisterHeaderScope(new GUIContent("Dithering Properties"), 1u << 2, DrawDitherOptions);
                firstTimeOpen = false;
            }

            materialEditor.serializedObject.ApplyModifiedProperties();
        }

        protected override void DrawBanner()
        {
            // Try and retrieve the banner texture if we don't have it yet.
            if (bannerTexture == null)
            {
                bannerTexture = Resources.Load<Texture2D>("DitherBanner");
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
                EditorGUILayout.LabelField("Dither Transparency", headerStyle);
            }

            EditorGUILayout.LabelField("A faux-transparency effect which uses alpha clipping along a Bayer matrix pattern.", descriptionStyle);
            GUILayout.Space(5);
        }

        private void DrawDitherOptions(Material material)
        {
            EditorGUILayout.Space(3);

            materialEditor.ShaderProperty(useDitherTextureProp, new GUIContent(useDitherTextureLabel, useDitherTextureTooltip));

            var useTexture = material.IsKeywordEnabled("_USE_DITHER_TEXTURE");

            if(useTexture)
            {
                materialEditor.ShaderProperty(ditherTextureProp, new GUIContent(ditherTextureLabel, useDitherTextureTooltip));
            }

            materialEditor.ShaderProperty(ditherSizeProp, new GUIContent(ditherSizeLabel, ditherSizeTooltip));
            materialEditor.ShaderProperty(opacityProp, new GUIContent(opacityLabel, opacityTooltip));
        }
    }
}

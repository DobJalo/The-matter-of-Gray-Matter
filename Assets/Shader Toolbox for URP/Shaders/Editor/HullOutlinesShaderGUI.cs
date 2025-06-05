namespace ShaderToolboxPro.URP
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class HullOutlinesShaderGUI : ToolboxShaderGUI
    {
        MaterialProperty outlineColorProp = null;
        const string outlineColorName = "_OutlineColor";
        const string outlineColorLabel = "Outline Color";
        const string outlineColorTooltip = "Base color of the outlines.";

        MaterialProperty outlineThicknessProp = null;
        const string outlineThicknessName = "_OutlineThickness";
        const string outlineThicknessLabel = "Outline Thickness";
        const string outlineThicknessTooltip = "Thickness of the outlines in world units.";

        private bool firstTimeOpen = true;

        protected override void FindProperties(MaterialProperty[] props)
        {
            outlineColorProp = FindProperty(outlineColorName, props, true);
            outlineThicknessProp = FindProperty(outlineThicknessName, props, true);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            base.OnGUI(materialEditor, properties);
            FindProperties(properties);

            if (firstTimeOpen)
            {
                materialScopeList.RegisterHeaderScope(new GUIContent("Hull Outline Properties"), 1u << 0, DrawOutlineOptions);
                firstTimeOpen = false;
            }

            materialEditor.serializedObject.ApplyModifiedProperties();
        }

        protected override void DrawBanner()
        {
            // Try and retrieve the banner texture if we don't have it yet.
            if (bannerTexture == null)
            {
                bannerTexture = Resources.Load<Texture2D>("HullOutlineBanner");
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
                EditorGUILayout.LabelField("Inverted-hull Outlines", headerStyle);
            }

            EditorGUILayout.LabelField("An effect which renders the object inside out and expanded along vertex normals. Best used in the second material slot on a renderer.", descriptionStyle);
            GUILayout.Space(5);
        }

        private void DrawOutlineOptions(Material material)
        {
            EditorGUILayout.Space(3);

            materialEditor.ShaderProperty(outlineColorProp, new GUIContent(outlineColorLabel, outlineColorTooltip));
            materialEditor.ShaderProperty(outlineThicknessProp, new GUIContent(outlineThicknessLabel, outlineThicknessTooltip));
        }
    }
}

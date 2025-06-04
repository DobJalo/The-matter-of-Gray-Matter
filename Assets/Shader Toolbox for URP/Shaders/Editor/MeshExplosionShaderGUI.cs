namespace ShaderToolboxPro.URP
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEngine;

    public sealed class MeshExplosionShaderGUI : DefaultLitShaderGUI
    {
        MaterialProperty expansionModeProp = null;
        const string expansionModeName = "_EXPANSION_MODE";
        const string expansionModeLabel = "Expansion Mode";
        const string expansionModeTooltip = "How to expand the mesh.\n" + 
            "\nNormals: expand along the vertex normal.\n" +
            "\nOffset: expand along the offset vector from the vertex position to the explosion origin.\n" +
            "\nColors: expand along the offset vector from the position data baked into the vertex colors to the explosion origin.\n" + 
            "\nNote that Colors mode works well when combined with the Bake Face Pos to Vertex Color tool.";

        MaterialProperty explosionOriginProp = null;
        const string explosionOriginName = "_ExplosionOriginPoint";
        const string explosionOriginLabel = "Explosion Origin Point";
        const string explosionOriginTooltip = "World origin point that explosions move away from.";

        MaterialProperty explosionDistanceProp = null;
        const string explosionDistanceName = "_ExplosionDistance";
        const string explosionDistanceLabel = "Explosion Distance";
        const string explosionDistanceTooltip = "How far the parts of the mesh extend from the center along the normal vector.";

        MaterialProperty debrisShrinkProp = null;
        const string debrisShrinkName = "_DebrisShrinkSpeed";
        const string debrisShrinkLabel = "Debris Shrink Speed";
        const string debrisShrinkTooltip = "How rapidly each mesh fragment decreases in size at higher distances.";

        MaterialProperty gravityProp = null;
        const string gravityName = "_Gravity";
        const string gravityLabel = "Gravity";
        const string gravityTooltip = "How rapidly each mesh fragment falls to the ground at higher distances.";

        MaterialProperty randomOffsetRangeProp = null;
        const string randomOffsetRangeName = "_RandomOffsetRange";
        const string randomOffsetRangeLabel = "Random Offset Range";
        const string randomOffsetRangeTooltip = "Apply a random multiplier to the explosion distance between these values." + 
            "\n\nTry and keep this value low, since higher values tend to spaghettify the mesh at high explosion distances.";

        private bool firstTimeOpen = true;

        private enum MeshExplosionMode
        {
            NORMALS,
            ORIGIN,
            COLORS
        }

        protected override void FindProperties(MaterialProperty[] props)
        {
            base.FindProperties(props);

            expansionModeProp = FindProperty(expansionModeName, props, true);
            explosionOriginProp = FindProperty(explosionOriginName, props, true);
            explosionDistanceProp = FindProperty(explosionDistanceName, props, true);
            debrisShrinkProp = FindProperty(debrisShrinkName, props, true);
            gravityProp = FindProperty(gravityName, props, true);
            randomOffsetRangeProp = FindProperty(randomOffsetRangeName, props, true);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            base.OnGUI(materialEditor, properties);

            if(firstTimeOpen)
            {
                materialScopeList.RegisterHeaderScope(new GUIContent("Explosion Properties"), 1u << 2, DrawExplosionOptions);
                firstTimeOpen = false;
            }

            materialEditor.serializedObject.ApplyModifiedProperties();
        }

        protected override void DrawBanner()
        {
            // Try and retrieve the banner texture if we don't have it yet.
            if (bannerTexture == null)
            {
                bannerTexture = Resources.Load<Texture2D>("MeshExplosionBanner");
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
                EditorGUILayout.LabelField("Mesh Explosion", headerStyle);
            }

            EditorGUILayout.LabelField("A vertex displacement effect with configurable explosion origin settings.", descriptionStyle);
            GUILayout.Space(5);
        }

        private void DrawExplosionOptions(Material material)
        {
            EditorGUILayout.Space(3);

            materialEditor.ShaderProperty(expansionModeProp, new GUIContent(expansionModeLabel, expansionModeTooltip));

            if(material.GetFloat(expansionModeName) > 0)
            {
                materialEditor.ShaderProperty(explosionOriginProp, new GUIContent(explosionOriginLabel, explosionOriginTooltip));
            }

            materialEditor.ShaderProperty(explosionDistanceProp, new GUIContent(explosionDistanceLabel, explosionDistanceTooltip));
            materialEditor.ShaderProperty(debrisShrinkProp, new GUIContent(debrisShrinkLabel, debrisShrinkTooltip));
            materialEditor.ShaderProperty(gravityProp, new GUIContent(gravityLabel, gravityTooltip));
            materialEditor.ShaderProperty(randomOffsetRangeProp, new GUIContent(randomOffsetRangeLabel, randomOffsetRangeTooltip));
        }
    }
}

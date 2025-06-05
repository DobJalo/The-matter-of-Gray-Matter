namespace ShaderToolboxPro.URP
{
    using System;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEditor.Rendering.Universal.ShaderGUI;
    using UnityEngine;

    public abstract class ToolboxShaderGUI : ShaderGUI
    {
        protected static GUIStyle _headerStyle;
        protected static GUIStyle headerStyle
        {
            get
            {
                if (_headerStyle == null)
                {
                    _headerStyle = new GUIStyle(GUI.skin.label)
                    {
                        wordWrap = true,
                        fontSize = 14,
                        fontStyle = FontStyle.Bold,
                        alignment = TextAnchor.MiddleLeft
                    };
                }

                return _headerStyle;
            }
        }

        protected static GUIStyle _tinyLabelStyle;
        protected static GUIStyle tinyLabelStyle
        {
            get
            {
                if (_tinyLabelStyle == null)
                {
                    _tinyLabelStyle = new GUIStyle(GUI.skin.label)
                    {
                        wordWrap = true,
                        fontSize = 10,
                        fontStyle = FontStyle.Normal,
                        alignment = TextAnchor.MiddleLeft
                    };
                }

                return _tinyLabelStyle;
            }
        }

        protected static GUIStyle _descriptionStyle;
        protected static GUIStyle descriptionStyle
        {
            get
            {
                if (_descriptionStyle == null)
                {
                    _descriptionStyle = new GUIStyle(GUI.skin.label)
                    {
                        richText = true,
                        wordWrap = true,
                        fontStyle = FontStyle.Bold,
                        fontSize = 12
                    };
                }

                return _descriptionStyle;
            }
        }

        protected readonly MaterialHeaderScopeList materialScopeList = new MaterialHeaderScopeList(uint.MaxValue);
        protected MaterialEditor materialEditor;

        protected Texture2D bannerTexture;

        protected abstract void FindProperties(MaterialProperty[] props);

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            DrawBanner();

            if (materialEditor == null)
            {
                throw new ArgumentNullException("No MaterialEditor found (ToolboxShaderGUI).");
            }

            this.materialEditor = materialEditor;

            Material material = materialEditor.target as Material;
            materialScopeList.DrawHeaders(materialEditor, material);
        }

        protected abstract void DrawBanner();
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;

namespace UnityEditor
{

    [CustomPropertyDrawer(typeof(RenderingLayersMaskPropertyAttribute))]
    public class RenderingLayersMaskPropertyDrawer : PropertyDrawer
    {
        private static string[] m_DefaultRenderingLayerNames;
        internal static string[] defaultRenderingLayerNames
        {
            get
            {
                if (m_DefaultRenderingLayerNames == null)
                {
                    m_DefaultRenderingLayerNames = new string[32];
                    for (int i = 0; i < m_DefaultRenderingLayerNames.Length; ++i)
                    {
                        m_DefaultRenderingLayerNames[i] = string.Format("Layer{0}", i + 1);
                    }
                }
                return m_DefaultRenderingLayerNames;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            RenderPipelineAsset srpAsset = GraphicsSettings.currentRenderPipeline;
            bool usingSRP = srpAsset != null;
            if (!usingSRP) { return; }

            var layerNames = srpAsset.renderingLayerMaskNames;
            if (layerNames == null)
            {
                layerNames = defaultRenderingLayerNames;
            }


            object owner = PropertyDrawerUtils.GetParent(property);
            int mask = (int)this.fieldInfo.GetValue(owner);


            EditorGUI.BeginProperty(position, label, property);
            Rect fieldRect = EditorGUI.PrefixLabel(position, new GUIContent(property.displayName));
            int newMask = EditorGUI.MaskField(fieldRect, mask, layerNames);
            if (newMask != mask)
            {
                property.intValue = newMask;
            }

            EditorGUI.EndProperty();
        }
    }
}
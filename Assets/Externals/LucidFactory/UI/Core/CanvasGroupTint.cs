using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace LucidFactory.UI
{
    public class CanvasGroupTint : MonoBehaviour
    {
        [System.Serializable]
        private struct GraphicTintBaseState
        {
            public readonly Color BaseColor;
            public readonly Graphic Graphic;

            public GraphicTintBaseState(Color baseColor, Graphic graphic)
            {
                this.BaseColor = baseColor;
                this.Graphic = graphic;
            }
        }

        private Color tint;
        private GraphicTintBaseState[] graphicsStates;

        private void Awake()
        {
            Scan();
        }
        
        private void Scan()
        {
            var graphics = GetComponents<Graphic>();
            graphicsStates = new GraphicTintBaseState[graphics.Length];
            
            for (int i = 0; i < graphics.Length; i++)
            {
                GraphicTintBaseState graphicTintBaseState = new(baseColor: graphics[i].color, graphic: graphics[i]);
                graphicsStates[i] = graphicTintBaseState;
            }
        }

        private void Start()
        {
            
        }
    }
}

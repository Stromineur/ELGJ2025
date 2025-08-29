using System.Collections;
using System.Collections.Generic;
using LTX;
using LTX.Singletons;
using UnityEngine;
using Untold.Core.Stories.UI;

namespace LucidFactory.UI
{
    public class HoverManagerFactory : ISingletonFactory<HoverManager>
    {
        public HoverManager CreateSingleton()
        {
            HoverManager hoverManager = GameObject.Instantiate(Resources.Load<HoverManager>("Prefabs/HoverManager"));
            Debug.Log($"{hoverManager.name} was created because none was found", hoverManager);
            return hoverManager;
        }
    }
}

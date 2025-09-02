using System;
using UnityEngine;

namespace NecroMotMicon.Script.UI
{
    public class FastForward : MonoBehaviour
    {
        private bool isFastForward;
        
        public void FastForwardButton()
        {
            if(isFastForward)
                Time.timeScale = 1;
            else 
                Time.timeScale = 2;
            isFastForward = !isFastForward;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}

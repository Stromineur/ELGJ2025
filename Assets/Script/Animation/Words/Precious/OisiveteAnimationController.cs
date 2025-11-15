using UnityEngine;

namespace NecroMotMicon.Script.Animation.Words.Precious
{
    public class OisiveteAnimationController : PreciousWordAnimationController
    {
        [SerializeField] protected WordAnimationParameters Slow;
        
        public void PlaySlowAnimation()
        {
            StartAnimation(Slow);
        }
    }
}

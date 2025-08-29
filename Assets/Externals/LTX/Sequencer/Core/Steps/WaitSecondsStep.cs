using System;
using UnityEngine;

namespace LTX.Sequencing.Steps
{
    [Serializable]
    public class WaitSecondsStep : IStep
    {
        private float start;
        readonly float duration;
        readonly bool ignoreTimeScale;
        private float actualTime => ignoreTimeScale ? Time.realtimeSinceStartup : Time.timeSinceLevelLoad;

        public WaitSecondsStep(float duration, bool ignoreTimeScale = false)
        {
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;
        }

        void IStep.OnBegin() => start = actualTime;

        bool IStep.Tick(ISequencer parent) => actualTime - start >= duration;

        void IStep.OnEnd() => start = 0;
    }
}

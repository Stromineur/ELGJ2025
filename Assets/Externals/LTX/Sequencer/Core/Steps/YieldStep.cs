using System;

namespace LTX.Sequencing.Steps
{
    [Serializable]
    public class YieldStep : WaitForFramesStep
    {
        public YieldStep() : base(1)
        {

        }
    }
}

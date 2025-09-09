using System;
using NecroMotMicon.Script.FightingPlan;

namespace NecroMotMicon.Script.Animation.Words
{
    public class BadWordAnimationController : WordAnimationController
    {
        private BadWord badWord => word as BadWord;

        protected override string GetWordName()
        {
            return badWord.BadWordData.name.Split('_', StringSplitOptions.None)[0].ToUpper();
        }
    }
}

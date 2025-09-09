using System;
using NecroMotMicon.Script.FightingPlan;

namespace NecroMotMicon.Script.Animation.Words
{
    public class PreciousWordAnimationController : WordAnimationController
    {
        private PreciousWord preciousWord => word as PreciousWord;
        
        protected override string GetWordName()
        {
            return $"Zombie_{preciousWord.WordData.name.Split('_', StringSplitOptions.None)[0].ToUpper()}";
        }
    }
}

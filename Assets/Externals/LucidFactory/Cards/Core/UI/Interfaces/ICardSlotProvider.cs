using System.Collections.Generic;
using LucidFactory.Cards.UI;
using LucidFactory.UI;

namespace LucidFactory.Cards
{
    public interface ICardSlotProvider : ICardSlotProvider<ICardUI, IDropSlot>
    {
        
    }

    public interface ICardSlotProvider<in T> : ICardSlotProvider<T, IDropSlot>
        where T : ICardUI 
    {

    }

    public interface ICardSlotProvider<in T, out Tu>
        where T : ICardUI 
        where Tu : IDropSlot
    {
        public IEnumerable<Tu> GetSlotsForCard(T cardUI);
    }
}
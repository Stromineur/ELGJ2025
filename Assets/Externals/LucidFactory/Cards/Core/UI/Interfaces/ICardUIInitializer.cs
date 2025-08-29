namespace LucidFactory.Cards.UI
{
    public interface ICardUIInitializer<in T> : ICardUI where T : ICard
    {
        void Init(T card, bool withPlaceholder = false);
        void DeInit(T card);
    }
}
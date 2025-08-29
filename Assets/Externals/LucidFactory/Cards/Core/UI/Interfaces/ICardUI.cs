using LucidFactory.UI;
using UnityEngine;

namespace LucidFactory.Cards.UI
{
    public interface ICardUI : IDraggable
    {
        RectTransform RectTransform { get; }
        Transform transform { get; }
        GameObject gameObject { get; }
    }

    public interface ICardUI<out T> : ICardUI where T : ICard
    {
        T Card { get; }
    }
}
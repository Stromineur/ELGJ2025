namespace LucidFactory.UI
{
    public interface IDropSlot
    {
        int Priority => 1;
        bool IsDraggableOver(IDraggable draggable);
    }

    public interface IDropSlotWithCallbacks : IDropSlot
    {
        void OnDraggableEnter(IDraggable draggable);
        void OnDraggableExit(IDraggable draggable);
        void OnDraggableDrop(IDraggable draggable);
    }
}

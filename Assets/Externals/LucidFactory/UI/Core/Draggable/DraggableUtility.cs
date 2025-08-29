using UnityEngine;

namespace LucidFactory.UI
{
    public static class DraggableUtility
    {
        private static readonly Vector3[] points = new Vector3[4];


        public static bool IsRectOver(this IDraggable draggable, RectTransform rectTransform)
        {
            return IsRectOver(draggable, rectTransform, draggable.Canvas);
        }

        public static bool IsRectOver(this IDraggable draggable, RectTransform rectTransform, Canvas canvas)
        {
            rectTransform.GetWorldCorners(points);

            Rect rect1 = GetScreenRectForRectTranform(rectTransform, canvas);
            Rect rect2 = GetScreenRectForRectTranform(draggable.RectTransform, draggable.Canvas);
            
            //Debug.Log($"\n{rectTransform.name} : Center = {rect1.center} Size = {rect1.size} \n {draggable.transform.name} : Center = {rect2.center} : Size = {rect2.size}");
            //Debug.Log(rect1.Overlaps(rect2, true));
            return rect1.Overlaps(rect2, true);
        }

        public static Rect GetScreenRectForRectTranform(RectTransform rectTransform, Canvas canvas)
        {
            Camera camera = canvas.worldCamera == null ? Camera.main : canvas.worldCamera;
            return GetScreenRectForRectTranform(rectTransform, camera);
        }
        public static Rect GetScreenRectForRectTranform(RectTransform rectTransform, Camera camera)
        {
            rectTransform.GetWorldCorners(points);

            for (int i = 0; i < 4; i++)
                points[i] = camera.WorldToScreenPoint(points[i]);

            //Debug.Log($"Screenpoints : {points[0]},{points[1]},{points[2]},{points[3]}");

            Vector2 center = (points[0] + points[1] + points[2] + points[3]) / 4;

            Rect rect = new(center, Vector2.zero)
            {
                min = points[0],
                max = points[2]
            };

            return rect;
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Untold.Core.Stories.UI
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI body;
        [SerializeField] private Image image;

        /// <summary>
        /// Define the tooltip position to stay in canvas, at the bottom of the mouse cursor (right or left depending where is the cursor on screen)
        /// </summary>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public Vector3 DefinePositionInCanvas(Canvas canvas)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = canvas.transform.position.z;
            Vector3 objectPos = mousePos;//_camera.ScreenToViewportPoint(mousePos);

            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                return mousePos;
            }
            else
            {
                RectTransform ToolTipRect = gameObject.GetComponent<RectTransform>();
                RectTransform CanvasRect = canvas.GetComponent<RectTransform>();

                if (canvas.worldCamera.ScreenToViewportPoint(mousePos).x < 0.6f)
                {
                    ToolTipRect.pivot = new Vector2(0f, 1f);
                }
                else
                {
                    ToolTipRect.pivot = new Vector2(1f, 1f);
                }

                float minX = (canvas.pixelRect.xMin);
                float maxX = (canvas.pixelRect.xMax);
                float minY = ToolTipRect.rect.height;
                float maxY = (canvas.pixelRect.yMax);

                objectPos.x = Mathf.Clamp(objectPos.x, minX, maxX);
                objectPos.y = Mathf.Clamp(objectPos.y, minY, maxY);

                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(CanvasRect, objectPos, canvas.worldCamera, out Vector3 canvasPos))
                {
                    return canvasPos;
                }
                return objectPos;
            }
        }

        /// <summary>
        /// Set Up tooltip values based on parameter ObjectToHover that contains Title, Description and optional Sprite
        /// </summary>
        /// <param name="keyWord"></param>
        public void SetTooltipFromWord(HoverInfo keyWord)
        {
            SetTitleText(keyWord.Title);
            SetBodyText(keyWord.Description);
            if(keyWord.Sprite != null)
            {
                SetImage(keyWord.Sprite);
            }
            else
            {
                SetImage(null);
            }
        }

        public void SetBodyText(string text)
        {
            body.text = text;
        }

        public void SetTitleText(string text)
        {
            title.text = text;
        }

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
        }

    }
}
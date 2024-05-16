using UnityEngine;
using UnityEngine.EventSystems;

namespace TheAshBot.UI
{
    public class DragUIWindow : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler
    {


        #region Variables

        [Header("If these are null then it will automatically try to find them")]
        [SerializeField] private RectTransform dragRectTransform;
        [SerializeField] private Canvas canvas;


        private Vector2 mouseOffset;

        #endregion


        #region Unity Functions

        private void Awake()
        {
            if (dragRectTransform == null)
            {
                dragRectTransform = transform.parent.GetComponent<RectTransform>();
            }

            if (canvas == null)
            {
                // Cycling though all parents until it finds one with a canvas
                Transform textCanvasTransform = transform.parent;
                while (textCanvasTransform != null)
                {
                    if (textCanvasTransform.TryGetComponent(out canvas))
                    {
                        break;
                    }

                    textCanvasTransform = textCanvasTransform.parent;
                }
            }
        }

        #endregion


        #region Interfaces

        public void OnDrag(PointerEventData eventData)
        {
            RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();

            Vector2 anchoredMouse = eventData.position / canvas.scaleFactor;

            Vector2 anchoredPosition = anchoredMouse;
            float padding = 64;

            // making sure it does not go to far off screen
            if (anchoredPosition.x + padding > canvasRectTransform.rect.width)
            {
                // Tooltip has left the screen on right side of the screen
                anchoredPosition.x = canvasRectTransform.rect.width - padding;
            }
            else if (anchoredPosition.x - padding < 0)
            {
                // Tooltip has left the screen on left side of the screen
                anchoredPosition.x = padding;
            }
            
            if (anchoredPosition.y + padding > canvasRectTransform.rect.height)
            {
                // Tooltip has left the screen on top side of the screen
                anchoredPosition.y = canvasRectTransform.rect.height - padding;
            }
            else if (anchoredPosition.y - padding < 0)
            {
                // Tooltip has left the screen on bottom side of the screen
                anchoredPosition.y = padding;
            }


            dragRectTransform.anchoredPosition = anchoredPosition - mouseOffset;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Setting this on top
            dragRectTransform.SetAsLastSibling();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // getting the mouse position relative to the position of the window.
            Vector2 anchoredMouse = eventData.position / canvas.scaleFactor;

            mouseOffset = anchoredMouse - dragRectTransform.anchoredPosition;
        }

        #endregion


    }
}

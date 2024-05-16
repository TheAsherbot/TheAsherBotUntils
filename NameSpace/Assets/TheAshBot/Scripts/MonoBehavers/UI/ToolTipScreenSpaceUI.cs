using System;

using TMPro;

using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace TheAshBot.UI
{
    public class ToolTipScreenSpaceUI : MonoBehaviour
    {

        public static ToolTipScreenSpaceUI Instance
        {
            get;
            private set;
        }



        private bool useGetTooltipFunc;
        private Func<string> getTooltipTextFunc;


        [SerializeField] private RectTransform canvasRectTransform;
        [SerializeField] private RectTransform backgroundRectTransform;
        [SerializeField] private TextMeshProUGUI textMeshPro;

        private RectTransform rectTransform;


        private void Awake()
        {
            if (Instance != null)
            {
                this.LogError("There is more than one instance of the ToolTipScreenSpaceUI Class!!! this should NEVER happen!");
                Destroy(this);
                return;
            }

            Instance = this;

            rectTransform = GetComponent<RectTransform>();

            Hide();
        }

        private void LateUpdate()
        {
            if (useGetTooltipFunc)
            {
                SetText(getTooltipTextFunc());
            }

#if ENABLE_INPUT_SYSTEM
            Vector2 anchoredPosition = Mouse.current.position.ReadValue() / canvasRectTransform.localScale.x; // x, y, or z will work here because all of them will be the same.
#elif !ENABLE_INPUT_SYSTEM
            Vector2 anchoredPosition = Input.mousePosition / canvasRectTransfrom.localScale.x; // x, y, or z will work here becouse all of them will be the same.
#else
            return;
#endif

            if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
            {
                // Tooltip has left the screen on right side of the screen
                anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
            }
            else if (anchoredPosition.x < 0)
            {
                // Tooltip has left the screen on left side of the screen
                anchoredPosition.x = 0;
            }

            if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
            {
                // Tooltip has left the screen on top side of the screen
                anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
            }
            else if (anchoredPosition.y < 0)
            {
                // Tooltip has left the screen on bottom side of the screen
                anchoredPosition.y = 0;
            }

            Vector2 offset = new Vector2(8, 8);

            rectTransform.anchoredPosition = anchoredPosition + offset;
        }


        private void SetText(string tooltipText)
        {
            textMeshPro.text = tooltipText;
            textMeshPro.ForceMeshUpdate();

            Vector2 textSize = textMeshPro.GetRenderedValues(false);

            backgroundRectTransform.sizeDelta = textSize;
        }

        private void Show(string tooltipText)
        {
            if (tooltipText == null || tooltipText == string.Empty || tooltipText == "")
            {
                return;
            }

            useGetTooltipFunc = false;
            gameObject.SetActive(true);
            SetText(tooltipText);
        }
        private void Show(out Action<string> OnTooltipChanged)
        {
            useGetTooltipFunc = false;
            OnTooltipChanged = Show;
        }
        private void Show(Func<string> getTooltipTextFunc)
        {
            useGetTooltipFunc = true;
            this.getTooltipTextFunc = getTooltipTextFunc;
            gameObject.SetActive(true);
            SetText(getTooltipTextFunc());
        }

        private void Hide()
        {
            useGetTooltipFunc = false;
            gameObject.SetActive(false);
        }


        /// <summary>
        /// will show the tool tip
        /// </summary>
        /// <param name="tooltipText"></param>
        public static void ShowTooltip(string tooltipText)
        {
            Instance.Show(tooltipText);
        }
        /// <summary>
        /// will show the tool tip
        /// </summary>
        /// <param name="OnTooltipChanged">when triggered the tooltip will change to the desired text</param>
        public static void ShowTooltip(out Action<string> OnTooltipChanged)
        {
            Instance.Show(out OnTooltipChanged);
        }
        /// <summary>
        /// will show the toot tip
        /// </summary>
        /// <param name="getTooltipTextFunc">called every frame. it will allow you to change the tooltip text to what ever you would like very frame.</param>
        public static void ShowTooltip(Func<string> getTooltipTextFunc)
        {
            Instance.Show(getTooltipTextFunc);
        }

        /// <summary>
        /// will disable the tool tip.
        /// </summary>
        public static void HideTooltip()
        {
            Instance.Hide();
        }


    }
}
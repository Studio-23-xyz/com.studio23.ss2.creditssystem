using UnityEngine;
using UnityEngine.UI;


namespace Studio23.SS2.CreditsSystem.Core
{
    public class ScrollViewController : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _scrollContent;
        [SerializeField] private readonly float _scrollSpeed = 500f; // Adjust this value to control the scrolling speed

        [SerializeField]
        private readonly float _scrollDampValue = 100f; // Adjust the damping value to control scroll reset position

        private float _resetPosition; // Position where the content resets

        public bool IsScrolling;
        public bool ShouldReset; // Boolean to determine whether to reset or not

        private Vector2 _contentPosition;

        private void Start()
        {
            // Calculate the reset position based on the content's height
            _resetPosition = _scrollContent.rect.height;
        }


        private void Update()
        {
            if (IsScrolling)
            {
                _contentPosition = _scrollRect.content.anchoredPosition;

                // Move the content upward
                _contentPosition.y += _scrollSpeed * Time.deltaTime;

                // Reset the content to the initial position when it goes beyond _resetPosition
                if (_contentPosition.y + _scrollDampValue >= _resetPosition && ShouldReset) _contentPosition.y = 0;

                // Update the content's position
                _scrollRect.content.anchoredPosition = _contentPosition;

                // Check if scrolling has reached the end
                if (_contentPosition.y >= _scrollRect.content.rect.height - _scrollRect.viewport.rect.height)
                    OnEndScroll();
            }
        }

        /// <summary>
        ///     Method to be called when scrolling reaches the end
        /// </summary>
        private void OnEndScroll()
        {
            // Do something when scrolling reaches the end
            IsScrolling = false;

            // If you want to return a bool based on the end of the scroll, you can modify this method accordingly
            var shouldContinue = DetermineContinueScroll();
            Debug.Log("End of Scroll. Should Continue: " + shouldContinue);
        }

        /// <summary>
        ///     Method to determine whether scrolling should continue or not
        /// </summary>
        /// <returns></returns>
        private bool DetermineContinueScroll()
        {
            // Implement your logic here to determine whether scrolling should continue
            // For example, you can prompt the user or check some condition
            return true; // Change this based on your logic
        }
    }
}


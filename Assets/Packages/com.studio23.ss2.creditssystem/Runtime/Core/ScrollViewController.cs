using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Studio23.SS2.CreditsSystem.Core
{
    public class ScrollViewController : MonoBehaviour
    {
        public ScrollRect scrollRect;
        public float scrollSpeed = 30.0f; // Adjust this value to control the scrolling speed
        public float resetPosition = -1.0f; // Position where the content resets

        public bool isScrolling;
        private Vector2 contentPosition;

        private void Start()
        {
            // Start the scrolling when the script is enabled
            //isScrolling = true;
        }

        private void Update()
        {
            if (isScrolling)
            {
                contentPosition = scrollRect.content.anchoredPosition;

                // Move the content upward
                contentPosition.y += scrollSpeed * Time.deltaTime;

                // Reset the content to the initial position when it goes beyond resetPosition
                if (contentPosition.y >= resetPosition)
                {
                    contentPosition.y = 0;
                }

                // Update the content's position
                scrollRect.content.anchoredPosition = contentPosition;
            }
        }
    }
}

using System;
using System.Threading;
using Codice.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Studio23.SS2.CreditsSystem.Core
{
    public class ScrollViewController : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _scrollContent;
        [SerializeField] private float _scrollSpeed = 500f; // Adjust this value to control the scrolling speed
        [SerializeField] private float _scrollDampValue = 100f; // Adjust the damping value to control scroll reset position
        private float _resetPosition; // Position where the content resets
        private bool _isScrolling;
        public bool ShouldReset; // Boolean to determine whether to reset or not
        private Vector2 _contentPosition;
        [SerializeField] private float _eventFireWaitDuration;
        public UnityEvent OnScrollEndEvent;
        private CancellationTokenSource _cancelToken;

        private void Start()
        {
            // Calculate the reset position based on the content's height
            _resetPosition = _scrollContent.rect.height;
        }

        public void StartScrolling()
        {
            ResetScrolling();
            _isScrolling = true;
        }

        private void Update()
        {
            if (_isScrolling)
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
        private async void OnEndScroll()
        {
            // Do something when scrolling reaches the end
            var cancelWaitTime = await UniTask.Delay(TimeSpan.FromSeconds(_eventFireWaitDuration),
                cancellationToken: _cancelToken.Token).SuppressCancellationThrow();
            if(cancelWaitTime) return;
            OnScrollEndEvent?.Invoke();
            _isScrolling = false;
        }

        public void ResetScrolling()
        {
            _isScrolling = false;
            _scrollRect.content.anchoredPosition *= new Vector2(1, 0);
            if (_cancelToken != null) _cancelToken?.Cancel();
            _cancelToken = new CancellationTokenSource();
        }
    }
}


using System;
using UnityEngine;
using Runningboy.Collection;
using Cinemachine;

namespace Runningboy.Module
{
    public sealed class IOModule : MonoBehaviour
    {
        #region Drag

        public event EventHandler onBeginDrag;
        public event EventHandler onDuringDrag;
        public event EventHandler onEndDrag;

        readonly DragCallbackArgs dragCallbackArgs = new DragCallbackArgs();

        [Header("Drag Event")]
        [SerializeField]
        private bool _reverse = false;

        public void OnBeginDrag(in object sender, in Vector2 start, in Vector2 current)
        {
            dragCallbackArgs.startScreenPosition = start;
            dragCallbackArgs.currentScreenPosition = current;
            dragCallbackArgs.reverse = _reverse;
            onBeginDrag?.Invoke(sender, dragCallbackArgs);
        }

        public void OnDuringDrag(in object sender, in Vector2 start, in Vector2 current)
        {
            dragCallbackArgs.startScreenPosition = start;
            dragCallbackArgs.currentScreenPosition = current;
            dragCallbackArgs.reverse = _reverse;
            onDuringDrag?.Invoke(sender, dragCallbackArgs);
        }

        public void OnEndDrag(in object sender, in Vector2 start, in Vector2 current)
        {
            dragCallbackArgs.startScreenPosition = start;
            dragCallbackArgs.currentScreenPosition = current;
            dragCallbackArgs.reverse = _reverse;
            onEndDrag?.Invoke(sender, dragCallbackArgs);
        }

        #endregion

        #region Sound



        #endregion

        #region Screen

        private Camera _mainCamera;
        public Camera mainCamera
        {
            get
            {
                if (_mainCamera == null)
                {
                    _mainCamera = Camera.main;
                }
                return _mainCamera;
            }
            private set
            {
                _mainCamera = value;
            }
        }

        private CinemachineConfiner2D _confiner2D;
        public CinemachineConfiner2D confiner2D
        {
            get
            {
                if (_confiner2D == null)
                {
                    _confiner2D = FindObjectOfType<CinemachineConfiner2D>();
                }

                return _confiner2D;
            }
            private set
            {
                _confiner2D = value;
            }
        }

        public Vector3 ScreenToWorldPoint(in Vector2 screenPoint)
        {
            Vector3 screenPoint3 = new Vector3(screenPoint.x, screenPoint.y, 0);
            Vector3 returnPoint = mainCamera.ScreenToWorldPoint(screenPoint3);

            return returnPoint;
        }

        #endregion
    }
}
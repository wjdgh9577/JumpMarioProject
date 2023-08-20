using System;
using System.Collections.Generic;
using UnityEngine;
using Runningboy.Utility;
using Runningboy.Collection;
using Cinemachine;
using Sirenix.OdinInspector;

namespace Runningboy.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        #region Drag Event

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

        #region Scene Load




        #endregion
    }
}

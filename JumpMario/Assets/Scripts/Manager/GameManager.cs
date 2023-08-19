using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runningboy.Utility;
using Runningboy.Collection;

namespace Runningboy.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        #region Drag Event

        public event EventHandler onBeginDrag;
        public event EventHandler onDuringDrag;
        public event EventHandler onEndDrag;

        readonly DragCallbackArgs dragCallbackArgs = new DragCallbackArgs();

        [Header("Drag")]
        [SerializeField]
        private bool reverse = false;

        public void OnBeginDrag(in object sender, in Vector2 start, in Vector2 current)
        {
            dragCallbackArgs.startScreenPosition = start;
            dragCallbackArgs.currentScreenPosition = current;
            dragCallbackArgs.reverse = reverse;
            onBeginDrag?.Invoke(sender, dragCallbackArgs);
        }

        public void OnDuringDrag(in object sender, in Vector2 start, in Vector2 current)
        {
            dragCallbackArgs.startScreenPosition = start;
            dragCallbackArgs.currentScreenPosition = current;
            dragCallbackArgs.reverse = reverse;
            onDuringDrag?.Invoke(sender, dragCallbackArgs);
        }

        public void OnEndDrag(in object sender, in Vector2 start, in Vector2 current)
        {
            dragCallbackArgs.startScreenPosition = start;
            dragCallbackArgs.currentScreenPosition = current;
            dragCallbackArgs.reverse = reverse;
            onEndDrag?.Invoke(sender, dragCallbackArgs);
        }

        #endregion
    }
}

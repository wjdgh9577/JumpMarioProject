using System;
using UnityEngine;

namespace Runningboy.Collection
{
    #region CallbackArgs

    public class DragCallbackArgs : EventArgs
    {
        public Vector2 startScreenPosition;
        public Vector2 currentScreenPosition;
        public bool reverse;
    }

    #endregion

    #region Interfaces

    interface IPlayable
    {

    }

    #endregion
}
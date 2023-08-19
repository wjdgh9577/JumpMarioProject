using System;
using UnityEngine;

namespace Runningboy.Collection
{
    #region Enums

    public enum EntityStatus
    {
        Idle = 1,
        Die = 2,
        Crouch = 4,
        Jump = 8,
        SuperJump = 16,
    }

    #endregion

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
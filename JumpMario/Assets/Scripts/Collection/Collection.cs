using System;
using UnityEngine;

namespace Runningboy.Collection
{
    #region Enums

    public enum EntityStatus
    {
        Idle = 1,
        Jump = 2,
        Die = 4,
    }

    #endregion

    #region CallbackArgs

    public abstract class DragCallbackArgs : EventArgs
    {
        public Vector3 touchPos;
    }

    public class BeginDragCallbackArgs : DragCallbackArgs
    {

    }

    public class DuringDragCallbackArgs : DragCallbackArgs
    {

    }

    public class EndDragCallbackArgs : DragCallbackArgs
    {
        public Vector3 dir;
    }

    #endregion

    #region Interfaces

    interface IPlayable
    {

    }

    #endregion
}
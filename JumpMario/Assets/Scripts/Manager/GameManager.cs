using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runningboy.Utility;

namespace Runningboy.Manager
{
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

    public class GameManager : Singleton<GameManager>
    {
        public void GetPlayerStatus()
        {
            
        }

        #region InGameFields

        public event EventHandler onBeginDrag;
        public event EventHandler onDuringDrag;
        public event EventHandler onEndDrag;

        BeginDragCallbackArgs beginDragCallbackArgs = new BeginDragCallbackArgs();
        DuringDragCallbackArgs duringDragCallbackArgs = new DuringDragCallbackArgs();
        EndDragCallbackArgs endDragCallbackArgs = new EndDragCallbackArgs();

        public void OnBeginDrag(in Vector3 pos)
        {
            beginDragCallbackArgs.touchPos = pos;
            onBeginDrag?.Invoke(this, beginDragCallbackArgs);
        }

        public void OnDuringDrag(in Vector3 pos)
        {
            duringDragCallbackArgs.touchPos = pos;
            onDuringDrag?.Invoke(this, duringDragCallbackArgs);
        }

        public void OnEndDrag(in Vector3 dir)
        {
            endDragCallbackArgs.dir = dir;
            onEndDrag?.Invoke(this, endDragCallbackArgs);
        }

        #endregion
    }
}

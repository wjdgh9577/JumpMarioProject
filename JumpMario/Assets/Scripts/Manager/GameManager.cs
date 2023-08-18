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

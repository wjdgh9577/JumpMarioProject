using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runningboy.Utility
{
    public abstract class PanelBase : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
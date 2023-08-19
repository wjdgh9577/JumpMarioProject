using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runningboy.Utility;
using Runningboy.GUI;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Runningboy.Manager
{
    public class GUIManager : Singleton<GUIManager>
    {
        public InputLayer inputLayer;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        public Vector3 ScreenToWorldPoint(in Vector2 screenPoint)
        {
            Vector3 screenPoint3 = new Vector3(screenPoint.x, screenPoint.y, 0);
            Vector3 returnPoint = mainCamera.ScreenToWorldPoint(screenPoint3);

            return returnPoint;
        }
    }
}
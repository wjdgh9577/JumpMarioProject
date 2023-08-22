using UnityEngine;
using Runningboy.GUI;

namespace Runningboy.Module
{
    public class GUIModule : MonoBehaviour
    {
        public InputLayer inputLayer;
        public MainMenu mainMenu;

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
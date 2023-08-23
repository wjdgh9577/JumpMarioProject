using UnityEngine;
using Runningboy.GUI;

namespace Runningboy.Module
{
    public sealed class GUIModule : MonoBehaviour
    {
        [SerializeField]
        InputLayer _inputLayer;
        [SerializeField]
        LobbyPanel _lobbyPanel;
        [SerializeField]
        MainPanel _mainPanel;
        [SerializeField]
        MenuPanel _menuPanel;
        [SerializeField]
        WorldMapPanel _worldMapPanel;

        public InputLayer inputLayer { get { return _inputLayer; } }
        public LobbyPanel lobbyPanel { get { return _lobbyPanel; } }
        public MainPanel mainPanel { get { return _mainPanel; } }
        public MenuPanel menuPanel { get { return _menuPanel; } }
        public WorldMapPanel worldMapPanel { get { return _worldMapPanel; } }

        private Camera _mainCamera;
        public Camera mainCamera
        {
            get
            {
                if (_mainCamera == null)
                {
                    _mainCamera = Camera.main;
                }
                return _mainCamera;
            }
            private set
            {
                _mainCamera = value;
            }
        }

        public Vector3 ScreenToWorldPoint(in Vector2 screenPoint)
        {
            Vector3 screenPoint3 = new Vector3(screenPoint.x, screenPoint.y, 0);
            Vector3 returnPoint = mainCamera.ScreenToWorldPoint(screenPoint3);

            return returnPoint;
        }
    }
}
using UnityEngine;
using Runningboy.Utility;
using Runningboy.Module;
using Runningboy.Data;
using Sirenix.OdinInspector;
using System;

namespace Runningboy.Manager
{
    public sealed class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        GUIModule _guiModule;
        [SerializeField]
        IOModule _ioModule;
        [SerializeField]
        SceneModule _sceneModule;
        [SerializeField]
        PlayerData _playerData;

        public GUIModule GUIModule { get { return _guiModule; } }
        public IOModule IOModule { get { return _ioModule; } }
        public SceneModule SceneModule { get { return _sceneModule; } }

        private void Reset()
        {
            _guiModule = GetComponent<GUIModule>();
            _ioModule = GetComponent<IOModule>();
            _sceneModule = GetComponent<SceneModule>();
            _playerData = GetComponent<PlayerData>();
        }

        public void StartGame(byte sector, Action<bool> onResponse)
        {
            // TODO: 화면 연출 구현

            // TODO: 선택한 sector의 section 1에서 시작
            if (MapManager.instance.SetMap(sector, 1))
            {
                onResponse(true);
            }
            else
            {
                onResponse(false);
            }
        }

        public void RestartGame(Action<bool> onResponse)
        {
            // TODO: 최근 체크포인트에서 시작
            if (MapManager.instance.SetMap(1, 1))
            {
                onResponse(true);
            }
            else
            {
                onResponse(false);
            }
        }
    }
}

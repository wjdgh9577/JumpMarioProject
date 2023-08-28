using UnityEngine;
using Runningboy.Utility;
using Runningboy.Module;
using Runningboy.Data;
using Runningboy.UI;
using System;

namespace Runningboy.Manager
{
    public sealed class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        IOModule _ioModule;
        [SerializeField]
        SceneModule _sceneModule;

        public IOModule IOModule { get { return _ioModule; } }
        public SceneModule SceneModule { get { return _sceneModule; } }

        private void Reset()
        {
            _ioModule = GetComponent<IOModule>();
            _sceneModule = GetComponent<SceneModule>();
        }

        // 임시
        private void Start()
        {
            if (UIView.TryGetView("LobbyPanel", out var view))
            {
                view.Show();
            }
        }

        public void StartGame(byte sector, Action<bool> onResponse)
        {
            // TODO: 화면 연출 구현

            // TODO: 선택한 sector의 section 1에서 시작
            if (MapManager.instance.SetMap(sector, 1))
            {
                PlayerData.instance.SetLifeMax();
                onResponse(true);
            }
            else
            {
                onResponse(false);
            }
        }

        public void RestartGame(Action<bool> onResponse)
        {
            var data = PlayerData.instance;
            if (data.ReduceLIfe() && MapManager.instance.SetMap(data.lastCheckPoint))
            {
                onResponse(true);
            }
            else
            {
                onResponse(false);
            }
        }

        public void SaveGame()
        {
            PlayerData.instance.SaveData(MapManager.instance.currentSectionData);
        }
    }
}

using UnityEngine;
using Runningboy.Utility;
using Runningboy.Module;
using Runningboy.Data;
using Sirenix.OdinInspector;
using System;
using Runningboy.GUI;

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

        public GUIModule GUIModule { get { return _guiModule; } }
        public IOModule IOModule { get { return _ioModule; } }
        public SceneModule SceneModule { get { return _sceneModule; } }

        private void Reset()
        {
            _guiModule = GetComponent<GUIModule>();
            _ioModule = GetComponent<IOModule>();
            _sceneModule = GetComponent<SceneModule>();
        }

        // �ӽ�
        private void Start()
        {
            GUIModule.lobbyPanel.Show();
        }

        public void StartGame(byte sector, Action<bool> onResponse)
        {
            // TODO: ȭ�� ���� ����

            // TODO: ������ sector�� section 1���� ����
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
            PlayerData.instance.SaveData(MapManager.instance.currentSection.sectionData);
        }
    }
}

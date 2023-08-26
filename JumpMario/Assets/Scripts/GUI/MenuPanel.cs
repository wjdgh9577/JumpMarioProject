using Runningboy.Data;
using Runningboy.Manager;
using Runningboy.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runningboy.GUI
{
    public class MenuPanel : PanelBase
    {
        public override void Show()
        {
            base.Show();

            Time.timeScale = 0;
        }

        public override void Hide()
        {
            base.Hide();

            Time.timeScale = 1f;
        }

        #region Button Events

        public void OnLobbyButton()
        {
            Hide();

            // TODO: 맵 정리

            GameManager.instance.GUIModule.lobbyPanel.Show();
        }

        public void OnCheckPointButton()
        {
            GameManager.instance.RestartGame((success) =>
            {
                if (success)
                {
                    Hide();
                }
                else
                {
                    // TODO: life 부족. 베이스캠프로 가라는 메세지.
                }
            });
        }

        public void OnBaseCampButton()
        {
            // TODO: 최근 체크포인트의 베이스캠프로
            GameManager.instance.StartGame(PlayerData.instance.lastCheckPoint.sectorNumber, (success) =>
            {
                if (success)
                {
                    Hide();

                    // TODO: MainPanel의 life 초기화
                }
                else
                {
                    Debug.LogError("Invalid sector.");
                }
            });
        }

        #endregion
    }
}
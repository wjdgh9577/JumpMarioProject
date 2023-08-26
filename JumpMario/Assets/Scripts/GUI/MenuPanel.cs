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

            // TODO: �� ����

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
                    // TODO: life ����. ���̽�ķ���� ����� �޼���.
                }
            });
        }

        public void OnBaseCampButton()
        {
            // TODO: �ֱ� üũ����Ʈ�� ���̽�ķ����
            GameManager.instance.StartGame(PlayerData.instance.lastCheckPoint.sectorNumber, (success) =>
            {
                if (success)
                {
                    Hide();

                    // TODO: MainPanel�� life �ʱ�ȭ
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
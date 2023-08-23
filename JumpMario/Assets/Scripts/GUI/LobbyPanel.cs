using Runningboy.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runningboy.Utility;

namespace Runningboy.GUI
{
    public class LobbyPanel : PanelBase
    {
        public void OnStartButton()
        {
            // TODO: Sector ¼±ÅÃ
            GameManager.instance.StartGame(1, (success) =>
            {
                if (success)
                {
                    Hide();
                }
                else
                {
                    Debug.LogError("Invalid sector.");
                }
            });
        }
    }
}
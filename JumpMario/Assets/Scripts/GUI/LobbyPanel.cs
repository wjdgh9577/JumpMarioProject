using Runningboy.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runningboy.Utility;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Runningboy.Data;

namespace Runningboy.GUI
{
    public class LobbyPanel : PanelBase
    {
        [SerializeField]
        GameObject _login;
        [SerializeField]
        GameObject _sectorSelect;

        [SerializeField]
        private Toggle[] _sectorToggles;
        [SerializeField, ReadOnly]
        private byte _sector = 1;

        #region Button Events

        public void OnNewGameButton()
        {
            PlayerData.instance.NewGameData();
            Login();
        }

        public void OnLoadGameButton()
        {
            if (PlayerData.instance.LoadData())
            {
                Login();
            }
        }

        public void OnStartButton()
        {
            MapManager.instance.Init();
            // TODO: Sector ¼±ÅÃ
            GameManager.instance.StartGame(_sector, (success) =>
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

        #endregion

        public override void Show()
        {
            base.Show();

            _login.SetActive(true);
            _sectorSelect.SetActive(false);
        }

        public void Login()
        {
            _login.SetActive(false);
            _sectorSelect.SetActive(true);

            for (int i = 1; i < _sectorToggles.Length; i++)
            {
                _sectorToggles[i].gameObject.SetActive(PlayerData.instance.isVisitSector((byte)(i + 1)));
            }
            _sectorToggles[0].gameObject.SetActive(true);
            _sectorToggles[0].isOn = true;
        }

        public void SetCurrentSector(int sector)
        {
            _sector = (byte)sector;
        }
    }
}
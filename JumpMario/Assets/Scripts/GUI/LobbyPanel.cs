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
        private Transform _toggleRoot;
        [SerializeField]
        private GameObject _togglePrefab;
        [SerializeField]
        private List<SectorToggle> _sectorToggles = new List<SectorToggle>();
        [SerializeField, ReadOnly]
        private byte _sector = 1;

        #region Button Events

        public void OnNewGameButton()
        {
            PlayerData.instance.NewGameData();
            _sector = 1;
            OnStartButton();
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

            SetSectorToggles();
        }

        public void SetSectorToggles()
        {
            for (int i = 0; i < _sectorToggles.Count; i++)
            {
                _sectorToggles[i].gameObject.SetActive(false);
            }

            var sectorLIst = PlayerData.instance.GetVisitSectors();

            for (int i = 0; i < sectorLIst.Count; i++)
            {
                SectorToggle _toggle = null;
                try
                {
                    _toggle = _sectorToggles[i];
                }
                catch
                {
                    _toggle = Instantiate(_togglePrefab, _toggleRoot).GetComponent<SectorToggle>();
                    _sectorToggles.Add(_toggle);
                }
                finally
                {
                    var _sectorNumber = (byte)(i + 1);
                    _toggle.gameObject.SetActive(true);
                    _toggle.SetToggle(_sectorNumber, () =>
                    {
                        _sector = _sectorNumber;
                    });
                }
            }

            _sectorToggles[0].Select();
        }

        public void SetCurrentSector(int sector)
        {
            _sector = (byte)sector;
        }
    }
}
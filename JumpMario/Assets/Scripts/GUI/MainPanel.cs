using Runningboy.Data;
using Runningboy.Manager;
using Runningboy.Map;
using Runningboy.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runningboy.GUI
{
    public class MainPanel : PanelBase
    {
        [SerializeField]
        Text _sectionText;
        [SerializeField]
        Text _lifeText;

        private void Start()
        {
            MapManager.instance.onSectionChanged += SetSectionText;
            PlayerData.instance.onLifeChanged += UpdateLifeText;
        }

        #region Button Events

        public void OnMenuButton()
        {
            GameManager.instance.GUIModule.menuPanel.Show();
        }

        public void OnWorldMapButton()
        {
            GameManager.instance.GUIModule.worldMapPanel.Show();
        }

        #endregion

        public void SetSectionText(SectionData sectionData)
        {
            _sectionText.text = sectionData.ToString();
        }

        public void UpdateLifeText(int current, int max)
        {
            _lifeText.text = $"{current}/{max}";
        }
    }
}
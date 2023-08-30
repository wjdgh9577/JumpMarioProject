using Runningboy.Data;
using Runningboy.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runningboy.UI
{
    public class MainPanel : UIView
    {
        protected void Start()
        {
            MapManager.instance.onSectionChanged += SetSectionText;
            PlayerData.instance.onLifeChanged += UpdateLifeText;
        }

        #region Button Events

        public void OnMenuButton()
        {
            var view = GetView("MenuPanel");
            view?.Show();
        }

        public void OnWorldMapButton()
        {
            var view = GetView("WorldMapPanel");
            view?.Show();
        }

        #endregion

        public void SetSectionText(SectionData sectionData)
        {
            SetValue("RoomText", sectionData.ToString());
        }

        public void UpdateLifeText(int current, int max)
        {
            SetValue("LifeText", $"{current}/{max}");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runningboy.Utility;

namespace Runningboy.GUI
{
    public class WorldMapPanel : PanelBase
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
    }
}
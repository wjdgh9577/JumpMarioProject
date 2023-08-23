using Runningboy.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runningboy.Data
{
    public class PlayerData
    {
        public static PlayerData Instance = new PlayerData();

        public SectionData lastCheckPoint;

        #region Save/Load

        public void SaveData()
        {

        }

        public void LoadData()
        {

        }

        #endregion
    }
}
using Runningboy.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runningboy.Data
{
    public class PlayerData
    {
        public static PlayerData instance = new PlayerData();

        public SectionData lastCheckPoint = new SectionData(1, 1);

        public HashSet<SectionData> visitSections = new HashSet<SectionData>();

        public int life = 5;

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
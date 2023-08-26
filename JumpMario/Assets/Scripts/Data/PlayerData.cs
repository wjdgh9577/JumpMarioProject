using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Newtonsoft.Json;
using JetBrains.Annotations;

namespace Runningboy.Data
{
    [Serializable]
    public struct SectionData
    {
        [HorizontalGroup("")]
        public byte sectorNumber;
        [HorizontalGroup("")]
        public byte sectionNumber;

        public SectionData(byte sector, byte section)
        {
            sectorNumber = sector;
            sectionNumber = section;
        }

        public override string ToString()
        {
            return $"{sectorNumber} - {sectionNumber}";
        }
    }

    public class PlayerData
    {
        class SaveDataFormat
        {
            public HashSet<SectionData> visitSections;
            public int currentLife;
            public int maxLife;

            public SaveDataFormat()
            {
                visitSections = new HashSet<SectionData>();
                currentLife = 5;
                maxLife = 5;
            }

            public SaveDataFormat(SaveDataFormat data)
            {
                visitSections = data.visitSections;
                maxLife = data.maxLife;
                currentLife = maxLife;
            }
        }

        public static PlayerData instance = new PlayerData();

        SaveDataFormat saveData = new SaveDataFormat();
        public SectionData lastCheckPoint = new SectionData(1, 1);

        public event Action<int, int> onLifeChanged;

        public void VisitSection(SectionData sectionData)
        {
            saveData.visitSections.Add(sectionData);
        }

        public bool isVisitSector(byte sectorNumber)
        {
            foreach (var section in saveData.visitSections)
            {
                if (section.sectorNumber == sectorNumber)
                    return true;
            }
            return false;
        }

        public List<byte> GetVisitSectors()
        {
            List<byte> result = new List<byte>();

            foreach (var section in saveData.visitSections)
            {
                if (result.Contains(section.sectorNumber))
                    continue;
                result.Add(section.sectorNumber);
            }
            result.Sort();

            return result;
        }

        public bool isVisitedSection(SectionData sectionData)
        {
            return saveData.visitSections.Contains(sectionData);
        }

        public int GetLife()
        {
            return saveData.currentLife;
        }

        public bool ReduceLIfe()
        {
            if (saveData.currentLife > 1)
            {
                saveData.currentLife -= 1;
                onLifeChanged?.Invoke(saveData.currentLife, saveData.maxLife);
                return true;
            }
            return false;
        }

        public void SetLifeMax()
        {
            saveData.currentLife = saveData.maxLife;
            onLifeChanged?.Invoke(saveData.currentLife, saveData.maxLife);
        }

        #region Save/Load

        public void SaveData(SectionData sectionData)
        {
            lastCheckPoint = sectionData;

            SaveData();

            // TODO: 클라우드에 저장
        }

        private void SaveData()
        {
            if (File.Exists(GetPath()))
            {
                File.Delete(GetPath());
            }

            using (FileStream fs = new FileStream(GetPath(), FileMode.Create, FileAccess.Write))
            {
                string data = JsonConvert.SerializeObject(saveData);
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        public void NewGameData()
        {
            saveData = new SaveDataFormat();
            SaveData();
        }

        public bool LoadData()
        {
            if (!File.Exists(GetPath()))
            {
                return false;
            }

            using (FileStream fs = new FileStream(GetPath(), FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                
                string data = System.Text.Encoding.UTF8.GetString(bytes);
                saveData = new SaveDataFormat(JsonConvert.DeserializeObject<SaveDataFormat>(data));
            }

            return true;

            // TODO: 클라우드에서 불러오기
        }

        private static string GetPath()
        {
            return Path.Combine(Application.persistentDataPath, "save0");
        }

        #endregion
    }
}
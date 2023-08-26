using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runningboy.Utility;
using Runningboy.Map;
using Cinemachine;
using Sirenix.OdinInspector;
using Runningboy.Data;

namespace Runningboy.Manager
{
    public class MapManager : Singleton<MapManager>
    {
        [SerializeField]
        GameObject player;
        [SerializeField]
        Background background;

        [ReadOnly]
        public Section currentSection = null;

        private Dictionary<SectionData, Section> sectionDic = new Dictionary<SectionData, Section>();
        private List<Section> sections = new List<Section>();

        public event Action<SectionData> onSectionChanged;

        public void Init()
        {
            sectionDic.Clear();

            var sections = GetComponentsInChildren<Section>();
            foreach (var section in sections)
            {
                var data = section.Init();
                if (data != null)
                {
                    sectionDic.Add(data.Value, section);
                }
            }
        }

        public void EnterSection(Section section)
        {
            if (!sections.Contains(section))
            {
                ClearSections();
                
                UpdateSection(section);
            }
        }

        private void UpdateSection(Section section)
        {
            if (currentSection != null && currentSection.sectionData.sectorNumber != section.sectionData.sectorNumber)
            {
                background.SetBackground(currentSection.sectionData.sectorNumber, section.sectionData.sectorNumber);
            }
            currentSection = section;

            PlayerData.instance.VisitSection(section.sectionData);

            sections.Add(section);
            section.SetActiveSection(true);

            GameManager.instance.GUIModule.confiner2D.m_BoundingShape2D = section.polygonCollider2D;

            onSectionChanged?.Invoke(section.sectionData);
        }

        private void ClearSections()
        {
            foreach (var section in sections)
                section.SetActiveSection(false);
            sections.Clear();
        }

        public bool SetMap(byte sectorNum, byte sectionNum)
        {
            SectionData key = new SectionData(sectorNum, sectionNum);

            if (sectionDic.TryGetValue(key, out Section section))
            {
                player.SetActive(false);
                section.SetPlayerToCheckPoint(player.transform);
                ClearSections();
                player.SetActive(true);

                background.SetBackground(sectorNum);

                return true;
            }

            return false;
        }

        public bool SetMap(SectionData key)
        {
            if (sectionDic.TryGetValue(key, out Section section))
            {
                player.SetActive(false);
                section.SetPlayerToCheckPoint(player.transform);
                ClearSections();
                player.SetActive(true);

                background.SetBackground(key.sectorNumber);

                return true;
            }

            return false;
        }
    }
}
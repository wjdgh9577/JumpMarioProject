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

        private Dictionary<SectionData, Section> sectionDic = new Dictionary<SectionData, Section>();
        private List<Section> sections = new List<Section>();
        private Section currentSection = null;

        private void Start()
        {
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

            PlayerData.instance.visitSections.Add(section.sectionData);

            sections.Add(section);
            section.SetActiveSection(true);

            GameManager.instance.GUIModule.confiner2D.m_BoundingShape2D = section.polygonCollider2D;
            GameManager.instance.GUIModule.mainPanel.SetSectionText(section.sectionData);
        }

        private void ClearSections()
        {
            foreach (var section in sections)
                section.SetActiveSection(false);
            sections.Clear();
        }

        [Button]
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
    }
}
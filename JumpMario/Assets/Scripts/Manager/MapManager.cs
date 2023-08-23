using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runningboy.Utility;
using Runningboy.Map;
using Cinemachine;
using Sirenix.OdinInspector;

namespace Runningboy.Manager
{
    public class MapManager : Singleton<MapManager>
    {
        [SerializeField]
        GameObject player;

        // TODO: ���� �޴��� ���� �������� ���� ���� �� �ٽ� ���� �ʿ�. Ȥ�� ���� ����� ��찡 ������ ���� �����ο� �Ű��� ��.
        private Dictionary<SectionData, Section> sectionDic = new Dictionary<SectionData, Section>();
        private List<Section> sections = new List<Section>();

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
            sections.Add(section);
            section.SetActiveTileMap(true);

            GameManager.instance.GUIModule.confiner2D.m_BoundingShape2D = section.polygonCollider2D;
        }

        private void ClearSections()
        {
            foreach (var section in sections)
                section.SetActiveTileMap(false);
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

                return true;
            }

            return false;
        }
    }
}